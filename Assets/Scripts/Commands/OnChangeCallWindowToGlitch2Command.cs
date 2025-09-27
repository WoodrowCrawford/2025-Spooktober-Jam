using UnityEngine;
using System;
using Naninovel;

public class OnChangeCallWindowToGlitch2Command : Command
{
    public static event Action OnChangeCallWindowToGlitch2;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnChangeCallWindowToGlitch2?.Invoke();
        return UniTask.CompletedTask;
    }

    
}
