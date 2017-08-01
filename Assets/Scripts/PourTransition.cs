using System;

using UnityEngine;

public class PourTransition : MonoBehaviour
{
    #region Public Fields

    public GameObject Bottle;

    public Vector3 BottlePourPosition;

    #endregion

    #region Private Fields

    private Vector3 bottleStartPosition;

    private Vector3 bottleDeltaPosition;

    // camera rotation limits
    private float maxTargetAngle = 90f;

    // tilt limit between 0 and 1
    private float maxTiltDeviceAngle = 0.9f;

    // output curve
    private float falloffAngle = 3f;

    // input smoothing, higher = faster
    private float lerpSpeed = 5f;

    private float outputLerp;

    // dead zone between 0 and 1
    private float deadzone = 0.1f;

    #endregion

    #region Unity Methods

    public void Start()
    {
        this.bottleStartPosition = this.Bottle.transform.localPosition;
        this.bottleDeltaPosition = this.BottlePourPosition - this.bottleStartPosition;
    }

    public void Update()
    {
        // if (Input.acceleration.y < 0f) return;
        if (Math.Abs(Input.acceleration.z) > Math.Abs(Input.acceleration.x)
            && Math.Abs(Input.acceleration.z) > Math.Abs(Input.acceleration.y)) return;

        float inputTiltAngle = Mathf.Clamp(Input.acceleration.normalized.x, -maxTiltDeviceAngle, maxTiltDeviceAngle);
        float outputAngle = Mathf.Pow(
                                Mathf.Clamp(Mathf.Abs(inputTiltAngle) - deadzone, 0, maxTiltDeviceAngle)
                                / (maxTiltDeviceAngle - deadzone),
                                falloffAngle) * maxTiltDeviceAngle;
        float outputDevice = Mathf.Clamp(maxTiltDeviceAngle / inputTiltAngle, -1, 1) * outputAngle;
        outputLerp = Mathf.Lerp(outputLerp, outputDevice, lerpSpeed * Time.deltaTime);
        float angle = Mathf.Abs((outputLerp / maxTiltDeviceAngle) * maxTargetAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        float maxPositionAngle = angle / 90f;

        if (angle > 0f)
        {
            Bottle.transform.localPosition = bottleStartPosition + (bottleDeltaPosition * maxPositionAngle);
        }
        else
        {
            Bottle.transform.localPosition = bottleStartPosition;
        }
    }

    #endregion
}