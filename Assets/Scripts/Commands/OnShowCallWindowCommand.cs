using UnityEngine;
using Naninovel;
using System;

public class OnShowCallWindowCommand : Command
{   
    public static event Action OnShowCallWindow;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnShowCallWindow?.Invoke();
        return UniTask.CompletedTask;
    }
}
