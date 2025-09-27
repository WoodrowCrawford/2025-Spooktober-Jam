using UnityEngine;
using Naninovel;
using System;

public class ChatIconTaskbarBehavior : MonoBehaviour
{
    [Header("Chat icons")]
    [SerializeField] private Sprite _unreadIcon;
    [SerializeField] private Sprite _newMessageIcon;

    private IAudioManager audioManager;



    void OnEnable()
    {
        audioManager = Engine.GetService<IAudioManager>();
        StoryManagerBehavior.OnPlayerReadAllArticles += ChangeToNewMessageIcon;
    }

    void OnDisable()
    {
        audioManager = null;
        StoryManagerBehavior.OnPlayerReadAllArticles -= ChangeToNewMessageIcon;
    }




    public void ChangeToUnreadIcon()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = _unreadIcon;
    }

    public void ChangeToNewMessageIcon()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = _newMessageIcon;
        audioManager.PlaySfx("Notification_sfx");
    }
}
