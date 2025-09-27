using UnityEngine;
using UnityEngine.UI;

public class CallWindowBehavior : MonoBehaviour
{

    [Header("Call Window Images")]
    [SerializeField] private Sprite _normalEffectImage;
    [SerializeField] private Sprite _glitch1EffectImage;
    [SerializeField] private Sprite _glitch2EffectImage;

    void OnEnable()
    {
        OnChangeCallWindowToNormalCommand.OnChangeCallWindowToNormal += ChangeToNormalEffect;
        OnChangeCallWindowToGlitch1Command.OnChangeCallWindowToGlitch1 += ChangeToGlitch1Effect;
        OnChangeCallWindowToGlitch2Command.OnChangeCallWindowToGlitch2 += ChangeToGlitch2Effect;
    }

    void OnDisable()
    {
        OnChangeCallWindowToNormalCommand.OnChangeCallWindowToNormal -= ChangeToNormalEffect;
        OnChangeCallWindowToGlitch1Command.OnChangeCallWindowToGlitch1 -= ChangeToGlitch1Effect;
        OnChangeCallWindowToGlitch2Command.OnChangeCallWindowToGlitch2 -= ChangeToGlitch2Effect;
    }

    public void ChangeToNormalEffect()
    {
        //change the call window to the normal effect
        GetComponent<Image>().sprite = _normalEffectImage;
    }

    public void ChangeToGlitch1Effect()
    {
        //change the call window to the glitch 1 effect
        GetComponent<Image>().sprite = _glitch1EffectImage;
    }

   
    
    public void ChangeToGlitch2Effect()
    {
        //change the call window to the glitch 2 effect
        GetComponent<Image>().sprite = _glitch2EffectImage;
    }   
}
