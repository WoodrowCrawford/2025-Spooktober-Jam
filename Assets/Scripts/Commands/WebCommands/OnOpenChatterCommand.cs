using UnityEngine;
using Naninovel;
using System;

public class OnOpenChatterCommand : Command
{
    public static event Action OnOpenChatter;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenChatter?.Invoke();
        return UniTask.CompletedTask;
    }

}
