using UnityEngine;
using System;
using Naninovel;

public class OnCloseFolder3AppCommand : Command
{
    public static event Action OnCloseFolder3App;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnCloseFolder3App?.Invoke();
        return UniTask.CompletedTask;
    }

    
}

    