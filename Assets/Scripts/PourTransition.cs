using UnityEngine;

public class PourTransition : MonoBehaviour
{
    #region Properties

    public GameObject Glass;

    #endregion

    #region Unity Methods

    public void Start()
    {

    }

    public void FixedUpdate()
    {
        Vector3 xAxis = new Vector3(Input.acceleration.x, 0f, 0f);

        float angle = Vector3.Angle(Physics.gravity, xAxis);

        Glass.transform.position += new Vector3(0.1f * angle, 0f, 0f);
    }

    #endregion

}