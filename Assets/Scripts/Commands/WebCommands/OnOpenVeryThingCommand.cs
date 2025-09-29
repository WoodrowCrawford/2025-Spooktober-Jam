using UnityEngine;
using System;
using Naninovel;

public class OnOpenVeryThingCommand : Command
{
    public static event Action OnOpenVeryThing;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenVeryThing?.Invoke();
        return UniTask.CompletedTask;
    }

    
}
