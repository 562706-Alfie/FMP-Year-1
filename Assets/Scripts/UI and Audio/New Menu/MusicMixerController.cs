using UnityEngine;
using UnityEngine.Audio;

public class MusicMixerController : MonoBehaviour
{
    [SerializeField] private AudioMixer myAudioMixer;

    public void SetVolume(float sliderValue)
    {
        myAudioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
}