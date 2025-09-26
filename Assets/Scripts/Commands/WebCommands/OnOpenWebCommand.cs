using UnityEngine;
using Naninovel;
using System;

public class OnOpenWebCommand : Command
{
public static event Action OnOpenWeb;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenWeb?.Invoke();
        return UniTask.CompletedTask;
    }
}
