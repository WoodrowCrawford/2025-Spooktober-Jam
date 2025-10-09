
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Naninovel;
using Naninovel.UI;

public class CreditsBehavior : MonoBehaviour, IPointerClickHandler
{
    public Image _mainMenuImage;
    public IUIManager _uiManager;


    void OnEnable()
    {
        Engine.OnInitializationFinished += HandleInitializationFinished;
    }

    void OnDisable()
    {
        Engine.OnInitializationFinished -= HandleInitializationFinished;
    }

    

    //if the image is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerCurrentRaycast.gameObject.GetComponent<Image>() == _mainMenuImage)
        {
            GoToMainMenu();
        }
    }

    public void GoToMainMenu()
    {
        // Use Naninovel's @title command behavior (resets state and shows Title UI).
        var cmd = new Naninovel.Commands.ExitToTitle();
        cmd.Execute().Forget();
        gameObject.SetActive(false);
    }

    private void HandleInitializationFinished()
    {
        var UIManager = Engine.GetService<IUIManager>();
        _uiManager = UIManager;
    }
    

}
