using Meta.WitAi.TTS.Utilities;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private TTSSpeaker _speaker;
    
    // translates text to character speech
    public void Speak(string text)
    {
        _speaker.Speak(text);
    }
}
