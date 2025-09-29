using UnityEngine;
using Naninovel;
using System;

public class OnCaptcha2OpenedCommand : Command
{
    public static event Action OnCaptcha2Opened;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnCaptcha2Opened?.Invoke();
        return UniTask.CompletedTask;
    }
}
