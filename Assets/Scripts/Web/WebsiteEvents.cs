using System;
using UnityEngine;

//A script that defines shared website events that multiple website behaviors can use.
//Provides a centralized event to notify when the active website page changes.
public static class WebsiteEvents
{
    //defines the delegate types
    public delegate void WebsiteEventHandler(GameObject newWebsitePage);
    public delegate void WebsiteImageEventHandler(Sprite interactedImage);


    //events that other scripts can subscribe to
    public static event WebsiteEventHandler OnWebsiteChange;
    public static event WebsiteImageEventHandler OnWebsiteImageChange;
   



    public static void RaiseWebsiteChange(GameObject newPage)
    {
        OnWebsiteChange?.Invoke(newPage);
    }

    public static void RaiseWebsiteImageChange(Sprite interactedImage)
    { 
        OnWebsiteImageChange?.Invoke(interactedImage);
    }
}
