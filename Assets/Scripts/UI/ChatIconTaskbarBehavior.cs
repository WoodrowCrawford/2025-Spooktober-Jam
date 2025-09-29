using UnityEngine;
using Naninovel;
using System;

public class ChatIconTaskbarBehavior : MonoBehaviour
{
    [Header("Chat icons")]
    [SerializeField] private Sprite _unreadIcon;
    [SerializeField] private Sprite _newMessageIcon;

    public static bool NewMessageNotification = false;

    private IAudioManager audioManager;



    void OnEnable()
    {
        audioManager = Engine.GetService<IAudioManager>();
        StoryManagerBehavior.OnPlayerReadAllArticles += ChangeToNewMessageIcon;
        StoryManagerBehavior.OnStoryWantsToGiveChatIconNotification += ChangeToNewMessageIcon;
    }

    void OnDisable()
    {
        audioManager = null;
        StoryManagerBehavior.OnPlayerReadAllArticles -= ChangeToNewMessageIcon;
        StoryManagerBehavior.OnStoryWantsToGiveChatIconNotification -= ChangeToNewMessageIcon;
    }




    public void ChangeToUnreadIcon()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = _unreadIcon;
        NewMessageNotification = false;
    }

    public void ChangeToNewMessageIcon()
    {
        Debug.Log("Change the chat icon to new message icon");
        GetComponent<UnityEngine.UI.Image>().sprite = _newMessageIcon;
        audioManager.PlaySfx("Notification_sfx");
        NewMessageNotification = true;
        
    }
}
