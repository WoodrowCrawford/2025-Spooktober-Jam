using UnityEngine;
using System;
using Naninovel;

public class OnChangeCallWindowToNormalCommand : Command
{
    public static event Action OnChangeCallWindowToNormal;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnChangeCallWindowToNormal?.Invoke();
        return UniTask.CompletedTask;
    }

}
