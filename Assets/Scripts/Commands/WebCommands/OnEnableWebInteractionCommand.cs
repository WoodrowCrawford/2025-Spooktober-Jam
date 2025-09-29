using UnityEngine;
using System;
using Naninovel;

public class OnEnableWebInteractionCommand : Command
{
    public static event Action OnEnableWebInteraction;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnEnableWebInteraction?.Invoke();
        return UniTask.CompletedTask;
    }
}
