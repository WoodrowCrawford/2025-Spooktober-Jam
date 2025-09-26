using UnityEngine;
using Naninovel;
using System;

public class OnOpenNewsCommand : Command
{
public static event Action OnOpenNews;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnOpenNews?.Invoke();
        return UniTask.CompletedTask;
    }
}
