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




    void OnEnable()
    {
        scriptPlayer = Engine.GetService<IScriptPlayer>();
        characterManager = Engine.GetService<ICharacterManager>();
        stateManager = Engine.GetService<IStateManager>();
        customVariableManager = Engine.GetService<ICustomVariableManager>();

        DesktopManager.OnPopUpErrorClosed += CheckIfPlayerInteractedWithFolder3Files;

        IconBehavior.OnWebpageIconClicked += CheckIfPlayerCanHaveOptionToChangeWebsite;
        IconBehavior.OnPlayerWantsToDownloadSummerPhotos += CheckIfPlayerCanDownloadSummerPhotos;

        CloverWebsiteBehavior.OnPlayerInteractedWithCloverPicture += CheckIfPlayerHasInteractedWithAllCloverPictures;

        WebsiteEvents.OnPlayerClickedFavoritesButton += PickWebsiteToOpen;

    }


    void OnDisable()
    {
        scriptPlayer = null;
        characterManager = null;
        stateManager = null;
        customVariableManager = null;

        DesktopManager.OnPopUpErrorClosed -= CheckIfPlayerInteractedWithFolder3Files;

        IconBehavior.OnWebpageIconClicked -= CheckIfPlayerCanHaveOptionToChangeWebsite;
        IconBehavior.OnPlayerWantsToDownloadSummerPhotos -= CheckIfPlayerCanDownloadSummerPhotos;

        CloverWebsiteBehavior.OnPlayerInteractedWithCloverPicture -= CheckIfPlayerHasInteractedWithAllCloverPictures;
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
        }
    }
}

