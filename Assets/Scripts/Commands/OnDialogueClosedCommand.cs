using UnityEngine;
using Naninovel;
using System;

public class OnDialogueClosedCommand : Command
{
    public static event Action OnDialogueClosed;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnDialogueClosed?.Invoke();
        return UniTask.CompletedTask;
    }
}

