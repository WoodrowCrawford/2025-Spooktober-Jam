using System;
using UnityEngine;

//A script that defines shared website events that multiple website behaviors can use.
//Provides a centralized event to notify when the active website page changes.
public static class WebsiteEvents
{
    //defines the delegate types
    public delegate void WebsiteEventHandler(GameObject newWebsitePage);
    public delegate void WebsiteImageEventHandler(Sprite interactedImage);

    public delegate void WebEventHandeler();


    //events that other scripts can subscribe to
    public static event WebsiteEventHandler OnWebsiteChange;
    public static event WebsiteImageEventHandler OnWebsiteImageChange;

    public static event WebEventHandeler OnPlayerClickedFavoritesButton;
    public static event WebEventHandeler OnPlayerClickedFoundThingPageButton;
    public static event WebEventHandeler OnPlayerClickedTheoryPageButton;
    public static event WebEventHandeler OnPlayerClickedGovernmentPageButton;
    public static event WebEventHandeler OnPlayerClickedClickPost1;
    public static event WebEventHandeler OnPlayerClickedClickPost2;
    public static event WebEventHandeler OnPlayerClickedClickPost3;




    public static void RaiseWebsiteChange(GameObject newPage)
    {
        OnWebsiteChange?.Invoke(newPage);
    }

    public static void RaiseWebsiteImageChange(Sprite interactedImage)
    {
        OnWebsiteImageChange?.Invoke(interactedImage);
    }

    public static void RaisePlayerClickedFavoritesButton()
    {
        OnPlayerClickedFavoritesButton?.Invoke();
    }

    public static void RaisePlayerClickedFoundThingPageButton()
    {
        OnPlayerClickedFoundThingPageButton?.Invoke();
    }

    public static void RaisePlayerClickedTheoryPageButton()
    {
        OnPlayerClickedTheoryPageButton?.Invoke();
    }

    public static void RaisePlayerClickedGovernmentPageButton()
    {
        OnPlayerClickedGovernmentPageButton?.Invoke();
    }

    public static void RaisePlayerClickedClickPost1()
    {
        OnPlayerClickedClickPost1?.Invoke();
    }

    public static void RaisePlayerClickedClickPost2()
    {
        OnPlayerClickedClickPost2?.Invoke();
    }

    public static void RaisePlayerClickedClickPost3()
    {
        OnPlayerClickedClickPost3?.Invoke();
    }
}
