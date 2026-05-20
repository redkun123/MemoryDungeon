using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusPopupUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private CanvasGroup canvasGroup;

    public IEnumerator Play(Sprite sprite,Transform anchor)
    {
        icon.sprite = sprite;

        //Vector3 screenPos = Camera.main.WorldToScreenPoint(anchor.position);

        //transform.position = screenPos;

        transform.localScale = Vector3.one * 0.5f;

        canvasGroup.alpha = 1f;

        Sequence seq = DOTween.Sequence();

        seq.Append(
            transform.DOScale(5f, 2f)
        );

        seq.Join(
            canvasGroup.DOFade(0f, 2f)
        );

        yield return seq.WaitForCompletion();

        Destroy(gameObject);
    }
}