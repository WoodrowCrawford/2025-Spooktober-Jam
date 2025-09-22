using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerythingWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    

    [Header("Verything pages")]
    [SerializeField] private GameObject _veryThingMainPage;
    [SerializeField] private GameObject veryThingFoundPage;
    [SerializeField] private GameObject _veryThingControlPage;

    [Header("Verything Buttons")]
    [SerializeField] private Image _foundThingsButton;
    [SerializeField] private Image _governmentButton;



    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events here
        //if the found things button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _foundThingsButton.gameObject)
        {
            WebsiteEvents.RaiseWebsiteChange(veryThingFoundPage);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _governmentButton.gameObject)
        {
            WebsiteEvents.RaiseWebsiteChange(_veryThingControlPage);
        }
    }


   
}
