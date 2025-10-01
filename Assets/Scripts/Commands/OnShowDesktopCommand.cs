using UnityEngine;
using Naninovel;
using System;

public class OnShowDesktopCommand : Command
{
    public static event Action OnShowDesktop;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnShowDesktop?.Invoke();
        return UniTask.CompletedTask;
    }
}
