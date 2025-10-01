using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SummerPhotosFolderBehavior : MonoBehaviour, IPointerClickHandler
{
    public delegate void SummerPhotosEventHandler();
    public static event SummerPhotosEventHandler OnPasswordsUnlocked;
    public static event SummerPhotosEventHandler OnPlayerWantsToDownloadSecret;

    [SerializeField] private String _attemptedPassword;

    [Header("Sprites")]
    [SerializeField] private Sprite _summerPhotosFolderLockedSprite;
    [SerializeField] private Sprite _summerPhotosFolderUnlockedSprite;
    [SerializeField] private Sprite _correctPasswordSprite;


    [Header("Code Fields")]
    [SerializeField] private GameObject _cloverCodeInputField;
    [SerializeField] private GameObject _chatterCodeInputField;
    [SerializeField] private GameObject _verythingCodeInputField;
    [SerializeField] private GameObject _clickCodeInputField;

    [Header("Codes")]
    [SerializeField] private String _cloverCode;
    [SerializeField] private String _chatterCode;
    [SerializeField] private String _verythingCode;
    [SerializeField] private String _clickCode;
    [SerializeField] private Image _exitButton;
    [SerializeField] private Image _downloadButton;




    public static bool CloverCodeUnlocked = false;
    public static bool ChatterCodeUnlocked = false;
    public static bool EverythingCodeUnlocked = false;
    public static bool ClickCodeUnlocked = false;

    public static bool AllUnlocked = false;





    public void OnPointerClick(PointerEventData eventData)
    {
        //if the exit image is clicked
        if (eventData.pointerCurrentRaycast.gameObject == _exitButton.gameObject)
        {
            Debug.Log("Exit button clicked, close summer photos folder app");
            gameObject.SetActive(false);
        }

        else if (eventData.pointerCurrentRaycast.gameObject == _downloadButton.gameObject && AllUnlocked)
        {
            //fire event to show pop up event
            Debug.Log("Download button clicked, raise event to show download complete pop up");
            OnPlayerWantsToDownloadSecret?.Invoke();
                
        }
    }





    public void ReadAttemptedPassword(string attemptedPassword)
    {
        _attemptedPassword = attemptedPassword;
        Debug.Log("Attempted password set to: " + _attemptedPassword);
        CheckPassword();
    }
    
    public void CheckPassword()
    {

        //if the current input field is the clover code input field
        if (_cloverCodeInputField.GetComponent<TMP_InputField>().isFocused)
        {
            if (_attemptedPassword == _cloverCode)
            {
                Debug.Log("Clover code correct, unlock summer photos folder");
                CloverCodeUnlocked = true;

                _cloverCodeInputField.GetComponent<Image>().sprite = _correctPasswordSprite;

                //make the input field not interactable
                _cloverCodeInputField.GetComponent<TMP_InputField>().interactable = false;
                CheckIfAllCodesUnlocked();
            }

        }
        else if (_chatterCodeInputField.GetComponent<TMP_InputField>().isFocused)
        {
            if (_attemptedPassword == _chatterCode)
            {
                Debug.Log("Chatter code correct, unlock summer photos folder");
                ChatterCodeUnlocked = true;

                _chatterCodeInputField.GetComponent<Image>().sprite = _correctPasswordSprite;
                _chatterCodeInputField.GetComponent<TMP_InputField>().interactable = false;
                CheckIfAllCodesUnlocked();
            }
            else
            {
                Debug.Log("Chatter code incorrect, try again");
            }
        }
        else if (_verythingCodeInputField.GetComponent<TMP_InputField>().isFocused)
        {
            if (_attemptedPassword == _verythingCode)
            {
                Debug.Log("Everything code correct, unlock summer photos folder");
                EverythingCodeUnlocked = true;

                _verythingCodeInputField.GetComponent<Image>().sprite = _correctPasswordSprite;
                _verythingCodeInputField.GetComponent<TMP_InputField>().interactable = false;
                CheckIfAllCodesUnlocked();
            }
            else
            {
                Debug.Log("Everything code incorrect, try again");
            }
        }
        else if (_clickCodeInputField.GetComponent<TMP_InputField>().isFocused)
        {
            if (_attemptedPassword == _clickCode)
            {
                Debug.Log("Click code correct, unlock summer photos folder");
                ClickCodeUnlocked = true;
                _clickCodeInputField.GetComponent<Image>().sprite = _correctPasswordSprite;
                _clickCodeInputField.GetComponent<TMP_InputField>().interactable = false;

                CheckIfAllCodesUnlocked();
            }
            else
            {
                Debug.Log("Click code incorrect, try again");
            }
        }
    }
    

    public void CheckIfAllCodesUnlocked()
    {
        if (CloverCodeUnlocked && ChatterCodeUnlocked && EverythingCodeUnlocked && ClickCodeUnlocked)
        {
            Debug.Log("All codes unlocked, raise event to notify story manager");
            OnPasswordsUnlocked?.Invoke();
            AllUnlocked = true;
            this.GetComponent<Image>().sprite = _summerPhotosFolderUnlockedSprite;

            //if all codes are unlocked, make hide all input fields
            _cloverCodeInputField.SetActive(false);
            _chatterCodeInputField.SetActive(false);
            _verythingCodeInputField.SetActive(false);
            _clickCodeInputField.SetActive(false);

            
            
        }
    }
}
