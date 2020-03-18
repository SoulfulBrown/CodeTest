using System;
using UnityEngine;
using UnityEngine.UI;

public class ReturnButton : MonoBehaviour
{
    public static Action ReturnToMenu;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ReturnToMenu.Invoke());
    }
}
