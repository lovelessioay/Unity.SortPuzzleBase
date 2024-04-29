using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerSpawner : MonoBehaviour
{
    [SerializeField] private AudioPlayerController controller;

    private void Awake()
    {
        if (AudioPlayerController.Instance == null)
        {
            GameObject audioPlayer = Instantiate(controller.gameObject);
            DontDestroyOnLoad(audioPlayer);
        }
    }
}
