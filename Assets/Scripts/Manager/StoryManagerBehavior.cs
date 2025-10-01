using UnityEngine;
using Naninovel;
using System;
using Naninovel.Commands;
using System.Collections;

// A manager class for handling story-related logic.
public class StoryManagerBehavior : MonoBehaviour
{

    public delegate void StoryManagerHandler();
    public static event StoryManagerHandler OnPlayerReadAllArticles;
    public static event StoryManagerHandler OnPlayerCanDownloadSummerPhotos;
    public static event StoryManagerHandler OnStoryWantsToGiveChatIconNotification;

    private int _currentChapter;
    private string _currentTask;

    private IScriptPlayer scriptPlayer;
    private ICharacterManager characterManager;
    private IStateManager stateManager;
    private ICustomVariableManager customVariableManager;


    //Chapter 2 variables
    public static bool HasReadAIChangingLifePage = false;
    public static bool HasReadChristianSchoolPage = false;
    public static bool HasReadEndlessFunPage = false;
    public static bool HasReadAllArticles = false;

    //Chapter 3 variables
    public static bool HasInteractedWithFolder3Files = false;
    public static bool DownloadedSummerPhotos = false;

    //Chapter 4 variables
    public static bool HasInteractedWithStatueWithCode = false;
    public static bool HasInteractedWithNakedWomanPicture = false;
    public static bool HasInteractedWithCreepyPeoplePicture = false;


    //Chapter 5 variables
    public static bool PlayerIsContinueBrowsingChatter = false;

    //Chapter 7 variables
    public static bool PlayerHasInteractedWithVeryThingFoundPage = false;
    public static bool PlayerHasInteractedWithVeryThingTheoryPage = false;
    public static bool PlayerHasInteractedWithVeryThingControlPage = false;

    public static bool PlayerHasInteractedWithClickPost1 = false;
    public static bool PlayerHasInteractedWithClickPost2 = false;
    public static bool PlayerHasInteractedWithClickPost3 = false;



    void OnEnable()
    {
        scriptPlayer = Engine.GetService<IScriptPlayer>();
        characterManager = Engine.GetService<ICharacterManager>();
        stateManager = Engine.GetService<IStateManager>();
        customVariableManager = Engine.GetService<ICustomVariableManager>();


        DesktopManager.OnPopUpErrorClosed += CheckIfPlayerInteractedWithFolder3Files;
        DesktopManager.OnPlayerWantsToUploadIDPhoto += PickPhotoIDToUpload;
        DesktopManager.OnVerificationPopUpClosed += PlayPhotoIDDialogueCompleteLabel;

        IconBehavior.OnWebpageIconClicked += CheckIfPlayerCanHaveOptionToChangeWebsite;
        IconBehavior.OnPlayerWantsToDownloadSummerPhotos += CheckIfPlayerCanDownloadSummerPhotos;
        IconBehavior.OnChatIconClicked += StartChapter6;

        WebsiteEvents.OnPlayerClickedEndlessFunButton += PlayChapter2EndlessFunDialogue;
        WebsiteEvents.OnPlayerClickedChristianButton += PlayChapter2NewSchoolDialogue;
        WebsiteEvents.OnPlayerClickedAIChangingLifeButton += PlayChapter2AIChangingLifeDialogue;

        WebsiteEvents.OnPlayerInteractedWithCloverPicture1 += PlayChapter4LabelStatuePicture;
        WebsiteEvents.OnPlayerInteractedWithCloverPicture2 += PlayChapter4LabelNakedWomanPicture;
        WebsiteEvents.OnPlayerInteractedWithCloverPicture4 += PlayChapter4LabelCreepyPeoplePicture;

        WebsiteEvents.OnPlayerClickedFavoritesButton += PickWebsiteToOpen;
        WebsiteEvents.OnPlayerClickedFoundThingPageButton += PlayChapter7VeryThingFoundPage;
        WebsiteEvents.OnPlayerClickedTheoryPageButton += PlayChapter7VeryThingTheoryPage;
        WebsiteEvents.OnPlayerClickedGovernmentPageButton += PlayChapter7VeryThingControlPage;

        WebsiteEvents.OnPlayerClickedClickPost1 += PlayChapter7VeryThingClickPost1;
        WebsiteEvents.OnPlayerClickedClickPost2 += PlayChapter7VeryThingClickPost2;
        WebsiteEvents.OnPlayerClickedClickPost3 += PlayChapter7VeryThingClickPost3;

    }


