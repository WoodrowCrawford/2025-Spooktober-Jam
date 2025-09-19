using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VerythingWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    // Define a delegate and event for website changes
    public delegate void WebsiteChangeHandler(GameObject newWebsitePage);
    public static event WebsiteChangeHandler OnWebsiteChange;


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
            //invoke the event to change the webpage to the found things page
            OnWebsiteChange?.Invoke(veryThingFoundPage);
            Debug.Log("Found Things Button Clicked");
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _governmentButton.gameObject)
        {
            //invoke the event to change the webpage to the government control page
            OnWebsiteChange?.Invoke(_veryThingControlPage);
            Debug.Log("Government Button Clicked");
        }
    }


   
}
