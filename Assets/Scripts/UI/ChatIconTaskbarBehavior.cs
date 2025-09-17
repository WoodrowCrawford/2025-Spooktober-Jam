using UnityEngine;

public class ChatIconTaskbarBehavior : MonoBehaviour
{
    [Header("Chat icons")]
    [SerializeField] private Sprite _unreadIcon;
    [SerializeField] private Sprite _newMessageIcon;


    public void ChangeToUnreadIcon()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = _unreadIcon;
    }

    public void ChangeToNewMessageIcon()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = _newMessageIcon;
    }
}
