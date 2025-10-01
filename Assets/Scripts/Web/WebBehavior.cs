using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Collections;
using Naninovel.Commands;



public class WebBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{


    [Header("UI Camera")]
    [SerializeField] private Camera _uiCamera;

    [Header("Website Parameters")]
    [SerializeField] private GameObject _currentWebsite;
    public GameObject previousWebsite;



    [Header("Web Sites")]

    [SerializeField] private GameObject _veryThingWebsite;
    [SerializeField] private GameObject _clickWebsite;
    [SerializeField] private GameObject _chatterWebsite;
    [SerializeField] private GameObject _cloverWebsite;
    [SerializeField] private GameObject _salvaVeritateWebsite;


    [Header("Webpage Pages")]

    [SerializeField] private GameObject _currentPage;
    [SerializeField] private GameObject _previousPage;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _exitButton;
    [SerializeField] private GameObject _favoritesButton;


    [Header("Scroll Parameters")]
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private Scrollbar _scrollBar;




    [Header("Bools")]
    [SerializeField] private bool _isClicked = false;
    public static bool WebIsOpen = false;




    void OnEnable()
    {
        DesktopManager.OnVerificationPopUpShown += UnblockWebpageInteraction;
        DesktopManager.OnVerificationPopUpClosed += BlockWebpageInteraction;
        OnEnableWebInteractionCommand.OnEnableWebInteraction += BlockWebpageInteraction;
        OnDisableWebInteractionCommand.OnDisableWebInteraction += UnblockWebpageInteraction;


        WebsiteEvents.OnWebsiteChange += ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews += ChangeWebpageToSalvaVeritate;
        OnOpenCloverCommand.OnOpenClover += ChangeWebpageToClover;
        OnOpenChatterCommand.OnOpenChatter += ChangeWebpageToChatter;
        OnOpenClickCommand.OnOpenClick += ChangeWebpageToClick;
        OnOpenVeryThingCommand.OnOpenVeryThing += ChangeWebpageToVeryThing;

        //change this objects render camera to the Naninovel camera
        _uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();

        WebIsOpen = true;

    }

    void OnDisable()
    {
        DesktopManager.OnVerificationPopUpShown -= UnblockWebpageInteraction;
        DesktopManager.OnVerificationPopUpClosed -= BlockWebpageInteraction;


        OnEnableWebInteractionCommand.OnEnableWebInteraction -= BlockWebpageInteraction;
        OnDisableWebInteractionCommand.OnDisableWebInteraction -= UnblockWebpageInteraction;

        WebsiteEvents.OnWebsiteChange -= ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews -= ChangeWebpageToSalvaVeritate;
        OnOpenCloverCommand.OnOpenClover -= ChangeWebpageToClover;
        OnOpenChatterCommand.OnOpenChatter -= ChangeWebpageToChatter;
        OnOpenClickCommand.OnOpenClick -= ChangeWebpageToClick;
        OnOpenVeryThingCommand.OnOpenVeryThing -= ChangeWebpageToVeryThing;

        _uiCamera = null;
        WebIsOpen = false;

    }



    public void OnBeginDrag(PointerEventData eventData) { return; }
    public void OnDrag(PointerEventData eventData) { return; }
    public void OnEndDrag(PointerEventData eventData) { return; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the exit button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _exitButton)
        {
            this.gameObject.SetActive(false);
        }

