using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSpeedSetting : MonoBehaviour
{
    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_Text speedText;

    private readonly float[] speeds = { 1f, 2f, 3f, 4f };

    private const string PREF_KEY = "GameSpeedIndex";

    private void Start()
    {
        speedSlider.wholeNumbers = true;
        speedSlider.minValue = 0;
        speedSlider.maxValue = speeds.Length - 1;

        int savedIndex = PlayerPrefs.GetInt(PREF_KEY, 0);

        speedSlider.value = savedIndex;
        ApplySpeed(savedIndex);

        speedSlider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDestroy()
    {
        speedSlider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        int index = (int)value;

        ApplySpeed(index);

        PlayerPrefs.SetInt(PREF_KEY, index);
        PlayerPrefs.Save();
    }

    private void ApplySpeed(int index)
    {
        float speed = speeds[index];

        Time.timeScale = speed;

        if (speedText != null)
        {
            speedText.text = $"{speed:0}x";
        }
    }
}
