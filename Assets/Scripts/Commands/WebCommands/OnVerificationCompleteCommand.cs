using UnityEngine;
using Naninovel;
using System;

public class OnVerificationCompleteCommand : Command
{
    public static event Action OnVerificationComplete;

    public override UniTask Execute(AsyncToken token = default)
    {
        OnVerificationComplete?.Invoke();
        return UniTask.CompletedTask;
    }
}