        //if the back button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _backButton)
        {
            //if there is a previous page, go back to it
            if (_previousPage != null)
            {
                ChangeWebsitePage(_previousPage);
                Debug.Log("Back Button Clicked");
            }

            else
            {
                Debug.Log("No previous page to go back to");
            }
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _favoritesButton)
        {
            Debug.Log("Favorites Button Clicked");

            //fire an event to tell the story manager that the player clicked the favorites button
            WebsiteEvents.RaisePlayerClickedFavoritesButton();
        }




    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isClicked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isClicked = false;
    }


    void Start()
    {
        ChangeWebsite(_salvaVeritateWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;

    }



    

    public void EnableFavoritesButton()
    {
        _favoritesButton.SetActive(true);
    }

   


    public void ChangeWebsitePage(GameObject newWebsitePage)
    {
        WebIsOpen = true;
        
        Debug.Log("Changing website page to: " + newWebsitePage.name);

        //set the previous page to the current page
        _previousPage = _currentPage;

        //set the current page to the new page
        _currentPage = newWebsitePage;

        //deactivate all children of the webpage object
        foreach (Transform child in _currentWebsite.transform)
        {
            child.gameObject.SetActive(false);
        }

        //set the scrollbar value to 1 (top)
        _scrollBar.value = 1f;
        _scrollView.GetComponent<ScrollRect>().enabled = true;
        _scrollBar.interactable = true;
        

        //activate the new page
        newWebsitePage.SetActive(true);


        if (_currentPage.name == "SalvaVeritate-EndlessFunPage" || _currentPage.name == "SalvaVeritate-ChristianSchoolPage" || _currentPage.name == "SalvaVeritate-AIChangingLifePage")
        {
            //set the sensitivity to 0 (lock it)
            _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 0f;

            //disable the scrollbar
            _scrollView.GetComponent<ScrollRect>().enabled = false;

            
            _scrollBar.interactable = false;
        }
        else
        {
            //set the sensitivity to 1
            _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;
            _scrollView.GetComponent<ScrollRect>().enabled = true;

            _scrollBar.interactable = true;
        }

    }


    public void ChangeWebsite(GameObject newWebsite)
    {
        WebIsOpen = true;


        //set the sensitivity to 1
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;
        _scrollView.GetComponent<ScrollRect>().enabled = true;

        _scrollBar.interactable = true;


        if (newWebsite == null)
        {
            Debug.LogWarning("ChangeWebsite called with null newWebsite");
            return;
        }

        // store previous
        previousWebsite = _currentWebsite;

        // deactivate children of the old current website (if any)
        if (_currentWebsite != null)
        {
            foreach (Transform child in _currentWebsite.transform)
            {
                child.gameObject.SetActive(false);
            }
        }

        // set the current website to the new website first
        _currentWebsite = newWebsite;

        // ensure the new website GameObject is active
        _currentWebsite.SetActive(true);

        // activate first child of the new current website and deactivate others
        if (_currentWebsite.transform.childCount > 0)
        {
            for (int i = 0; i < _currentWebsite.transform.childCount; i++)
            {
                var child = _currentWebsite.transform.GetChild(i).gameObject;
                child.SetActive(i == 0);
            }
        }

        // set the scroll view to the new page's rect transform (if available)
        if (_scrollView != null)
        {
            var scrollRect = _scrollView.GetComponent<ScrollRect>();
            if (scrollRect != null)
            {
                var rect = _currentWebsite.GetComponent<RectTransform>();
                if (rect != null)
                    scrollRect.content = rect;
            }
        }

        // reset scrollbar to top if provided
        if (_scrollBar != null)
            _scrollBar.value = 1f;

        //set the previous website to hidden
        if (previousWebsite != null)
        {
            previousWebsite.SetActive(false);
        }

    }


    public void ChangeWebpageToSalvaVeritate()
    {

        if (_currentWebsite == _salvaVeritateWebsite)
        {
            Debug.Log("Already on Salva Veritate website, no need to change.");
            return;
        }
        
        ChangeWebsite(_salvaVeritateWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

    public void ChangeWebpageToChatter()
    {
        if (_currentWebsite == _chatterWebsite)
        {
            Debug.Log("Already on Chatter website, no need to change.");
            return;
        }

        ChangeWebsite(_chatterWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

    public void ChangeWebpageToClover()
    {
        if (_currentWebsite == _cloverWebsite)
        {
            Debug.Log("Already on Clover website, no need to change.");
            return;
        }

        ChangeWebsite(_cloverWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

    public void ChangeWebpageToVeryThing()
    {
        if (_currentWebsite == _veryThingWebsite)
        {
            Debug.Log("Already on Verything website, no need to change.");
            return;
        }

        ChangeWebsite(_veryThingWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

    public void ChangeWebpageToClick()
    {
        if (_currentWebsite == _clickWebsite)   
        {
            Debug.Log("Already on Click website, no need to change.");
            return;
        }

        ChangeWebsite(_clickWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;
        _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

    public void BlockWebpageInteraction()
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    public void UnblockWebpageInteraction()
    {
        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

}
