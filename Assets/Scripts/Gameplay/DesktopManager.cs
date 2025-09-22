using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DesktopManager : MonoBehaviour, IPointerClickHandler
{
    [Header("Desktop Applications")]
    [SerializeField] private GameObject _webBrowserApp;


    [Header("Zoomed In Image")]
    [SerializeField] private GameObject _zoomedInImage;



   


    void OnEnable()
    {
        IconBehavior.OnWebpageIconClicked += OpenApplication;
        WebsiteEvents.OnWebsiteImageChange += ShowZoomedInImage;
    }



    void OnDisable()
    {
        IconBehavior.OnWebpageIconClicked -= OpenApplication;
        WebsiteEvents.OnWebsiteImageChange -= ShowZoomedInImage;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events
        if (eventData.pointerCurrentRaycast.gameObject == _zoomedInImage)
        {
            //hide the zoomed in image
            _zoomedInImage.SetActive(false);
        }
    }

    public void OpenApplication()
    {
        _webBrowserApp.SetActive(true);
    }



     public void ShowZoomedInImage(Sprite imageToShow)
    {
        //show the zoomed in image
        _zoomedInImage.SetActive(true);

        //set the zoomed in image to the interacted image
        _zoomedInImage.GetComponent<Image>().sprite = imageToShow;
    }

}
