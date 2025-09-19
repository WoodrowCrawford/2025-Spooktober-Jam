using System;
using UnityEngine;

public class DesktopManager : MonoBehaviour
{
    [Header("Desktop Applications")]
    [SerializeField] private GameObject _webBrowserApp;


    void OnEnable()
    {
        IconBehavior.OnWebpageIconClicked += OpenApplication;
    }



    void OnDisable()
    {
        IconBehavior.OnWebpageIconClicked -= OpenApplication;
    }

    public void OpenApplication()
    {
        _webBrowserApp.SetActive(true);
    }

}
