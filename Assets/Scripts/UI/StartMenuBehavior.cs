using UnityEngine;
using UnityEngine.EventSystems;

public class StartMenuBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private bool _isOpen = false;

    [Header("Option Menu Buttons")]
    [SerializeField] private GameObject _saveButton;
    [SerializeField] private GameObject _loadButton;
    [SerializeField] private GameObject _optionsButton;
    [SerializeField] private GameObject _quitButton;




    void OnEnable()
    {
        StartButtonBehavior.OnOptionsButtonClicked += ToggleOptionMenu;
    }

    void OnDisable()
    {
        StartButtonBehavior.OnOptionsButtonClicked -= ToggleOptionMenu;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the button clicked is the save button
        if (eventData.pointerCurrentRaycast.gameObject == _saveButton)
        {
            Debug.Log("Save button clicked.");
            //implement save functionality here
        }
        else if (eventData.pointerCurrentRaycast.gameObject == _loadButton)
        {
            Debug.Log("Load button clicked.");
            //implement load functionality here
        }
        else if (eventData.pointerCurrentRaycast.gameObject == _optionsButton)
        {
            Debug.Log("Options button clicked.");
            
        }
        else if (eventData.pointerCurrentRaycast.gameObject == _quitButton)
        {
            Debug.Log("Quit button clicked.");
            
        }
    }

    public void ToggleOptionMenu()
    {
        if (!_isOpen)
        {
            _isOpen = true;
            _startMenu.gameObject.SetActive(true);

            Debug.Log("Options menu opened.");
        }
        else if (_isOpen)
        {
            _isOpen = false;
            _startMenu.gameObject.SetActive(false);

            Debug.Log("Options menu closed.");
        }
    }

    
}
