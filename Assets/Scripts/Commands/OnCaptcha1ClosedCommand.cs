using UnityEngine;
using Naninovel;
using System;

public class OnCaptcha1ClosedCommand : Command
{
    public static event Action OnCaptcha1Closed;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnCaptcha1Closed?.Invoke();
        return UniTask.CompletedTask;
    }
}
