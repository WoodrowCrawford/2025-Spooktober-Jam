using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatterWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    // Define a delegate and event for website changes
    public delegate void ChatterWebsiteEventHandler(GameObject newWebsitePage);
    public static event ChatterWebsiteEventHandler OnWebsiteChange;


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
            //invoke the event to change the webpage to the picture 1 page
            OnWebsiteChange?.Invoke(_chatterMainPage);
            Debug.Log("Picture 1 Button Clicked");

            //show the zoomed in image
            _zoomedInImage.SetActive(true);

            //set the zoomed in image to the picture 1 sprite
            _zoomedInImage.GetComponent<Image>().sprite = _picture1;


        }

        else if (eventData.pointerCurrentRaycast.gameObject == _picture2Button.gameObject)
        {
            //invoke the event to change the webpage to the picture 2 page
            OnWebsiteChange?.Invoke(_chatterMainPage);
            Debug.Log("Picture 2 Button Clicked");

            //show the zoomed in image
            _zoomedInImage.SetActive(true);

            //set the zoomed in image to the picture 2 sprite
            _zoomedInImage.GetComponent<Image>().sprite = _picture2;
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _picture3Button.gameObject)
        {
            //invoke the event to change the webpage to the picture 3 page
            OnWebsiteChange?.Invoke(_chatterMainPage);
            Debug.Log("Picture 3 Button Clicked");

            //show the zoomed in image
            _zoomedInImage.SetActive(true);

            //set the zoomed in image to the picture 3 sprite
            _zoomedInImage.GetComponent<Image>().sprite = _picture3;
        }

    }
}
