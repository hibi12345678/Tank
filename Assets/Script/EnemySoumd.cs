using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoumd : MonoBehaviour
{

    private Vector3 previousPosition;
    AudioSource moveSound;
    AudioSource fireSound;
    private int b = 0;

    void Start()
    {
        previousPosition = transform.position;
        AudioSource[] audioSource = GetComponents<AudioSource>();
        moveSound = audioSource[0];
        Invoke("SoundPlay", 0.5f);
    }

    void Update()
    {
        if (transform.position != previousPosition)
        {
            if (b == 1)
            {
                moveSound.UnPause();
            }
        }
        else
        {
            moveSound.Pause();
        }

        previousPosition = transform.position;
        
    }

    public void SoundPlay()
    {
        if (moveSound != null && !moveSound.isPlaying)
        {
            moveSound.Play();
            b = 1;
        }
    }

}
