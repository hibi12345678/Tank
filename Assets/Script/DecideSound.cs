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
        //Component���擾
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundClick()
    {
        if (a == 0)
        {
            //��(sound1)��炷
            audioSource.PlayOneShot(sound1);
            a = 1;
        }
        
    }
}
