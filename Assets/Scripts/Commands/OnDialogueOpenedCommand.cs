using UnityEngine;
using Naninovel;
using System;

public class OnDialogueOpenedCommand : Command
{
    public static event Action OnDialogueOpened;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnDialogueOpened?.Invoke();
        return UniTask.CompletedTask;
    }
}
