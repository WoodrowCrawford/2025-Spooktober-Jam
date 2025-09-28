using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Naninovel;
using System.Collections;

public class DesktopManager : MonoBehaviour, IPointerClickHandler
{
    public delegate void DesktopManagerHandler();
    public static event DesktopManagerHandler OnPopUpErrorClosed;


    [Header("Desktop Icons")]
    [SerializeField] private GameObject _webIcon;
    [SerializeField] private GameObject _folderIcon;
    [SerializeField] private GameObject _summerPhotosFolderIcon;
    [SerializeField] private GameObject _chatIcon;
    [SerializeField] private GameObject _archivesIcon;
    
    [SerializeField] private GameObject _notesIcon;



    [Header("Desktop Applications")]
    [SerializeField] private GameObject _webBrowserApp;
    [SerializeField] private GameObject _folderApp;
    [SerializeField] private GameObject _chatApp;

    [SerializeField] private GameObject _callApp;
    [SerializeField] private GameObject _archivesApp;

    [Header("Summer Photos Folder App")]
    [SerializeField] private GameObject _summerPhotosFolderApp;
    [SerializeField] private Sprite _enterPasswordImage;
    [SerializeField] private Sprite _summerPhotosFolderImage;
    [SerializeField] private GameObject _passwordInputField;

    [Header("Zoomed In Image")]
    [SerializeField] private GameObject _zoomedInImage;

    


    [Header("Alert Pop Up")]
    [SerializeField] private Image _alertPopup;
    [SerializeField] private Sprite _errorPopupImage;
    [SerializeField] private Sprite _downloadQuestionPopupImage;
    [SerializeField] private Sprite _archivesDownloadingPopupImage;
    [SerializeField] private Sprite _downloadCompletePopupImage;
    [SerializeField] private Image _alertPopUpExitButton;
    [SerializeField] private Button _downloadYesButton;
    [SerializeField] private Button _downloadNoButton;
    [SerializeField] private bool _canClosePopUp = true;



    void OnEnable()
    {
        Engine.OnInitializationFinished += HandleInitializationFinished;

        StoryManagerBehavior.OnPlayerCanDownloadSummerPhotos+= ShowDownloadQuestionPopUp;

        OnDialogueOpenedCommand.OnDialogueOpened += UnblockCanvasRaycast;
        OnDialogueClosedCommand.OnDialogueClosed += BlockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb += OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb += CloseWebApplication;
        OnCloseFolder3AppCommand.OnCloseFolder3App += CloseFolderApplication;

        IconBehavior.OnWebpageIconClicked += OpenWebApplication;
        IconBehavior.OnFolderIconClicked += OpenFolderApplication;
        IconBehavior.OnChatIconClicked += OpenChatApplication;
        IconBehavior.OnArchivesIconClicked += OpenArchivesApplication;
        IconBehavior.OnIconWantsToSendErrorEvent += ShowErrorPopUp;
        IconBehavior.OnSummerPhotosIconClicked += OpenSummerPhotosFolderApplication;

        SummerPhotosFolderBehavior.OnPasswordCorrect += UnlockSummerPhotos;

        WebsiteEvents.OnWebsiteImageChange += ShowZoomedInImage;

        OnShowCallWindowCommand.OnShowCallWindow += OpenCallApplication;
        OnHideCallWindowCommand.OnHideCallWindow += CloseCallApplication;


        //change this objects render camera to the Naninovel camera
        GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();


        //add listeners to the download question pop up buttons
        _downloadYesButton.onClick.AddListener(() => StartCoroutine(ShowDownloadingPopup()));
        _downloadNoButton.onClick.AddListener(() => HideDownloadQuestionPopUp());
    }



