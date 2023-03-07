using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int _hiz;

    public GameObject _hedef;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerPrefs.GetInt("SesKapat") == 0)
        {
            transform.GetComponent<AudioSource>().enabled = true;
        }
        else
        {
            transform.GetComponent<AudioSource>().enabled = false;
        }
        if (GameController.instance.isContinue)
        {
            transform.LookAt(_hedef.transform.position);
            transform.Translate(Vector3.forward * Time.deltaTime * _hiz);
        }
    }

}
