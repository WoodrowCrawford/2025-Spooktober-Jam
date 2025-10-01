using System;
using UnityEngine;

//A script that defines shared website events that multiple website behaviors can use.
//Provides a centralized event to notify when the active website page changes.
public static class WebsiteEvents
{
    //defines the delegate types
    public delegate void WebsiteEventHandler(GameObject newWebsitePage);
    public delegate void WebsiteImageEventHandler(Sprite interactedImage);

    public delegate void WebEventHandler();


    //events that other scripts can subscribe to
    public static event WebsiteEventHandler OnWebsiteChange;
    public static event WebsiteImageEventHandler OnWebsiteImageChange;

    public static event WebEventHandler OnPlayerClickedFavoritesButton;
    public static event WebEventHandler OnPlayerClickedFoundThingPageButton;
    public static event WebEventHandler OnPlayerClickedTheoryPageButton;
    public static event WebEventHandler OnPlayerClickedGovernmentPageButton;
    public static event WebEventHandler OnPlayerClickedClickPost1;
    public static event WebEventHandler OnPlayerClickedClickPost2;
    public static event WebEventHandler OnPlayerClickedClickPost3;



    //Salva Veritate events
    public static event WebEventHandler OnPlayerClickedEndlessFunButton;
    public static event WebEventHandler OnPlayerClickedChristianButton;
    public static event WebEventHandler OnPlayerClickedAIChangingLifeButton;



    //clover website events
    public static event WebEventHandler OnPlayerInteractedWithCloverPicture1;
    public static event WebEventHandler OnPlayerInteractedWithCloverPicture2;
    public static event WebEventHandler OnPlayerInteractedWithCloverPicture3;
    public static event WebEventHandler OnPlayerInteractedWithCloverPicture4;



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

    public static void RaisePlayerInteractedWithCloverPicture1()
    {
        OnPlayerInteractedWithCloverPicture1?.Invoke();
    }

    public static void RaisePlayerInteractedWithCloverPicture2()
    {
        OnPlayerInteractedWithCloverPicture2?.Invoke();
    }

    public static void RaisePlayerInteractedWithCloverPicture3()
    {
        OnPlayerInteractedWithCloverPicture3?.Invoke();
    }

    public static void RaisePlayerInteractedWithCloverPicture4()
    {
        OnPlayerInteractedWithCloverPicture4?.Invoke();
    }

    public static void RaisePlayerClickedEndlessFunButton()
    {
        OnPlayerClickedEndlessFunButton?.Invoke();
    }

    public static void RaisePlayerClickedChristianButton()
    {
        OnPlayerClickedChristianButton?.Invoke();
    }
    
    public static void RaisePlayerClickedAIChangingLifeButton()
    {
        OnPlayerClickedAIChangingLifeButton?.Invoke();
    }
}
