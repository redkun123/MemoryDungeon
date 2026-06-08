using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicSetting : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TMP_Text musicVolumeText;

    private void Start()
    {
        float volume = AudioManager.Instance.GetMusicVolume();

        musicSlider.value = volume;
        UpdateVolumeText(volume);

        musicSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
    }

    private void OnDestroy()
    {
        if (musicSlider != null)
        {
            musicSlider.onValueChanged.RemoveListener(OnMusicVolumeChanged);
        }
    }

    private void OnMusicVolumeChanged(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
        UpdateVolumeText(value);
    }

    private void UpdateVolumeText(float volume)
    {
        int percent = Mathf.RoundToInt(volume * 100f);
        musicVolumeText.text = percent + "%";
    }
}
