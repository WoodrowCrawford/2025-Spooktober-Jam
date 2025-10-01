using UnityEngine;
using Naninovel;
using System;

public class OnHideDesktopCommand : Command
{
    public static event Action OnHideDesktop;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnHideDesktop?.Invoke();
        return UniTask.CompletedTask;
    }
}


