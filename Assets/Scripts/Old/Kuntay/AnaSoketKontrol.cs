using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;


public class AnaSoketKontrol : MonoBehaviour
{
    [SerializeField] GameObject _AnaSoket,_generatorOpenSound,_generatorCloseSound;
    [SerializeField] Animator _GeneratorAnimator;
    public bool _SYSTEMCONTROL;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameController.instance.isContinue)
        {

            if (_AnaSoket.transform.GetComponent<YapbozAlaniDoluluk>()._soketDoluluk)
            {
                _SYSTEMCONTROL = true;
                _GeneratorAnimator.SetBool("Run", true);
                if (PlayerPrefs.GetInt("GeneratorSound")==0)
                {
                    PlayerPrefs.SetInt("GeneratorSound", 1);
                    if (PlayerPrefs.GetInt("SesKapat") == 0)
                    {
                        _generatorOpenSound.transform.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                    }
                }
                else
                {

                }
            }
            else
            {
                _SYSTEMCONTROL = false;
                _GeneratorAnimator.SetBool("Run",false);
                if (PlayerPrefs.GetInt("GeneratorSound") == 1)
                {
                    PlayerPrefs.SetInt("GeneratorSound", 0);
                    if (PlayerPrefs.GetInt("SesKapat") == 0)
                    {
                        _generatorCloseSound.transform.GetComponent<AudioSource>().Play();
                    }
                    else
                    {
                    }
                }
                else
                {

                }
            }

        }
    }
}
