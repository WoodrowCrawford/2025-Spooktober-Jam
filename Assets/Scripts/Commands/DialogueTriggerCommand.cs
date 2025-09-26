using UnityEngine;
using System;
using Naninovel;
using Unity.VisualScripting;


public class DialogueTriggerCommand : Command
{

    public override UniTask Execute(AsyncToken token = default)
    {
        throw new NotImplementedException();
    }

    public static async void StartDialogue(string dialogueScriptName)
    {
        var player = Engine.GetService<IScriptPlayer>();
        await player.LoadAndPlay(dialogueScriptName);

    }
}
