using UnityEngine;
using Naninovel;
using System;

// A manager class for handling story-related logic.
public class StoryManagerBehavior : MonoBehaviour
{

    public delegate void StoryManagerHandler();
    public static event StoryManagerHandler OnPlayerReadAllArticles;

    private IScriptPlayer scriptPlayer;
    private ICharacterManager characterManager;
    private IStateManager stateManager;
    private ICustomVariableManager customVariableManager;


    //Chapter 2 variables
    public static bool HasReadAIChangingLifePage = false;
    public static bool HasReadChristianSchoolPage = false;
    public static bool HasReadEndlessFunPage = false;
    public static bool HasReadAllArticles = false;




    void OnEnable()
    {
        scriptPlayer = Engine.GetService<IScriptPlayer>();
        characterManager = Engine.GetService<ICharacterManager>();
        stateManager = Engine.GetService<IStateManager>();
        customVariableManager = Engine.GetService<ICustomVariableManager>();
    }


    void OnDisable()
    {
        scriptPlayer = null;
        characterManager = null;
        stateManager = null;
        customVariableManager = null;

    }



    public void Update()
    {
        CheckIfPlayerReadAllNewsSites();
    }



    public void CheckIfPlayerReadAllNewsSites()
    {

        if (customVariableManager.TryGetVariableValue<bool>("playerInteractedWithEndlessFun", out HasReadEndlessFunPage) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithChristianSchool", out HasReadChristianSchoolPage) &&
            customVariableManager.TryGetVariableValue<bool>("playerInteractedWithAIChangingLife", out HasReadAIChangingLifePage))
        {
            if (HasReadEndlessFunPage && HasReadChristianSchoolPage && HasReadAIChangingLifePage && !HasReadAllArticles)
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
}
