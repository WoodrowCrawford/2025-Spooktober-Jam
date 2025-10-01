using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Naninovel;

public class IconBehavior : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public delegate void IconClickHandler();
    public static event IconClickHandler OnWebpageIconClicked;
    public static event IconClickHandler OnFolderIconClicked;
    public static event IconClickHandler OnChatIconClicked;
    public static event IconClickHandler OnArchivesIconClicked;
    public static event IconClickHandler OnSummerPhotosIconClicked;

    public static event IconClickHandler OnIconWantsToSendErrorEvent;
    public static event IconClickHandler OnPlayerWantsToDownloadSummerPhotos;


    [Header("Icon Settings")]
    [SerializeField] private Color _hoverColor = Color.lightGray;
    [SerializeField] private Color _clickedColor = Color.gray;
    [SerializeField] private float _timeHeldDown = 0f;
    [SerializeField] private Camera _uiCamera;
    

    [Header("Screenshots")]
    [SerializeField] private Sprite _screenshotImage;

    [Header("Bool settings")]
    [SerializeField] private bool _isClicked = false;
    [SerializeField] private bool _isDraggable = true;




    void OnEnable()
    {
        _uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void OnDisable()
    {
        _uiCamera = null;
    }



    //what happens when the icon is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //if the wbebsite icon is clicked
        if (gameObject.name == "WebIcon")
        {
            Debug.Log("Web Icon Clicked, open webpage");
            OnWebpageIconClicked?.Invoke();

        }

        //if the folder icon is clicked
        else if (gameObject.name == "FolderIcon")
        {
            Debug.Log("Folder Icon Clicked, open folder app");
            OnFolderIconClicked?.Invoke();
        }

        //if the chat icon is clicked
        else if (gameObject.name == "ChatIcon")
        {
            Debug.Log("Chat Icon Clicked, open chat app");
            OnChatIconClicked?.Invoke();
        }
        else if (gameObject.name == "ScreenshotIcon")
        {
            Debug.Log("Screenshot Icon Clicked, open screenshot app");
            if (_screenshotImage != null)
            {
                WebsiteEvents.RaiseWebsiteImageChange(_screenshotImage);
            }
            
        }

        //if the archives icon is clicked
        else if (gameObject.name == "ArchivesIcon")
        {
            Debug.Log("Archives Icon Clicked, open archives app");
            OnArchivesIconClicked?.Invoke();
        }

        else if (gameObject.name == "SummerPhotosIcon")
        {
            //check if the player has the police ending variable set to true in the story manager
            var customVariableManager = Engine.GetService<ICustomVariableManager>();
            var scriptPlayer = Engine.GetService<IScriptPlayer>();
            if (customVariableManager.TryGetVariableValue<bool>("playerHasPoliceEnding", out bool playerHasPoliceEnding) &&
                customVariableManager.TryGetVariableValue<int>("badChoiceCounter", out int badChoiceCounter))
            {
                if (playerHasPoliceEnding || badChoiceCounter >= 6)
                {
                    //if true, play the bad ending script
                    scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_Police_Ending");

                }
                else
                {
                    //if false, open the summer photos folder normally
                    OnSummerPhotosIconClicked?.Invoke();

                }

            }

        }

        else if (gameObject.name == "SouthwingFile")
        {
            //fire an event that has a pop up error message
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "CodeForXFile")
        {
            Debug.Log("Code for X file clicked, open code for x webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "MapsFile")
        {
            Debug.Log("Maps file clicked, open maps webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "Maps4File")
        {
            Debug.Log("Maps 4 file clicked, open maps 4 webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "NotesFile")
        {
            Debug.Log("Notes file clicked, open notes webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "PngFile")
        {
            Debug.Log("PNG file clicked, open png webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "ServicePassFile")
        {
            Debug.Log("Service Pass file clicked, open service pass webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "FilesHelpFile")
        {
            Debug.Log("Help file clicked, open help webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "Png49File")
        {
            Debug.Log("PNG 49 file clicked, open png 49 webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "ListNamesFile")
        {
            Debug.Log("List Names file clicked, open list names webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "Png52File")
        {
            Debug.Log("PNG 52 file clicked, open png 52 webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "Png53File")
        {
            Debug.Log("PNG 53 file clicked, open png 53 webpage");
            OnIconWantsToSendErrorEvent?.Invoke();
            Folder3AppBehavior.HasInteractedWithFolder3Files = true;
        }

        else if (gameObject.name == "SummerPhotosFolder")
        {

            OnPlayerWantsToDownloadSummerPhotos?.Invoke();



        }



        else if (gameObject.name == "ChatIconTaskbar")
        {
            Debug.Log("Chat Icon Clicked, open chat app");
            OnChatIconClicked?.Invoke();
        }

    }

    //what happens when the icon is clicked down
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer down on icon: " + gameObject.name);
        _isClicked = true;

        GetComponent<Image>().color = _clickedColor;

    }


    //what happens when the icon is released

    public void OnPointerUp(PointerEventData eventData)
    {

        Debug.Log("Pointer released on icon: " + gameObject.name);
        _isClicked = false;

        GetComponent<Image>().color = Color.white;
        
    }


    //what happens when the pointer enters the icon area
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered icon: " + gameObject.name);
        GetComponent<Image>().color = _hoverColor;

    }


    //what happens when the pointer exits the icon area
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited icon: " + gameObject.name);
        GetComponent<Image>().color = Color.white;
    }




  
  
    private void Update()
    {

        //use a timer based feature to determine if the icon is draggable
        if (_isClicked && _isDraggable)
        {
            //start a timer
            _timeHeldDown += Time.deltaTime;

            //after a while, make the icon draggable
            if (_timeHeldDown >= 0.2f)
            {
            
                //move the icon to the mouse position
                //this.transform.position = new Vector3(Mouse.current.position.ReadValue().x, this.transform.position.y, this.transform.position.z);
                this.transform.position = _uiCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10f));
            }

        }
        else
        {
            this.transform.position = this.transform.position;
            _timeHeldDown = 0f;
        }

      
        

    }

    
}
