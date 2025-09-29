using UnityEngine;
using System;
using Naninovel;

public class OnShowGlitchOverlayCommand : Command
{
    public static event Action OnShowGlitchOverlay;
    public override UniTask Execute(AsyncToken token = default)
    {
        OnShowGlitchOverlay?.Invoke();
        return UniTask.CompletedTask;
    }
}
