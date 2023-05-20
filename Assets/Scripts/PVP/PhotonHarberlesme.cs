using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class PhotonHarberlesme : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    private List<GameObject> _denemeGameObjectList = new List<GameObject>();
    [SerializeField]
    private List<Transform> _denemeTransformList = new List<Transform>();
    [SerializeField]
    private InputField _deneme;

    void Start()
    {
        PhotonNetwork.SendRate = 40;
        PhotonNetwork.SerializationRate = 40;
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Update()
    {

    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(_denemeGameObjectList);
            stream.SendNext(_denemeTransformList);
            stream.SendNext(_deneme.text);
        }
        else if (stream.IsReading)
        {
            _denemeGameObjectList = (List<GameObject>)stream.ReceiveNext();
            _denemeTransformList = (List<Transform>)stream.ReceiveNext();
            _deneme.text = (string)stream.ReceiveNext();
        }
    }
}
