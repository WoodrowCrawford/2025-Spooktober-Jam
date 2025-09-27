using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Folder3AppBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _exitButton;

    [SerializeField] private Color _hoverColor = Color.lightGray;


    [Header("Folder 3 Contents")]
    [SerializeField] private Image _southWingFile;
    [SerializeField] private Image _codeForXFile;
    [SerializeField] private Image _mapsFile;
    [SerializeField] private Image _maps4File;
    [SerializeField] private Image _notesFile;
    [SerializeField] private Image _pngFile;
    [SerializeField] private Image _servicePassFile;
    [SerializeField] private Image _filesHelpFile;
    [SerializeField] private Image _png49File;
    [SerializeField] private Image _listNamesFile;
    [SerializeField] private Image _png52File;
    [SerializeField] private Image _png53File;


    public void OnPointerClick(PointerEventData eventData)
    {
       if (eventData.pointerCurrentRaycast.gameObject == _exitButton.gameObject)
        {
            Debug.Log("Exit button clicked, close folder app");
            gameObject.SetActive(false);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
    }
}
