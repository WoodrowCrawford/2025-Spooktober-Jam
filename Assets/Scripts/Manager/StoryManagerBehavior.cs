using UnityEngine;
using Naninovel;
using System;
using Naninovel.Commands;

// A manager class for handling story-related logic.
public class StoryManagerBehavior : MonoBehaviour
{

    public delegate void StoryManagerHandler();
    public static event StoryManagerHandler OnPlayerReadAllArticles;
    public static event StoryManagerHandler OnPlayerCanDownloadSummerPhotos;
    public static event StoryManagerHandler OnPlayerCanClickFavoritesButton;
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


        CloverWebsiteBehavior.OnPlayerInteractedWithCloverPicture += CheckIfPlayerHasInteractedWithAllCloverPictures;

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

        CloverWebsiteBehavior.OnPlayerInteractedWithCloverPicture -= CheckIfPlayerHasInteractedWithAllCloverPictures;

        WebsiteEvents.OnPlayerClickedFavoritesButton -= PickWebsiteToOpen;
        WebsiteEvents.OnPlayerClickedFoundThingPageButton -= PlayChapter7VeryThingFoundPage;
        WebsiteEvents.OnPlayerClickedTheoryPageButton -= PlayChapter7VeryThingTheoryPage;
        WebsiteEvents.OnPlayerClickedGovernmentPageButton -= PlayChapter7VeryThingControlPage;
    }



    public void Update()
    {
        CheckIfPlayerReadAllNewsSites();
    }



    public void CheckIfPlayerReadAllNewsSites()
    {

        if (customVariableManager.TryGetVariableValue<bool>("playerInteractedWithEndlessFun", out HasReadEndlessFunPage) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithChristianSchool", out HasReadChristianSchoolPage) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithAIChangingLife", out HasReadAIChangingLifePage) &&
            customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (HasReadEndlessFunPage && HasReadChristianSchoolPage && HasReadAIChangingLifePage && !HasReadAllArticles && _currentChapter == 2)
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
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 3 && !DownloadedSummerPhotos)
            {
                //set the current task to downloading summer photos
                _currentTask = "Download summer photos from Archives";

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

    public void CheckIfPlayerHasInteractedWithAllCloverPictures()
    {
        if (HasInteractedWithStatueWithCode && HasInteractedWithNakedWomanPicture && HasInteractedWithCreepyPeoplePicture)
        {
            Debug.Log("Player has interacted with all clover pictures!");

            //Fire an event to let the favorites tab on the web site to be clickable
            OnPlayerCanClickFavoritesButton?.Invoke();

        }


    }


    public void PickWebsiteToOpen()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 4)
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
            else
            {
                Debug.Log("Player clicked favorites button but nothing happens");
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
            }
        }
    }

    public void PlayChapter7VeryThingControlPage()
    {
        if (customVariableManager.TryGetVariableValue<int>("currentChapter", out _currentChapter))
        {
            if (_currentChapter == 7)
            {
                Debug.Log("Play chapter 7 control page interlude");

                //play chapter 7 control page interlude
                scriptPlayer.LoadAndPlayAtLabel("Chapter7/Chapter7", "Label_VeryThing_Control_Page");
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
            }
        }
    }
}