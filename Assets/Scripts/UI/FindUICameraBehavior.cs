using UnityEngine;


//A script that other objects can use to find the UI camera in the scene
public class FindUICameraBehavior : MonoBehaviour
{

    void OnEnable()
    {
        this.GetComponent<Canvas>().worldCamera = GameObject.Find("UICamera").GetComponent<Camera>();
    }
    void OnDisable()
    {
        this.GetComponent<Canvas>().worldCamera = null;
    }
}
