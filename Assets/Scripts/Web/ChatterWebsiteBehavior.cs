using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatterWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    // Define a delegate and event for website changes
    public delegate void ChatterWebsiteEventHandler(GameObject newWebsitePage);
    public delegate void ChatterWebsiteImageEventHandler(Sprite interactedImage);


    public static event ChatterWebsiteEventHandler OnWebsiteChange;
    public static event ChatterWebsiteImageEventHandler OnImageInteracted;


    [Header("Chatter pages")]
    [SerializeField] private GameObject _chatterMainPage;


    [Header("Chatter Buttons")]
    [SerializeField] private Image _picture1Button;
    [SerializeField] private Image _picture2Button;
    [SerializeField] private Image _picture3Button;


    [Header("Chatter Images")]
    [SerializeField] private Sprite _picture1;
    [SerializeField] private Sprite _picture2;
    [SerializeField] private Sprite _picture3;


    [Header("Zoom in Image Parameters")]
    [SerializeField] private GameObject _zoomedInImage;



    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle pointer click events here
        //if the picture 1 button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _picture1Button.gameObject)
        {
            //invoke the event to notify that an image has been interacted with
            OnImageInteracted?.Invoke(_picture1);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _picture2Button.gameObject)
        {
            //invoke the event to notify that an image has been interacted with
           OnImageInteracted?.Invoke(_picture2);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _picture3Button.gameObject)
        {
            //invoke the event to notify that an image has been interacted with
           OnImageInteracted?.Invoke(_picture3);
        }

    }
}
