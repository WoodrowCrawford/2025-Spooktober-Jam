using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class WebBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{

    [Header("Web References")]

    //the actual webpage object
    public GameObject webpage;

    

    [SerializeField] private GameObject _currentPage;
    [SerializeField] private GameObject _previousPage;
    [SerializeField] private GameObject _backButton;
    [SerializeField] private GameObject _exitButton;


    [Header("Bools")]
    [SerializeField] private bool _isClicked = false;
    [SerializeField] private float _timeHeldDown = 0f;


   


    void OnEnable()
    {
        VerythingWebsiteBehavior.OnWebsiteChange += ChangeWebsitePage;
    }

    void OnDisable()
    {
        VerythingWebsiteBehavior.OnWebsiteChange -= ChangeWebsitePage;
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
        foreach (Transform child in webpage.transform)
        {
            child.gameObject.SetActive(false);
        }

        //activate the new page
        newWebsitePage.SetActive(true);
       

        
    }
    
   
}
