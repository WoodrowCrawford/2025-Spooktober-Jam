using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    [Header("Click pages")]
    [SerializeField] private GameObject _clickMainPage;


    [Header("Click Main Page Buttons")]
    [SerializeField] private Image _clickPost1Button;
    [SerializeField] private Image _clickPost2Button;
    [SerializeField] private Image _clickPost3Button;


    [Header("Click Website Images")]
    [SerializeField] private Sprite _clickPost1Image;
    [SerializeField] private Sprite _clickPost2Image;
    [SerializeField] private Sprite _clickPost3Image;


   

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the clover image 1 button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _clickPost1Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_clickPost1Image);
            WebsiteEvents.RaisePlayerClickedClickPost1();
        }

        // Change the image on the main page to clover image 1
        else if (eventData.pointerCurrentRaycast.gameObject == _clickPost2Button.gameObject)
        {
            Debug.Log("Clover Image 2 Button Clicked");
            WebsiteEvents.RaiseWebsiteImageChange(_clickPost2Image);
            WebsiteEvents.RaisePlayerClickedClickPost2();
        }

        //if the clover image 3 button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _clickPost3Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_clickPost3Image);
            WebsiteEvents.RaisePlayerClickedClickPost3();
        }
    }
}
