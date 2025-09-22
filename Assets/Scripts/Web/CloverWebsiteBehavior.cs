using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CloverWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    
    [Header("Clover pages")]
    [SerializeField] private GameObject _cloverMainPage;


    [Header("Clover Main Page Buttons")]
    [SerializeField] private Image _cloverImage1Button;
    [SerializeField] private Image _cloverImage2Button;
    [SerializeField] private Image _cloverImage3Button;
    [SerializeField] private Image _cloverImage4Button;


    [Header("Clover Website Images")]
    [SerializeField] private Sprite _cloverWebsiteImage1;
    [SerializeField] private Sprite _cloverWebsiteImage2;
    [SerializeField] private Sprite _cloverWebsiteImage3;
    [SerializeField] private Sprite _cloverWebsiteImage4;


   

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the clover image 1 button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _cloverImage1Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage1);
        }

        // Change the image on the main page to clover image 1
        else if (eventData.pointerCurrentRaycast.gameObject == _cloverImage2Button.gameObject)
        {
            Debug.Log("Clover Image 2 Button Clicked");
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage2);
        }

        //if the clover image 3 button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _cloverImage3Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage3);
        }

        //if the clover image 4 button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _cloverImage4Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage4);
        }
    }

}
