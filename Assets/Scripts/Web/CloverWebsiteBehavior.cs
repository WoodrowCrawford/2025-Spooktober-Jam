using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Naninovel;

public class CloverWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{
    public delegate void CloverWebsiteEventHandler();

    public static event CloverWebsiteEventHandler OnPlayerInteractedWithCloverPicture;

    
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



    private IScriptPlayer scriptPlayer;

    private void Awake()
    {
        scriptPlayer = Naninovel.Engine.GetService<IScriptPlayer>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if the clover image 1 button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _cloverImage1Button.gameObject)
        {
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage1);

            if (!StoryManagerBehavior.HasInteractedWithStatueWithCode)
            {
                //tell naninovel to go to the label where the player clicked on the statue picture
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Statue_Picture_Clicked");

                StoryManagerBehavior.HasInteractedWithStatueWithCode = true;
                OnPlayerInteractedWithCloverPicture?.Invoke();

            }

        }

        // Change the image on the main page to clover image 2
        else if (eventData.pointerCurrentRaycast.gameObject == _cloverImage2Button.gameObject)
        {
            Debug.Log("Clover Image 2 Button Clicked");
            WebsiteEvents.RaiseWebsiteImageChange(_cloverWebsiteImage2);

            if (!StoryManagerBehavior.HasInteractedWithNakedWomanPicture)
            {
                //tell naninovel to go to the label where the player clicked on the naked woman picture
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Naked_Woman_Picture_Clicked");
                StoryManagerBehavior.HasInteractedWithNakedWomanPicture = true;
                OnPlayerInteractedWithCloverPicture?.Invoke();
            }
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

            if (!StoryManagerBehavior.HasInteractedWithCreepyPeoplePicture)
            {
                //tell naninovel to go to the label where the player clicked on the creepy people picture
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Creepy_People_Picture_Clicked");

                StoryManagerBehavior.HasInteractedWithCreepyPeoplePicture = true;
                OnPlayerInteractedWithCloverPicture?.Invoke();
            }

            }
    }

}
