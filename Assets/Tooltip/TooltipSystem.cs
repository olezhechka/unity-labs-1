using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TooltipSystem : MonoBehaviour
{
    private static TooltipSystem current;

    [SerializeField]
    private Tooltip tooltip;

    public void Awake()
    {
        TooltipSystem.current = this;
    }

    public static void Show(TooltipSettings tooltipSettings)
    {
        TooltipSystem.current.tooltip.ApplySettings(tooltipSettings);
        TooltipSystem.current.tooltip.gameObject.SetActive(true);

        RectTransform rectTransform = TooltipSystem.current.tooltip.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = TooltipSystem.current.tooltip.GetComponent<CanvasGroup>();
        rectTransform.localScale = new Vector3(0, 0);

        Sequence showSequence = DOTween.Sequence();
        showSequence
            .Append(rectTransform.DOScale(1, 0.3f))
            .Join(canvasGroup.DOFade(1, 0.3f));
    }

    public static void Hide()
    {
        RectTransform rectTransform = TooltipSystem.current.tooltip.GetComponent<RectTransform>();
        CanvasGroup canvasGroup = TooltipSystem.current.tooltip.GetComponent<CanvasGroup>();

        Sequence hideSequence = DOTween.Sequence();
        hideSequence
            .Append(rectTransform.DOScale(0, 0.3f))
            .Join(canvasGroup.DOFade(0, 0.3f))
            .OnComplete(() =>
            {
                TooltipSystem.current.tooltip.gameObject.SetActive(false);
            });
    }
}