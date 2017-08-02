using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class MainMusic : MonoBehaviour
{
    #region Singletton Pattern

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private static MainMusic instance;

    public static MainMusic Instance
    {
        get
        {
            if (instance != null) return instance;
            instance = new MainMusic();
            DontDestroyOnLoad(instance);
            return instance;
        }
    }

    #endregion

    public AudioSource AudioSource;

    // Use this for initialization
    void Start()
    {
        MainMusic.Instance.AudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
    }
}