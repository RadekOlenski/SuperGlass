using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassFill : MonoBehaviour
{
    [SerializeField]
    private GameObject foam, moreFoam;
    [SerializeField]
    private float fillSpeed;
    private float fillLevel = 0;

    private Vector3 startFoamPos;

    [SerializeField] private SpriteRenderer sr;
	// Use this for initialization
	void Start ()
	{
	    startFoamPos = foam.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		Fill();
	    foam.transform.localPosition = Vector3.Lerp(startFoamPos, new Vector3(startFoamPos.x,startFoamPos.y + 62,startFoamPos.z), fillLevel);
	    moreFoam.transform.localPosition = Vector3.Lerp(startFoamPos, new Vector3(startFoamPos.x, startFoamPos.y + 55, startFoamPos.z), fillLevel);
        sr.gameObject.transform.localScale = new Vector3(1,fillLevel,1);
	}

    public void Fill()
    {
        fillLevel = fillLevel +Time.deltaTime * fillSpeed;
    }
}
