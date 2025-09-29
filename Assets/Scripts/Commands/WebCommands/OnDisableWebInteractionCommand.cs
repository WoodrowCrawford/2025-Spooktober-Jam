using UnityEngine;
using Naninovel;
using System;

public class OnDisableWebInteractionCommand : Command
{
    public static event Action OnDisableWebInteraction;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnDisableWebInteraction?.Invoke();
        return UniTask.CompletedTask;
    }
}
