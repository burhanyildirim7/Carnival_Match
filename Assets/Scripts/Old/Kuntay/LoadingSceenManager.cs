using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LoadingSceenManager : MonoBehaviour
{

    public Slider progressBar;
    public Text progressValueText;
    [SerializeField] private bool _StartLoadingScene, _GameLoadingScene;
    [SerializeField] private List<GameObject> _gorseller = new List<GameObject>();
    // Start is called before the first frame update

    private void Start()
    {
        if (_StartLoadingScene)
        {
            Load(1);
        }
        else if(_GameLoadingScene)
        {
            Load(3);
        }
        else
        {

        }
    }

    public void Load(int level)
    {

        if (_StartLoadingScene)
        {
            StartCoroutine(startLoading(level));
        }
        else if (_GameLoadingScene)
        {
            int randomDeger = Random.Range(0,_gorseller.Count-1);
            for (int i = 0; i < _gorseller.Count; i++)
            {
                if (i==randomDeger)
                {
                    _gorseller[i].SetActive(true);
                }
                else
                {
                    _gorseller[i].SetActive(false);
                }
            }
            StartCoroutine(gameLoading(level));
        }
        else
        {

        }

    }

    IEnumerator startLoading(int level)
    {
        yield return new WaitForSeconds(2.5f);
        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {

            progressBar.value = async.progress;
            progressValueText.text = "%"+((int)((progressBar.value)*112)).ToString();
            yield return null;

        }
        
    }
    IEnumerator gameLoading(int level)
    {
        yield return new WaitForSeconds(2f);

        AsyncOperation async = SceneManager.LoadSceneAsync(level);

        while (!async.isDone)
        {

            progressBar.value = async.progress;
            progressValueText.text = "%" + ((int)((progressBar.value) * 112)).ToString();
            yield return null;

        }
    }

}
