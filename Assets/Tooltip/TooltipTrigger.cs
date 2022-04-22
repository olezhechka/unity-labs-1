using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string text;

    [SerializeField]
    private int offsetX;
    [SerializeField]
    private int offsetY;
    [SerializeField]
    private int offsetZ;

    [SerializeField]
    private int[] maxCharsPerLine = new int[] {80};

    private RectTransform target;

    public void Awake()
    {
        this.target = this.GetComponent<RectTransform>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipSystem.Show(new TooltipSettings
        {
            text = this.text,

            offsetX = this.offsetX,
            offsetY = this.offsetY,
            offsetZ = this.offsetZ,

            maxCharsPerLine = this.maxCharsPerLine,

            target = this.target
        });
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.Hide();
    }
}