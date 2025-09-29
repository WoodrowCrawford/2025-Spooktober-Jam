using UnityEngine;
using System;
using Naninovel;

public class OnCaptchaOpenedCommand : Command
{
    public static event Action OnCaptchaOpened;
                    
    public override UniTask Execute(AsyncToken token = default)
    {
        OnCaptchaOpened?.Invoke();
        return UniTask.CompletedTask;
    }

}
