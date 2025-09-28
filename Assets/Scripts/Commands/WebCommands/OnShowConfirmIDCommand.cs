using UnityEngine;
using Naninovel;
using System;

public class OnShowConfirmIDCommand : Command
{
    public static event Action OnShowConfirmID;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnShowConfirmID?.Invoke();
        return UniTask.CompletedTask;
    }

   
}
