using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextKopyalama : MonoBehaviour
{
    [SerializeField] Text _kopyalanacakText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<Text>().text = _kopyalanacakText.text;
    }
}
