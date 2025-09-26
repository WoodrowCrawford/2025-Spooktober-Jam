using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Naninovel;

public class DesktopManager : MonoBehaviour, IPointerClickHandler
{
    [Header("Desktop Applications")]
    [SerializeField] private GameObject _webBrowserApp;


    [Header("Zoomed In Image")]
    [SerializeField] private GameObject _zoomedInImage;



    void OnEnable()
    {
        Engine.OnInitializationFinished += HandleInitializationFinished;

        OnDialogueOpenedCommand.OnDialogueOpened += UnblockCanvasRaycast;
        OnDialogueClosedCommand.OnDialogueClosed += BlockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb += OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb += CloseWebApplication;

        IconBehavior.OnWebpageIconClicked += OpenWebApplication;
        WebsiteEvents.OnWebsiteImageChange += ShowZoomedInImage;

        //change this objects render camera to the Naninovel camera
        GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }



    void OnDisable()
    {
        Engine.OnInitializationFinished -= HandleInitializationFinished;

        OnDialogueClosedCommand.OnDialogueClosed -= BlockCanvasRaycast;
        OnDialogueOpenedCommand.OnDialogueOpened -= UnblockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb -= OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb -= CloseWebApplication;

        
        IconBehavior.OnWebpageIconClicked -= OpenWebApplication;
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


    private void HandleInitializationFinished()
    {
        var stateManager = Engine.GetService<IStateManager>();


    }

    public void OpenWebApplication()
    {
        _webBrowserApp.SetActive(true);
    }
    
    public void CloseWebApplication()
    {
        _webBrowserApp.SetActive(false);
    }



    public void ShowZoomedInImage(Sprite imageToShow)
    {
        //show the zoomed in image
        _zoomedInImage.SetActive(true);

        //set the zoomed in image to the interacted image
        _zoomedInImage.GetComponent<Image>().sprite = imageToShow;
    }

    public void BlockCanvasRaycast()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void UnblockCanvasRaycast()
    {
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
