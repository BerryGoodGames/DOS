using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MTexture : MonoBehaviour
{
    public static MTexture Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
}
