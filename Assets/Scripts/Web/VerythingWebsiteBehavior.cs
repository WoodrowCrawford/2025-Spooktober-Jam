using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerythingWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    

    [Header("Verything pages")]
    [SerializeField] private GameObject _veryThingMainPage;
    [SerializeField] private GameObject veryThingFoundPage;
    [SerializeField] private GameObject _veryThingControlPage;
    [SerializeField] private GameObject _theoryPage;

    [Header("Verything Buttons")]
    [SerializeField] private Image _foundThingsButton;
    [SerializeField] private Image _governmentButton;
    [SerializeField] private Image _theoryButton;



    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events here
        //if the found things button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _foundThingsButton.gameObject)
        {
            WebsiteEvents.RaiseWebsiteChange(veryThingFoundPage);

            //raise an event to tell the story manager that the player has clicked the found things button
            WebsiteEvents.RaisePlayerClickedFoundThingPageButton();
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _governmentButton.gameObject)
        {
            WebsiteEvents.RaiseWebsiteChange(_veryThingControlPage);
            //raise an event to tell the story manager that the player has clicked the government button
            WebsiteEvents.RaisePlayerClickedGovernmentPageButton();
        }
        else if (eventData.pointerCurrentRaycast.gameObject == _theoryButton.gameObject)
        {
            WebsiteEvents.RaiseWebsiteChange(_theoryPage);
            WebsiteEvents.RaisePlayerClickedTheoryPageButton();
        }
    }


   
}
