using UnityEngine;
using UnityEngine.EventSystems;
using Naninovel;
using Naninovel.UI;
using System.Collections;

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
            var uiManager = Engine.GetService<IUIManager>();
            uiManager.GetUI<ISaveLoadUI>()?.Show();


        }
        else if (eventData.pointerCurrentRaycast.gameObject == _loadButton)
        {
            Debug.Log("Load button clicked.");
            //implement load functionality here
            var uiManager = Engine.GetService<IUIManager>();
            uiManager.GetUI<ISaveLoadUI>()?.Show();
        }
        else if (eventData.pointerCurrentRaycast.gameObject == _optionsButton)
        {
            Debug.Log("Options button clicked.");
            var uiManager = Engine.GetService<IUIManager>();
            uiManager.GetUI<ISettingsUI>()?.Show();

        }
        else if (eventData.pointerCurrentRaycast.gameObject == _quitButton)
        {
            Debug.Log("Quit button clicked.");
            StartCoroutine(QuitGame());

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


    public IEnumerator QuitGame()
    {
        var audioManager = Engine.GetService<IAudioManager>();
        audioManager.PlaySfx("VA_phrase_8");

        yield return new WaitForSeconds(3.5f);
        Application.Quit();
    }

}
