using UnityEngine;
using Naninovel;
using System;

public class OnCloseWebCommand : Command
{
public static event Action OnCloseWeb;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnCloseWeb?.Invoke();
        return UniTask.CompletedTask;
    }
}
