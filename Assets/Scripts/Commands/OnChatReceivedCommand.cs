using UnityEngine;
using System;
using Naninovel;

public class OnChatReceivedCommand : Command
{
    public static event Action OnChatReceived;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnChatReceived?.Invoke();
        return UniTask.CompletedTask;
    }
}
