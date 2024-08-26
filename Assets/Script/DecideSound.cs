using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideSound : MonoBehaviour
{

    public AudioClip sound1;
    AudioSource audioSource;
    int a;
    void Start()
    {
        a = 0;
        //Component‚ðŽæ“¾
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundClick()
    {
        if (a == 0)
        {
            //‰¹(sound1)‚ð–Â‚ç‚·
            audioSource.PlayOneShot(sound1);
            a = 1;
        }
        
    }
}
