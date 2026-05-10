using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpText;

    [SerializeField] private float tweenDuration = 0.6f;

    private Tween hpTween;

    private int displayedHP;

    public void InitSet(int currentHP, int maxHP)
    {
        hpBar.maxValue = maxHP;
        hpBar.minValue = 0;

        hpBar.value = currentHP;

        displayedHP = currentHP;

        hpText.text = $"{currentHP}/{maxHP}";
    }

    public void Set(int currentHP, int maxHP)
    {
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);

        hpTween?.Kill();

        float startHP = hpBar.value;

        hpTween = DOTween.To(
            () => startHP,
            x =>
            {
                hpBar.value = x;

                displayedHP = Mathf.RoundToInt(x);

                hpText.text = $"{displayedHP}/{maxHP}";
            },
            currentHP,
            tweenDuration
        )
        .SetEase(Ease.OutCubic);
    }
}