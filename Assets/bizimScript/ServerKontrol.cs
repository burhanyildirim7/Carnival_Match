using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Photon.Pun;
using Photon.Realtime;

public class ServerKontrol : MonoBehaviourPunCallbacks
{
    private bool _IsInternetAvailable = false;

    [Header("GENEL")]

    [SerializeField] List<GameObject> _pvpLevels = new List<GameObject>();
    [SerializeField] List<GameObject> _pvpLevelsLockedObjects = new List<GameObject>();
    [SerializeField] List<Text> _pvpLevelsAcilisBedelText=new List<Text>();
    [SerializeField] List<Text> _pvpLevelsOynamaBedelText = new List<Text>();
    [SerializeField] List<Text> _pvpLevelsRewarsAmountText = new List<Text>();
    [SerializeField] GameObject _sagOkObject, _solOkObject;
    [SerializeField] Text _rozetAmountText, _sliderRozetAmountText,_sliderBasiLevelText,_sliderSonuLevelText;
    [SerializeField] Slider _rozetBiriktirmeSlideri;


    [Header("RAKIP ARAMA PANELI")]
    [SerializeField] GameObject _rakipAraniyorPanel,_cancelButton;
    [SerializeField] Text _rakipAraniyorText,_playerNameText,_playerTeamNameText,_playerRozetAmountText,_rakipPlayerNameText, _rakipPlayerTeamNameText, _rakipPlayerRozetAmountText;



    void Start()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("HATA:INTERNETE BAĞLI DEĞİL");
            _IsInternetAvailable = false;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
        {
            Debug.Log("UYARI:INTERNETE BAĞLI + WIFI");
            _IsInternetAvailable = true;
        }
        else if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
        {
            Debug.Log("UYARI:INTERNETE BAĞLI + MOBILE");
            _IsInternetAvailable = true;
        }
        else
        {
            Debug.Log("UYARI:INTERNETE BAĞLI + BIR SEKILDE");
            _IsInternetAvailable = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    #region //PHOTONENGINE KODLARI
    public override void OnConnectedToMaster()
    {   /*
        Debug.Log("OnConnectedToMaster() was called by PUN.");

        PhotonNetwork.JoinLobby();

        PhotonNetwork.JoinRoom("Room_10000001"); // Room_10000001 isimli odaya katılır.

        PhotonNetwork.JoinRandomRoom();// rastgele odaya katılır.
        
        #region // Oda kurma
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = 2;
        roomOptions.IsOpen = true;
        PhotonNetwork.CreateRoom("MyGameRoom", roomOptions, TypedLobby.Default);
        #endregion
        */
        //açık oda varsa odaya katılır yoksa RoomOptions özelliklerinde yeni bir oda oluşturur.
        PhotonNetwork.JoinOrCreateRoom("MyGameRoom", new RoomOptions { MaxPlayers = 2, IsOpen = true, IsVisible = true }, TypedLobby.Default);

    }

    public override void OnConnected()
    {

        Debug.Log("SERVERA BAĞLANILDI");
        //_serverDurumText.text = "SERVERA BAĞLANILDI";

    }

    public override void OnJoinedLobby()
    {
        Debug.Log("LOBIYE GIRILDI");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("ODAYA GIRILDI");
        if (PhotonNetwork.PlayerList.Length > 1)
        {
            Debug.Log("ODADA BIRI VAR-ODADAKİ OYUNCU SAYISI: " + PhotonNetwork.PlayerList.Length);
            GameObject playerNesne = PhotonNetwork.Instantiate("PlayerObjesi", new Vector3(-3, 1, 5), Quaternion.identity, 0, null);
            playerNesne.transform.tag = "Player";
            playerNesne.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.NickName;
            //_rakipDurumuText.text = "RAKİP BULUNDU";
        }
        else
        {
            Debug.Log("ODADA KIMSE YOK");
            GameObject playerNesne = PhotonNetwork.Instantiate("PlayerObjesi", new Vector3(3, 1, 5), Quaternion.identity, 0, null);
            playerNesne.transform.tag = "Player";
            playerNesne.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = PhotonNetwork.NickName;
            //_rakipDurumuText.text = "RAKİP BEKLENİYOR...";
        }

    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("HATA:ODAYA GIRILEMEDI");
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("HATA:RASTGELE ODAYA GIRILEMEDI");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("ODA OLUŞTURULDU");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("HATA:ODA OLUŞTURULAMADI");
    }

    public override void OnLeftLobby()
    {
        Debug.Log("LOBIDEN CIKILDI");
    }

    public override void OnLeftRoom()
    {
        Debug.Log("ODADAN CIKILDI");
    }

    public override void OnPlayerEnteredRoom(Player newPlayer) //bir kullıcı odaya girdiğinde çalışır.
    {
        Debug.Log("BIR KULLANICI ODAYA KATILDI-ODADAKİ OYUNCU SAYISI: " + PhotonNetwork.PlayerList.Length);
        //_rakipDurumuText.text = "RAKİP BULUNDU";
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)//bir kullıcı odaya çıktığında çalışır.
    {
        Debug.Log("BIR KULLANICI ODADAN AYRILDI-ODADAKİ OYUNCU SAYISI: " + PhotonNetwork.PlayerList.Length);
    }

    public override void OnDisconnected(DisconnectCause cause) //Photon Server ile bağlantı kesiltiğinde çalışır.
    {
        Debug.Log("SERVER BAĞLANTISI KESILDI");
    }

    #endregion



}
