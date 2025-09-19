using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class IconBehavior : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    public delegate void IconClickHandler();
    public static event IconClickHandler OnWebpageIconClicked;


    [Header("Icon Settings")]
    [SerializeField] private Color _hoverColor = Color.lightGray;
    [SerializeField] private Color _clickedColor = Color.gray;
    [SerializeField] private float _timeHeldDown = 0f;


    [Header("Bool settings")]
    [SerializeField] private bool _isClicked = false;

    



    //what happens when the icon is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        //if the wbebsite icon is clicked
        if (gameObject.name == "WebIcon")
        {
            Debug.Log("Web Icon Clicked, open webpage");
           OnWebpageIconClicked?.Invoke();
          
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

    
}
