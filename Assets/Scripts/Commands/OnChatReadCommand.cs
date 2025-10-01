using UnityEngine;
using Naninovel;
using System;

public class OnChatReadCommand : Command
{
    public static event Action OnChatRead;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnChatRead?.Invoke();
        return UniTask.CompletedTask;
    }
}

