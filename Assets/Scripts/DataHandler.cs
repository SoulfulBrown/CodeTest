using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace CakeApp
{
    public class DataHandler : MonoBehaviour
    {
        public CakeList cakeList { get; private set; }
        [SerializeField]
        GameObject cakeListEntryPrefab;
       // [SerializeField]
        //GameObject CakeObjectPrefab;
        [SerializeField]
        Transform content;
        List<GameObject> listEntries;
        public GameObject CakeListUI;
        public GameObject ARUI;
        GameObject selectedCakeUI;
        GameObject cakeObject;

        private void Start()
        {
            StartCoroutine(PopulateScrollView());
            CakeListEntry.ListEntrySelected += HideCakeList;
            ReturnButton.ReturnToMenu += ShowCakeList;
        }

        private void ParseData()
        {
            StartCoroutine(NetworkAPI.GetJson(output => cakeList = JsonUtility.FromJson<CakeList>("{ \"cakes\": " + output + "}")));
        }

        private void CreateCakeListEntry()
        {
            var i = 0;
            var entryHeight = 500;

            foreach(Cake cake in cakeList.cakes)
            {
                var listEntry = Instantiate(cakeListEntryPrefab, content);
                var rect = listEntry.GetComponent<RectTransform>().localPosition;
                listEntry.GetComponent<RectTransform>().localPosition = new Vector2(rect.x, rect.y - (entryHeight * i));
                listEntry.GetComponent<CakeListEntry>().Initialize(cake);
                i++;
            }

            content.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(content.gameObject.GetComponent<RectTransform>().sizeDelta.x, entryHeight * i);
        }

        private IEnumerator PopulateScrollView()
        {
            yield return StartCoroutine(NetworkAPI.GetJson(output => cakeList = JsonUtility.FromJson<CakeList>("{ \"cakes\": " + output + "}")));

            CreateCakeListEntry();
        }

        public void HideCakeList(Cake cake)
        {
            CakeListUI.SetActive(false);
            ARUI.SetActive(true);
            selectedCakeUI = Instantiate(cakeListEntryPrefab, ARUI.transform);
            var rect = selectedCakeUI.GetComponent<RectTransform>().localPosition;
            selectedCakeUI.GetComponent<RectTransform>().localPosition = new Vector2(rect.x, -888);
            selectedCakeUI.GetComponent<CakeListEntry>().Initialize(cake);
            Destroy(selectedCakeUI.GetComponent<Button>());
        }

        public void ShowCakeList()
        {
            CakeListUI.SetActive(true);
            ARUI.SetActive(false);
            Destroy(selectedCakeUI);
        }
    }

    [Serializable]
    public class CakeList
    {
        public Cake[] cakes;
    }

    [Serializable]
    public class Cake
    {
        public string title;
        public string image;
        public string desc;
    }
}
