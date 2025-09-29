using UnityEngine;
using System;
using Naninovel;

public class OnOpenClickCommand : Command
{
    public static event Action OnOpenClick;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenClick?.Invoke();
        return UniTask.CompletedTask;
    }
}

   
