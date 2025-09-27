using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Naninovel;

public class DesktopManager : MonoBehaviour, IPointerClickHandler
{
    public delegate void DesktopManagerHandler();
    public static event DesktopManagerHandler OnPopUpErrorClosed;

    [Header("Desktop Applications")]
    [SerializeField] private GameObject _webBrowserApp;
    [SerializeField] private GameObject _folderApp;
    [SerializeField] private GameObject _chatApp;

    [SerializeField] private GameObject _callApp;


    [Header("Zoomed In Image")]
    [SerializeField] private GameObject _zoomedInImage;

    [Header("Error Pop Up")]
    [SerializeField] private Image _errorPopUp;
    [SerializeField] private Image _errorPopUpExitButton;

    void OnEnable()
    {
        Engine.OnInitializationFinished += HandleInitializationFinished;

        OnDialogueOpenedCommand.OnDialogueOpened += UnblockCanvasRaycast;
        OnDialogueClosedCommand.OnDialogueClosed += BlockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb += OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb += CloseWebApplication;
        OnCloseFolder3AppCommand.OnCloseFolder3App += CloseFolderApplication;

        IconBehavior.OnWebpageIconClicked += OpenWebApplication;
        IconBehavior.OnFolderIconClicked += OpenFolderApplication;
        IconBehavior.OnChatIconClicked += OpenChatApplication;
        IconBehavior.OnIconWantsToSendErrorEvent += ShowErrorPopUp;

        WebsiteEvents.OnWebsiteImageChange += ShowZoomedInImage;

        OnShowCallWindowCommand.OnShowCallWindow += OpenCallApplication;
        OnHideCallWindowCommand.OnHideCallWindow += CloseCallApplication;

        //change this objects render camera to the Naninovel camera
        GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }



    void OnDisable()
    {
        Engine.OnInitializationFinished -= HandleInitializationFinished;

        OnDialogueOpenedCommand.OnDialogueOpened -= UnblockCanvasRaycast;
        OnDialogueClosedCommand.OnDialogueClosed -= BlockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb -= OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb -= CloseWebApplication;
        OnCloseFolder3AppCommand.OnCloseFolder3App -= CloseFolderApplication;

        IconBehavior.OnWebpageIconClicked -= OpenWebApplication;
        IconBehavior.OnFolderIconClicked -= OpenFolderApplication;
        IconBehavior.OnChatIconClicked -= OpenChatApplication;
        IconBehavior.OnIconWantsToSendErrorEvent -= ShowErrorPopUp;

        WebsiteEvents.OnWebsiteImageChange -= ShowZoomedInImage;

        OnShowCallWindowCommand.OnShowCallWindow -= OpenCallApplication;
        OnHideCallWindowCommand.OnHideCallWindow -= CloseCallApplication;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events
        if (eventData.pointerCurrentRaycast.gameObject == _zoomedInImage)
        {
            //hide the zoomed in image
            _zoomedInImage.SetActive(false);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _errorPopUpExitButton.gameObject)
        {
            //hide the error pop up
            HideErrorPopUp();
        }
    }


    private void HandleInitializationFinished()
    {
        var stateManager = Engine.GetService<IStateManager>();
    }

    //web app functions
    public void OpenWebApplication()
    {
        _webBrowserApp.SetActive(true);
    }
    
    public void CloseWebApplication()
    {
        _webBrowserApp.SetActive(false);
    }


    //chat app functions
    public void OpenChatApplication()
    {
        _chatApp.SetActive(true);
    }

    public void CloseChatApplication()
    {
        _chatApp.SetActive(false);

        
    }


    //call app functions
    public void OpenCallApplication()
    {
        _callApp.SetActive(true);
    }

    public void CloseCallApplication()
    {
        _callApp.SetActive(false);
    }


    //folder app functions
    public void OpenFolderApplication()
    {
        _folderApp.SetActive(true);
    }

    public void CloseFolderApplication()
    {
        _folderApp.SetActive(false);
    }


    public void ShowZoomedInImage(Sprite imageToShow)
    {
        //show the zoomed in image
        _zoomedInImage.SetActive(true);

        //set the zoomed in image to the interacted image
        _zoomedInImage.GetComponent<Image>().sprite = imageToShow;
    }


    public void ShowErrorPopUp()
    {
        _errorPopUp.gameObject.SetActive(true);

        //play a pop up error sound
        var audioManager = Engine.GetService<IAudioManager>();
        audioManager.PlaySfx("Popup_alert_sfx");
    }

    public void HideErrorPopUp()
    {
        _errorPopUp.gameObject.SetActive(false);

        //signal that the pop up has been closed
        OnPopUpErrorClosed?.Invoke();
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
