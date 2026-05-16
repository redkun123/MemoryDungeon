using DG.Tweening;
using TMPro;
using UnityEngine;
public class DamageText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;

    public void Setup(int damage)
    {
        damageText.text = damage.ToString();

        RectTransform rect = GetComponent<RectTransform>();

        Vector2 randomOffset = new Vector2(
            Random.Range(-40f, 40f),
            Random.Range(-20f, 20f)
        );

        rect.anchoredPosition += randomOffset;

        Sequence seq = DOTween.Sequence();

        seq.Join(
            rect.DOAnchorPos(
                rect.anchoredPosition + Vector2.down * 80f,
                0.8f
            ).SetEase(Ease.OutCubic)
        );

        seq.Join(
            damageText.DOFade(0f, 0.8f)
        );

        seq.Join(
            transform.DOScale(1.2f, 0.15f)
                .SetLoops(2, LoopType.Yoyo)
        );

        seq.OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
}
