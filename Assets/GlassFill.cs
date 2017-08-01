using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFill : MonoBehaviour
{
    [SerializeField]
    private GameObject foam, moreFoam, overFoam;
    private float fillLevel = 0;

    private Vector3 startFoamPos;
    private float timer = 0;

    [SerializeField] private SpriteRenderer sr;
	void Start ()
	{
	    startFoamPos = foam.transform.localPosition;

        startFoamPos.y = 0;
	    foam.transform.position = startFoamPos;
	    moreFoam.transform.position = startFoamPos;
	    overFoam.transform.position = startFoamPos;
	    timer = 0;
	}

    void Update()
    {
        sr.gameObject.transform.localScale = new Vector3(1, Mathf.Clamp01(fillLevel), 1);
    }

    public void StartFill(float fillSpeed)
    {
        fillLevel = fillLevel + Time.deltaTime * fillSpeed;
        foam.transform.localPosition = Vector3.Lerp(startFoamPos, new Vector3(startFoamPos.x, startFoamPos.y + 59, startFoamPos.z), fillLevel);
        moreFoam.transform.localPosition = Vector3.Lerp(startFoamPos, new Vector3(startFoamPos.x, startFoamPos.y + 52, startFoamPos.z), fillLevel);
    }

    public float? EndFill(float endSpeed, float endTime)
    {
        if (timer <= endTime)
        {
            fillLevel = fillLevel + Time.deltaTime * endSpeed;
            timer += Time.deltaTime;
            return null;
        }
        else
        {
            return fillLevel;
        }
    }
}
