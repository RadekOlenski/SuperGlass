using System.Collections;
using System.Collections.Generic;

using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

public class TextPunch : MonoBehaviour
{
    public void OnEnable()
    {
        transform.DOPunchScale(new Vector3(3f, 3f, 3f), 0.5f, 2);
    }
}