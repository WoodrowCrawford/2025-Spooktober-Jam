using UnityEngine;
using Naninovel;
using System;

public class OnHideCallWindowCommand : Command
{
    public static event Action OnHideCallWindow;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnHideCallWindow?.Invoke();
        return UniTask.CompletedTask;
    }

    
}
