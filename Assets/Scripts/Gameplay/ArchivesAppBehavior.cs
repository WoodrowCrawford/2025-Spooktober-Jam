using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ArchivesAppBehavior : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _summerPhotos;
    [SerializeField] private Image _exitButton;
    public void OnPointerClick(PointerEventData eventData)
    {
        // Handle the click event
        if (eventData.pointerCurrentRaycast.gameObject == _exitButton.gameObject)
        {
            // Close the archives app
            CloseArchivesApp();
        }

    }

    private void CloseArchivesApp()
    {
        // Implement the logic to close the archives app
        gameObject.SetActive(false);
    }
}
