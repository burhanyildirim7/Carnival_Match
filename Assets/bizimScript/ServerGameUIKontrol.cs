using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;

public class ServerGameUIKontrol : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* ORNEK KOD
         * 
        if (PhotonNetwork.IsConnected)
        {
           
           
        }

        hit.collider.gameObject.GetComponent<PhotonView>().RPC("OrnekMetod",RpcTarget.All,null);
        hit.collider.gameObject.GetComponent<PhotonView>().RPC("OrnekMetod", RpcTarget.All, 10); //her vuruşta 10 can götürecek
        *
        */



    }

    #region // RPC kodlar

    [PunRPC]
    public void OrnekMetod(int _deger)
    {




    }

    #endregion

}
