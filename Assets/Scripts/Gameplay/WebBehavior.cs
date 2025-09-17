using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class WebBehavior : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [SerializeField] private bool _isClicked = false;
    [SerializeField] private float _timeHeldDown = 0f;

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
}