    void OnDisable()
    {
        scriptPlayer = null;
        characterManager = null;
        stateManager = null;
        customVariableManager = null;

        DesktopManager.OnPopUpErrorClosed -= CheckIfPlayerInteractedWithFolder3Files;
        DesktopManager.OnPlayerWantsToUploadIDPhoto -= PickPhotoIDToUpload;
        DesktopManager.OnVerificationPopUpClosed -= PlayPhotoIDDialogueCompleteLabel;

        IconBehavior.OnWebpageIconClicked -= CheckIfPlayerCanHaveOptionToChangeWebsite;
        IconBehavior.OnPlayerWantsToDownloadSummerPhotos -= CheckIfPlayerCanDownloadSummerPhotos;
        IconBehavior.OnChatIconClicked -= StartChapter6;

        WebsiteEvents.OnPlayerClickedEndlessFunButton -= PlayChapter2EndlessFunDialogue;
        WebsiteEvents.OnPlayerClickedChristianButton -= PlayChapter2NewSchoolDialogue;
        WebsiteEvents.OnPlayerClickedAIChangingLifeButton -= PlayChapter2AIChangingLifeDialogue;

        WebsiteEvents.OnPlayerInteractedWithCloverPicture1 -= PlayChapter4LabelStatuePicture;
        WebsiteEvents.OnPlayerInteractedWithCloverPicture2 -= PlayChapter4LabelNakedWomanPicture;
        WebsiteEvents.OnPlayerInteractedWithCloverPicture4 -= PlayChapter4LabelCreepyPeoplePicture;


        WebsiteEvents.OnPlayerClickedFavoritesButton -= PickWebsiteToOpen;
        WebsiteEvents.OnPlayerClickedFoundThingPageButton -= PlayChapter7VeryThingFoundPage;
        WebsiteEvents.OnPlayerClickedTheoryPageButton -= PlayChapter7VeryThingTheoryPage;
        WebsiteEvents.OnPlayerClickedGovernmentPageButton -= PlayChapter7VeryThingControlPage;
    }



    void Update()
    {
        CheckIfPlayerReadAllNewsSites();
    }



