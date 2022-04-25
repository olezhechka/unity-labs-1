using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class XpBar : MonoBehaviour
{
    public float maxXp;
    public float currentXp;

    [SerializeField]
    private Image filledBar;
    [SerializeField]
    private Color filledBarColor;

    private RectTransform filledBarTransform;
    private float CurrentXpBarRatio
    {
        get
        {
            if (this.maxXp == 0)
            {
                return 0;
            }

            return this.currentXp / this.maxXp;
        }
    }

    void Start()
    {
        this.filledBar.color = this.filledBarColor;
        this.filledBarTransform = this.filledBar.GetComponent<RectTransform>();
    }

    void Update()
    {
        this.filledBar.color = this.filledBarColor;

        Vector3 newFilledBarScale = this.filledBarTransform.localScale;
        newFilledBarScale.x = this.CurrentXpBarRatio;

        this.filledBarTransform.localScale = newFilledBarScale;
    }
}