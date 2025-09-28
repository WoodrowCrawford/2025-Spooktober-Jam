using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummerPhotosFolderBehavior : MonoBehaviour, IPointerClickHandler
{
    public delegate void SummerPhotosEventHandler();
    public static event SummerPhotosEventHandler OnPasswordCorrect;

    [SerializeField] private String _attemptedPassword;
    [SerializeField] private String _password;
    [SerializeField] private Image _exitButton;

    public static bool IsUnlocked = false;



    public void OnPointerClick(PointerEventData eventData)
    {
        //if the exit image is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _exitButton.gameObject)
        {
            Debug.Log("Exit button clicked, close summer photos folder app");
            gameObject.SetActive(false);
        }
    }

    public void SetPassword(string newPassword)
    {
        _password = newPassword;
    }

    public void ReadAttemptedPassword(string attemptedPassword)
    {
        _attemptedPassword = attemptedPassword;
        Debug.Log("Attempted password set to: " + _attemptedPassword);
    }
    public void CheckPassword()
    {
        if (_attemptedPassword == _password)
        {
            Debug.Log("Password correct, unlock summer photos");
            OnPasswordCorrect?.Invoke();
            IsUnlocked = true;

        }
        else
        {
            Debug.Log("Password incorrect, show error message");
            IsUnlocked = false;

        }
    }

   
}
