using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class WebBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
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

    [Header("Scroll Parameters")]
    [SerializeField] private GameObject _scrollView;
    [SerializeField] private Scrollbar _scrollBar;




    [Header("Bools")]
    [SerializeField] private bool _isClicked = false;
    [SerializeField] private float _timeHeldDown = 0f;





    void OnEnable()
    {
        WebsiteEvents.OnWebsiteChange += ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews += ChangeWebpageToSalvaVeritate;

        //change this objects render camera to the Naninovel camera
        _uiCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void OnDisable()
    {
        WebsiteEvents.OnWebsiteChange -= ChangeWebsitePage;
        OnOpenNewsCommand.OnOpenNews -= ChangeWebpageToSalvaVeritate;

        _uiCamera = null;
    }


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



    private void Update()
    {
        //use a timer based feature to determine if the icon is draggable
        if (_isClicked)
        {
            //start a timer
            _timeHeldDown += Time.deltaTime;

            //after a while, make the icon draggable
            if (_timeHeldDown >= 0.2f)
            {
                //move the icon to the mouse position
                //this.transform.position = new Vector3(Mouse.current.position.ReadValue().x, this.transform.position.y, this.transform.position.z);
                this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, 10f));
            }

        }
        else
        {
            this.transform.position = this.transform.position;
            _timeHeldDown = 0f;
        }
    }


    public void ChangeWebsitePage(GameObject newWebsitePage)
    {

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

        //activate the new page
        newWebsitePage.SetActive(true);


        if (_currentPage.name == "SalvaVeritate-EndlessFunPage" || _currentPage.name == "SalvaVeritate-ChristianSchoolPage" || _currentPage.name == "SalvaVeritate-AIChangingLifePage")
        {
            //set the sensitivity to 0 (lock it)
            _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 0f;

            _scrollBar.interactable = false;
        }
        else
        {
            //set the sensitivity to 1
            _scrollView.GetComponent<ScrollRect>().scrollSensitivity = 1f;

            _scrollBar.interactable = true;
        }

    }


    public void ChangeWebsite(GameObject newWebsite)
    {

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

    }


    public void ChangeWebpageToSalvaVeritate()
    {
        ChangeWebsite(_salvaVeritateWebsite);
        _currentPage = _currentWebsite.transform.GetChild(0).gameObject;

        //reset scrollbar to top
        if (_scrollBar != null)
        {
            _scrollBar.value = 1f;
        }
    }

}
