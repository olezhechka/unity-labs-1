using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EndlessTranslateModifying : MonoBehaviour
{
    public float scaleModificationSpeed = 0.2f;
    public float scaleUpperBound = 3f;
    public float scaleLowerBound = 0.3f;

    public float rotationModificationSpeed = 20f;

    private float _xScale = 1f;
    private float _yScale = 1f;
    private float _zScale = 1f;

    private bool _shouldIncreaseXScale = false;
    private bool _shouldIncreaseYScale = false;
    private bool _shouldIncreaseZScale = false;

    void Start()
    {
    }

    void Update()
    {
        this.UpdateScale();
        this.UpdateRotation();
    }

    private void UpdateScale()
    {
        this.ComputeNewScale(ref this._xScale, ref _shouldIncreaseXScale);
        this.ComputeNewScale(ref this._yScale, ref _shouldIncreaseYScale);
        this.ComputeNewScale(ref this._zScale, ref _shouldIncreaseZScale);

        this.transform.localScale = new Vector3(this._xScale, this._yScale, this._zScale);
    }
    private void ComputeNewScale(ref float scale, ref bool shouldIncreaseScale)
    {
        int deltaSign = shouldIncreaseScale ? 1 : -1;
        float assumedScaleDelta = deltaSign * this.scaleModificationSpeed * Time.smoothDeltaTime;
        float assumedScale = scale + assumedScaleDelta;

        if (assumedScale < scaleLowerBound)
        {
            scale = scaleLowerBound;
            shouldIncreaseScale = true;
        }
        else if (assumedScale > scaleUpperBound)
        {
            scale = scaleUpperBound;
            shouldIncreaseScale = false;
        }
        else
        {
            scale = assumedScale;
        }
    }

    private void UpdateRotation()
    {
        this.transform.Rotate(this.rotationModificationSpeed * new Vector3(1, 1, 1) * Time.smoothDeltaTime);
    }
}