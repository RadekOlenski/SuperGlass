using System;

using UnityEngine;

public class PourTransition : MonoBehaviour
{
    #region Public Fields

    public GameObject Bottle;

    public AnglesController AnglesController;

    public Vector3 BottlePourPosition;

    public Canvas GameCanvas;

    public ScoreCameraController ScoreCameraController;

    public bool PourStarted;

    public bool PourEnding;

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
    private float lerpSpeed = 6f;

    private float outputLerp;

    // dead zone between 0 and 1
    private float deadzone = 0.1f;

    #endregion

    #region Unity Methods

    public void Start()
    {
        bottleStartPosition = Bottle.transform.localPosition;
        bottleDeltaPosition = BottlePourPosition - bottleStartPosition;
    }

    public void Update()
    {
        // Calculate z-axis rotation based on accelerometer input 
        float inputTiltAngle = Mathf.Clamp(Input.acceleration.normalized.x, -maxTiltDeviceAngle, maxTiltDeviceAngle);
        float outputAngle = Mathf.Pow(
                                Mathf.Clamp(Mathf.Abs(inputTiltAngle) - deadzone, 0, maxTiltDeviceAngle)
                                / (maxTiltDeviceAngle - deadzone),
                                falloffAngle) * maxTiltDeviceAngle;
        float outputDevice = Mathf.Clamp(maxTiltDeviceAngle / inputTiltAngle, -1, 1) * outputAngle;
        outputLerp = Mathf.Lerp(outputLerp, outputDevice, lerpSpeed * Time.deltaTime);
        float angle = Mathf.Abs((outputLerp / maxTiltDeviceAngle) * maxTargetAngle);

        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle);

        // Change bottle position
        float maxPositionAngle = angle / 90f;
        if (angle >= this.AnglesController.CanvasDisableAngle)
        {
            this.GameCanvas.enabled = false;
        }
        else
        {
            this.GameCanvas.enabled = true;
        }

        if (angle > 0f)
        {
            Bottle.transform.localPosition = bottleStartPosition + (bottleDeltaPosition * maxPositionAngle);
            if (angle >= this.AnglesController.PourStartAngle)
            {
                if (!PourStarted) PourStarted = true;
            }
        }
        else
        {
            Bottle.transform.localPosition = bottleStartPosition;
        }

        if (PourStarted && angle < AnglesController.PourEndAngle)
        {
            PourEnding = true;
            PourStarted = false;
            this.ScoreCameraController.enabled = true;
            this.GameCanvas.enabled = true;
        }
    }

    #endregion
}