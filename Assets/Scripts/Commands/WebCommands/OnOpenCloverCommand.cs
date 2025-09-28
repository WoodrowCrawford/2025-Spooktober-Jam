using UnityEngine;
using Naninovel;
using System;

public class OnOpenCloverCommand : Command
{
    public static event Action OnOpenClover;
    
    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenClover?.Invoke();
        return UniTask.CompletedTask;
    }

    
}
