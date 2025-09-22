using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SalvaVeritateWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    [Header("Salva Veritate pages")]
    [SerializeField] private GameObject _salvaVeritateMainPage;
    [SerializeField] private GameObject _endlessFunPage;
    [SerializeField] private GameObject _christianSchoolPage;
    [SerializeField] private GameObject _aiChangingLifePage;


    [Header("Salva Veritate Main Page Buttons")]
    [SerializeField] private Image _endlessFunButton;
    [SerializeField] private Image _christianSchoolButton;
    [SerializeField] private Image _aiChangingLifeButton;







    public void OnPointerClick(PointerEventData eventData)
    {
        //if the endless fun button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _endlessFunButton.gameObject)
        {
            Debug.Log("Endless Fun Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_endlessFunPage);
        }

        // Change the image on the main page to endless fun image
        else if (eventData.pointerCurrentRaycast.gameObject == _christianSchoolButton.gameObject)
        {
            Debug.Log("Christian School Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_christianSchoolPage);
        }

        //if the AI Changing Life button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _aiChangingLifeButton.gameObject)
        {
            Debug.Log("AI Changing Life Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_aiChangingLifePage);

            
        }
    }


   
}
