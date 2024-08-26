using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAwake : MonoBehaviour
{
    [SerializeField]
    GameObject sensya;

    [SerializeField]
    GameObject effect;

    bool isDestroyed = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDestroyed && sensya.activeSelf == false)
        {
            isDestroyed = true;
            effect.SetActive(true);
        }
    }
}