    public void CheckIfPlayerReadAllNewsSites()
    {

        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithEndlessFun", out bool playerInteractedWithEndlessFun) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithChristianSchool", out bool playerInteractedWithChristianSchool) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithAIChangingLife", out bool playerInteractedWithAIChangingLife))
        {
            if (HasReadEndlessFunPage && HasReadChristianSchoolPage && HasReadAIChangingLifePage && _currentChapter == 2 && !scriptPlayer.Playing)
            {
                Debug.Log("Player has read all news articles!");

                // Trigger the event for reading all articles
                HasReadAllArticles = true;

                OnPlayerReadAllArticles?.Invoke();

                //play chapter 3 interlude 1
                scriptPlayer.LoadAndPlay("Chapter3/Interlude1");
            }
            return;
        }
    }

    public void PlayChapter2EndlessFunDialogue()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (!HasReadEndlessFunPage)
            {
                Debug.Log("Play chapter 2 endless fun dialogue");

                //play chapter 2 endless fun dialogue
                scriptPlayer.LoadAndPlay("Chapter2/EndlessFunDialogue");
                HasReadEndlessFunPage = true;

            }
        }
    }

    public void PlayChapter2NewSchoolDialogue()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (!HasReadChristianSchoolPage)
            {
                Debug.Log("Play chapter 2 new school dialogue");

                //play chapter 2 new school dialogue
                scriptPlayer.LoadAndPlay("Chapter2/NewSchoolDialogue");
                HasReadChristianSchoolPage = true;

            }
        }
    }

    public void PlayChapter2AIChangingLifeDialogue()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (!HasReadAIChangingLifePage)
            {
                Debug.Log("Play chapter 2 AI changing life dialogue");

                //play chapter 2 AI changing life dialogue
                scriptPlayer.LoadAndPlay("Chapter2/AIChangingLifeDialogue");
                HasReadAIChangingLifePage = true;

            }
        }
    }


    public void CheckIfPlayerInteractedWithFolder3Files()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter) &&
            customVariableManager.TryGetVariableValue<string>("currentTask", out _currentTask))
        {
            if (_currentChapter == 3 && !HasInteractedWithFolder3Files && _currentTask == "Interact with folder files" && Folder3AppBehavior.HasInteractedWithFolder3Files)
            {
                Debug.Log("Player has interacted with folder files in chapter 3!");
                scriptPlayer.LoadAndPlayAtLabel("Chapter3/Interlude1", "Start_Archives_Section");
                HasInteractedWithFolder3Files = true;
            }
        }
    }

    public void CheckIfPlayerCanDownloadSummerPhotos()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter) &&
            customVariableManager.TryGetVariableValue<string>("currentTask", out _currentTask))
        {
            if (_currentChapter == 3 && !DownloadedSummerPhotos && _currentTask == "Download summer photos" && HasInteractedWithFolder3Files)
            {

                Debug.Log("Player can now download summer photos from Archives!");

                //fire an event to show the download question pop up
                OnPlayerCanDownloadSummerPhotos?.Invoke();

            }
            else
            {
                Debug.Log("Player cannot download summer photos yet.");
            }
        }
    }

    public void CheckIfPlayerCanHaveOptionToChangeWebsite()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 3 && _currentTask == "Download summer photos from Archives")
            {
                //play the dialogue where the player can choose to change website
                scriptPlayer.LoadAndPlayAtLabel("Chapter3/Interlude1", "Pick_Website_Section");
            }
        }
    }







    public void PickWebsiteToOpen()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 1)
            {
                //play the dialogue where the player can choose to change website
                Debug.Log("Player clicked favorites button in chapter 1, can not change website");
            }

            else if (_currentChapter == 2)
            {
                //play the dialogue where the player can choose to change website
                Debug.Log("Player clicked favorites button in chapter 2, can not change website");
            }

            else if (_currentChapter == 3)
            {
                //play the dialogue where the player can choose to change website
                Debug.Log("Player clicked favorites button in chapter 3, can not change website");
            }


            else if (_currentChapter == 4)
            {
                //play the dialogue where the player can choose to change website
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Pick_Favorite_Website_Section");
            }

            else if (_currentChapter == 5)
            {

                Debug.Log("Player clicked favorites button in chapter 5, give chat icon notification");
                //fire an event to give the chat icon a notification
                OnStoryWantsToGiveChatIconNotification?.Invoke();



                customVariableManager.SetVariableValue("currentTask", new("Read notification from chat app"));

            }

            else if (_currentChapter == 7)
            {
                //play the dialogue where the player can choose to change website
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Pick_Favorite_Website_Section_7");
            }

        }
    }

    public void PickPhotoIDToUpload()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            //play the dialogue where the player can choose to change website
            scriptPlayer.LoadAndPlayAtLabel("Chapter5/Chapter5", "Upload_ID_Photo_Section");
        }
    }

    public void PlayPhotoIDDialogueCompleteLabel()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 5)
            {
                //play the dialogue where the player can choose to change website
                scriptPlayer.LoadAndPlayAtLabel("Chapter5/Chapter5", "After_Uploading_ID_Photo");
            }
        }
    }

    //Chapter 4 functions
    public void PlayChapter4LabelStatuePicture()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 4 && !HasInteractedWithStatueWithCode)
            {
                Debug.Log("Play chapter 4 statue picture interlude");

                //play chapter 4 statue picture interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Statue_Picture_Clicked");
                HasInteractedWithStatueWithCode = true;
            }
        }
    }

    public void PlayChapter4LabelNakedWomanPicture()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 4 && !HasInteractedWithNakedWomanPicture)
            {
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Naked_Woman_Picture_Clicked");
                HasInteractedWithNakedWomanPicture = true;

            }
        }
    }

    public void PlayChapter4LabelCreepyPeoplePicture()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 4 && !HasInteractedWithCreepyPeoplePicture)
            {
                scriptPlayer.LoadAndPlayAtLabel("Chapter4/Chapter4", "Label_Creepy_People_Picture_Clicked");
                HasInteractedWithCreepyPeoplePicture = true;

            }
        }
    }




    public void StartChapter6()
    {
        //check if the player is in chapter 5 and the task is to read notification
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter) &&
            customVariableManager.TryGetVariableValue<string>("currentTask", out _currentTask))
        {
            if (_currentChapter == 5 && _currentTask == "Read notification from chat app")
            {
                Debug.Log("Player clicked chat icon in chapter 5, start chapter 6");

                //play chapter 6
                scriptPlayer.LoadAndPlay("Chapter6/Chapter6");

            }
        }
    }

    public void PlayChapter7VeryThingFoundPage()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithVeryThingFoundPage)
            {
                Debug.Log("Play chapter 7 found thing interlude");

                //play chapter 7 found thing interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Found_Thing_Page");
                PlayerHasInteractedWithVeryThingFoundPage = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }


        }
    }

    public void PlayChapter7VeryThingTheoryPage()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithVeryThingTheoryPage)
            {
                Debug.Log("Play chapter 7 theory page interlude");

                //play chapter 7 theory page interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Theory_Page");
                PlayerHasInteractedWithVeryThingTheoryPage = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }
        }
    }

    public void PlayChapter7VeryThingControlPage()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithVeryThingControlPage)
            {
                Debug.Log("Play chapter 7 control page interlude");

                //play chapter 7 control page interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Control_Page");
                PlayerHasInteractedWithVeryThingControlPage = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }
        }
    }


    public void PlayChapter7VeryThingClickPost1()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithClickPost1)
            {
                Debug.Log("Play chapter 7 click post 1 interlude");

                //play chapter 7 click post 1 interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Click_Post_1");
                PlayerHasInteractedWithClickPost1 = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }
        }
    }

    public void PlayChapter7VeryThingClickPost2()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithClickPost2)
            {
                Debug.Log("Play chapter 7 click post 2 interlude");

                //play chapter 7 click post 2 interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Click_Post_2");
                PlayerHasInteractedWithClickPost2 = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }
        }
    }

    public void PlayChapter7VeryThingClickPost3()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7 && !PlayerHasInteractedWithClickPost3)
            {
                Debug.Log("Play chapter 7 click post 3 interlude");

                //play chapter 7 click post 3 interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Click_Post_3");
                PlayerHasInteractedWithClickPost3 = true;

                StartCoroutine(CheckWhichEndingToPlay());
            }
        }
    }

    
    public IEnumerator CheckWhichEndingToPlay()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter) &&
            customVariableManager.TryGetVariableValue<bool>("playerHasPoliceEnding", out bool playerHasPoliceEnding) &&
            customVariableManager.TryGetVariableValue<int>("badChoiceCounter", out int badChoiceCounter))
        {
            if (_currentChapter == 7 && PlayerHasInteractedWithClickPost1 && PlayerHasInteractedWithClickPost2 && PlayerHasInteractedWithClickPost3
             && PlayerHasInteractedWithVeryThingFoundPage && PlayerHasInteractedWithVeryThingTheoryPage && PlayerHasInteractedWithVeryThingControlPage)
            {
                yield return new WaitUntil(() => !scriptPlayer.Playing);

                //if the player has police ending variable is true, play police ending
                if (playerHasPoliceEnding)
                {
                    yield return new WaitUntil(() => !scriptPlayer.Playing);
                    Debug.Log("Play police ending");
                    
                    scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_Police_Ending");
                }
                else if (badChoiceCounter <= 6)
                {
                    yield return new WaitUntil(() => !scriptPlayer.Playing);
                    Debug.Log("Play bad ending");

                }
                else
                {
                    yield return new WaitUntil(() => !scriptPlayer.Playing);
                    Debug.Log("Play good ending");

                }
            }

            else
            {
                yield return null;
            }
        }
    }
}