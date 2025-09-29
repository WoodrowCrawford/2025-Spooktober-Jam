using UnityEngine;
using Naninovel;
using System;

public class OnCaptcha2ClosedCommand : Command
{
    public static event Action OnCaptcha2Closed;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnCaptcha2Closed?.Invoke();
        return UniTask.CompletedTask;
    }
}
