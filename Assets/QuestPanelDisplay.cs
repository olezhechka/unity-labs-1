using DG.Tweening;
using UnityEngine;

public class QuestPanelDisplay : MonoBehaviour
{
    [SerializeField]
    private RectTransform backdrop;
    [SerializeField]
    private RectTransform toggleVisibilityButton;
    [SerializeField]
    private RectTransform arrow;

    [SerializeField]
    private float closedStateButtonGap = 30f;
    [SerializeField]
    private float closedStateStillVisibleBackdropGap = 15f;

    [SerializeField]
    private float cardAnimMoveToHidePointDuration = 0.5f;
    [SerializeField]
    private float cardAnimMoveHiddenPointDuration = 0.2f;
    [SerializeField]
    private float cardAnimMoveToVisiblePointDuration = 0.5f;
    [SerializeField]
    private float cardAnimMoveToStartPointDuration = 0.2f;

    private bool _isClosed = false;
    private bool IsClosed
    {
        get => this._isClosed;
        set
        {
            if (this._isClosed)
            {
                this.ShowQuestPanel();
            }
            else
            {
                this.HideQuestPanel();
            }

            this._isClosed = value;
        }
    }

    public void ToggleQuestPanelVisibility()
    {
        this.IsClosed = !this.IsClosed;
    }

    private void ShowQuestPanel()
    {
        Sequence showSequence = DOTween.Sequence();

        float backdropWidth = this.backdrop.rect.width;

        float firstPhaseBackdropX = this.closedStateStillVisibleBackdropGap + this.closedStateButtonGap - backdropWidth;
        float secondPhaseBackdropX = 0;
        float buttonX = backdropWidth;

        showSequence
            .Append(this.backdrop.DOMoveX(firstPhaseBackdropX, this.cardAnimMoveToVisiblePointDuration))
            .Append(this.backdrop.DOMoveX(secondPhaseBackdropX, this.cardAnimMoveToStartPointDuration))
            .Join(this.toggleVisibilityButton.DOMoveX(buttonX, this.cardAnimMoveToStartPointDuration))
            .Join(this.arrow.DORotate(new Vector3(0, 0, 0), this.cardAnimMoveToHidePointDuration, RotateMode.FastBeyond360));
    }

    private void HideQuestPanel()
    {
        Sequence hideSequence = DOTween.Sequence();

        float backdropWidth = this.backdrop.rect.width;

        float buttonX = this.closedStateStillVisibleBackdropGap + this.closedStateButtonGap;
        float firstPhaseBackdropX = buttonX - backdropWidth;
        float secondPhaseBackdropX = -1* backdropWidth + this.closedStateStillVisibleBackdropGap;

        hideSequence
            .Append(this.backdrop.DOMoveX(firstPhaseBackdropX, this.cardAnimMoveToHidePointDuration))
            .Join(this.toggleVisibilityButton.DOMoveX(buttonX, this.cardAnimMoveToHidePointDuration))
            .Join(this.arrow.DORotate(new Vector3(0, 0, 180), this.cardAnimMoveToHidePointDuration, RotateMode.FastBeyond360))
            .Append(this.backdrop.DOMoveX(secondPhaseBackdropX, this.cardAnimMoveToHidePointDuration));
    }
}
