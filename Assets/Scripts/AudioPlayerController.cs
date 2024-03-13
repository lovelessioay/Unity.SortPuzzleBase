using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerController : MonoBehaviour
{
    private static AudioPlayerController instance;
    public static AudioPlayerController Instance { get => instance; }

    private void Awake()
    {
        if (!instance) instance = this;
    }
}
