using UnityEngine;
using System;
using Naninovel;

public class OnChangeCallWindowToGlitch1Command : Command
{
    public static event Action OnChangeCallWindowToGlitch1;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnChangeCallWindowToGlitch1?.Invoke();
        return UniTask.CompletedTask;
    }
}
