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
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    public void SoundClick()
    {
        if (a == 0)
        {
            //音(sound1)を鳴らす
            audioSource.PlayOneShot(sound1);
            a = 1;
        }
        
    }
}
