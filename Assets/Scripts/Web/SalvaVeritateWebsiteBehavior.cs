using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Naninovel;
using System.Threading.Tasks;

public class SalvaVeritateWebsiteBehavior : MonoBehaviour, IPointerClickHandler
{

    private IScriptPlayer scriptPlayer;

    [Header("Salva Veritate pages")]
    [SerializeField] private GameObject _salvaVeritateMainPage;
    [SerializeField] private GameObject _endlessFunPage;
    [SerializeField] private GameObject _christianSchoolPage;
    [SerializeField] private GameObject _aiChangingLifePage;


    [Header("Salva Veritate Main Page Buttons")]
    [SerializeField] private Image _endlessFunButton;
    [SerializeField] private Image _christianSchoolButton;
    [SerializeField] private Image _aiChangingLifeButton;




    void OnEnable()
    {
        scriptPlayer = Engine.GetService<IScriptPlayer>();
    }

    void OnDisable()
    {
        scriptPlayer = null;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        //if the endless fun button is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _endlessFunButton.gameObject)
        {
            Debug.Log("Endless Fun Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_endlessFunPage);

            //start the endless fun dialogue if it hasn't played yet
            if (!StoryManagerBehavior.HasReadEndlessFunPage)
            {
                scriptPlayer.LoadAndPlay("Chapter2/EndlessFunDialogue");
            }
        }

        // if the Christian School button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _christianSchoolButton.gameObject)
        {
            Debug.Log("Christian School Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_christianSchoolPage);

            //start the new school dialogue if it hasn't played yet
            if (!StoryManagerBehavior.HasReadChristianSchoolPage)
            {
                scriptPlayer.LoadAndPlay("Chapter2/NewSchoolDialogue");
            
            }
        }

        //if the AI Changing Life button is clicked
        else if (eventData.pointerCurrentRaycast.gameObject == _aiChangingLifeButton.gameObject)
        {
            Debug.Log("AI Changing Life Button Clicked");
            WebsiteEvents.RaiseWebsiteChange(_aiChangingLifePage);

            //start the AI Changing Life dialogue if it hasn't played yet
            if (!StoryManagerBehavior.HasReadAIChangingLifePage)
            {
                scriptPlayer.LoadAndPlay("Chapter2/AIChangingLifeDialogue");
            }

        }
    }



}
