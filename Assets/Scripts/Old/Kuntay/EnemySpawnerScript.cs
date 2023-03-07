using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] GameObject _enemyObject, _bossObject;
    [SerializeField]
    public List<GameObject> _spawnPointsList = new List<GameObject>(),
        _enemyList = new List<GameObject>(),
        _bossList = new List<GameObject>(),
        _moneyList = new List<GameObject>(),
        _moneyStackParent = new List<GameObject>();

    private float _sayac1, _sayac2;
    private int _randomSayi;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log("EnemySpawnRate : " + PlayerPrefs.GetFloat("EnemySpawnRate"));

        if (PlayerPrefs.GetFloat("EnemySpawnRate") < 0.2f)
        {
            PlayerPrefs.SetFloat("EnemySpawnRate", .2f);
        }

        if (GameController.instance.isContinue)
        {
            if (GameObject.Find("SOKETLER_PARENT").transform.GetComponent<AnaSoketKontrol>()._SYSTEMCONTROL)
            {
                _sayac1 += Time.deltaTime;

                /*if (_sayac1 > PlayerPrefs.GetFloat("EnemySpawnRate") / 5)
                {
                    if (_moneyList.Count < 750)
                    {
                        _moneyStackParent[0].gameObject.GetComponent<moneyToplamaScript>().OtoToplanma();

                    }
                }*/

                if (_sayac1 > PlayerPrefs.GetFloat("EnemySpawnRate"))
                {
                    _sayac1 = 0;
                    _sayac2++;

                    if (_sayac2 < 20)
                    {
                        _randomSayi = Random.Range(0, _spawnPointsList.Count);
                        _enemyList[0].gameObject.SetActive(true);
                        _enemyList[0].transform.position = _spawnPointsList[_randomSayi].transform.position;
                        _enemyList[0].transform.parent = null;
                        _enemyList[0].transform.GetComponent<BoxCollider>().enabled = true;
                        _enemyList[0].transform.GetComponent<StickmanAnimation>()._dur = false;
                        _enemyList[0].transform.GetComponent<Animator>().SetBool("run", true);
                        _enemyList[0].transform.GetComponent<StickmanAnimation>()._secildi = false;
                        _enemyList.RemoveAt(0);
                    }
                    else
                    {
                        _sayac2 = 0;
                        _bossList[0].gameObject.SetActive(true);
                        _bossList[0].transform.position = _spawnPointsList[_randomSayi].transform.position;
                        _bossList[0].transform.GetComponent<BoxCollider>().enabled = true;
                        _bossList[0].transform.GetComponent<StickmanAnimation>()._dur = false;
                        _enemyList[0].transform.GetComponent<StickmanAnimation>()._secildi = false;
                        _bossList[0].transform.parent = null;
                        _enemyList[0].transform.GetComponent<Animator>().SetBool("slowRun", true);
                        _bossList.RemoveAt(0);
                    }
                }
            }
            else
            {

            }

        }
    }
}
