using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sesKontrolIcin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("SesKapat") == 0)
        {
            transform.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            transform.GetComponent<AudioSource>().enabled = false;
        }
    }
}
