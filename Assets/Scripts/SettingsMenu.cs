using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;    


public void SetMusicVolume()
{
    float volume = musicSlider.value;
    myMixer.SetFloat("music", volume);
}
}
