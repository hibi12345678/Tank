using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EffDestroy", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EffDestroy()
    {
        Destroy(gameObject);
    }
}
