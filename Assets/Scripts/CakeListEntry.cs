using UnityEngine;
using UnityEngine.UI;
using System;

namespace CakeApp
{ 
    public class CakeListEntry : MonoBehaviour
    {
        [SerializeField]
        Text title, desc;
        [SerializeField]
        Image image;

        public static Action<Cake> ListEntrySelected;

        // Start is called before the first frame update
        public void Initialize(Cake cake)
        {
            title.text = cake.title;
            desc.text = cake.desc;
            StartCoroutine(NetworkAPI.GetImage(cake.image, downloadedImage => image.sprite = downloadedImage));
            GetComponent<Button>().onClick.AddListener(() => ListEntrySelected.Invoke(cake));
        }
    }
}