    void OnDisable()
    {
        Engine.OnInitializationFinished -= HandleInitializationFinished;

        StoryManagerBehavior.OnPlayerCanDownloadSummerPhotos -= ShowDownloadQuestionPopUp;

        OnDialogueOpenedCommand.OnDialogueOpened -= UnblockCanvasRaycast;
        OnDialogueClosedCommand.OnDialogueClosed -= BlockCanvasRaycast;

        OnOpenWebCommand.OnOpenWeb -= OpenWebApplication;
        OnCloseWebCommand.OnCloseWeb -= CloseWebApplication;
        OnCloseFolder3AppCommand.OnCloseFolder3App -= CloseFolderApplication;

        IconBehavior.OnWebpageIconClicked -= OpenWebApplication;
        IconBehavior.OnFolderIconClicked -= OpenFolderApplication;
        IconBehavior.OnChatIconClicked -= OpenChatApplication;
        IconBehavior.OnArchivesIconClicked -= OpenArchivesApplication;
        IconBehavior.OnIconWantsToSendErrorEvent -= ShowErrorPopUp;

        SummerPhotosFolderBehavior.OnPasswordCorrect -= UnlockSummerPhotos;

        WebsiteEvents.OnWebsiteImageChange -= ShowZoomedInImage;

        OnShowCallWindowCommand.OnShowCallWindow -= OpenCallApplication;
        OnHideCallWindowCommand.OnHideCallWindow -= CloseCallApplication;

        _downloadYesButton.onClick.RemoveAllListeners();
        _downloadNoButton.onClick.RemoveAllListeners();
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events
        if (eventData.pointerCurrentRaycast.gameObject == _zoomedInImage)
        {
            //hide the zoomed in image
            _zoomedInImage.SetActive(false);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _alertPopUpExitButton.gameObject)
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

    //summer photos folder app functions
    public void OpenSummerPhotosFolderApplication()
    {
        //first check if the summer photos folder app is already unlocked
        if (SummerPhotosFolderBehavior.IsUnlocked)
        {
            //if it is unlocked, just open the app
            _summerPhotosFolderApp.SetActive(true);
        }
        else
        {
            //if it is not unlocked, set the app image to the enter password image and show the password input field
            _summerPhotosFolderApp.GetComponent<Image>().sprite = _enterPasswordImage;
            _passwordInputField.SetActive(true);
            _summerPhotosFolderApp.SetActive(true);
        }
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

    //archives app functions
    public void OpenArchivesApplication()
    {
        _archivesApp.SetActive(true);
    }

    public void CloseArchivesApplication()
    {
        _archivesApp.SetActive(false);
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
        _alertPopup.sprite = _errorPopupImage;
        _alertPopup.gameObject.SetActive(true);

        //hide yes and no buttons
        _downloadYesButton.gameObject.SetActive(false);
        _downloadNoButton.gameObject.SetActive(false);

        //play a pop up error sound
        var audioManager = Engine.GetService<IAudioManager>();
        audioManager.PlaySfx("Popup_alert_sfx");

        _canClosePopUp = true;
    }

    public void HideErrorPopUp()
    {
        _alertPopup.sprite = null;
        _alertPopup.gameObject.SetActive(false);

        //signal that the pop up has been closed
        OnPopUpErrorClosed?.Invoke();
    }

    //download question pop up functions
    public void ShowDownloadQuestionPopUp()
    {
        _alertPopup.sprite = _downloadQuestionPopupImage;
        _alertPopup.gameObject.SetActive(true);

        //show yes and no buttons
        _downloadYesButton.gameObject.SetActive(true);
        _downloadNoButton.gameObject.SetActive(true);

        //play a pop up sound
        var audioManager = Engine.GetService<IAudioManager>();
        audioManager.PlaySfx("Popup_alert_sfx");

        _canClosePopUp = true;
    }

    public void HideDownloadQuestionPopUp()
    {
        _alertPopup.sprite = null;
        _alertPopup.gameObject.SetActive(false);
        _downloadYesButton.gameObject.SetActive(false);
        _downloadNoButton.gameObject.SetActive(false);

        _canClosePopUp = true;
    }

    public IEnumerator ShowDownloadingPopup()
    {
        _alertPopup.sprite = _archivesDownloadingPopupImage;
        _alertPopup.gameObject.SetActive(true);

        //hide yes and no buttons
        _downloadYesButton.gameObject.SetActive(false);
        _downloadNoButton.gameObject.SetActive(false);



        _canClosePopUp = false;

        yield return new WaitForSeconds(2f);

        _canClosePopUp = true;

        //show download complete pop up
        ShowDownloadCompletePopUp();
        
        //show the summer photos folder icon on the desktop
        _summerPhotosFolderIcon.SetActive(true);

    }

    public void ShowDownloadCompletePopUp()
    {
        _alertPopup.sprite = _downloadCompletePopupImage;
        _alertPopup.gameObject.SetActive(true);

        //hide yes and no buttons
        _downloadYesButton.gameObject.SetActive(false);
        _downloadNoButton.gameObject.SetActive(false);

        //play a pop up sound
        var audioManager = Engine.GetService<IAudioManager>();
        audioManager.PlaySfx("Popup_alert_sfx");

        _canClosePopUp = true;
    }

    public void UnlockSummerPhotos()
    {
        //change the summer photos folder app image to the summer photos folder image
        _summerPhotosFolderApp.GetComponent<Image>().sprite = _summerPhotosFolderImage;

        //hide the password input field
        _passwordInputField.SetActive(false);
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
