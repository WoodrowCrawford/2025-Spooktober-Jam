using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

public class StartButtonBehavior : MonoBehaviour, IPointerClickHandler
{
    //static events
    public static event Action OnOptionsButtonClicked;

    [SerializeField] private GameObject _startMenu;
    [SerializeField] private GameObject _clickableArea;

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the clickable area is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _clickableArea)
        {
            Debug.Log("Clickable area clicked.");
            OnOptionsButtonClicked?.Invoke();
        }
        
        
    }


   
}


