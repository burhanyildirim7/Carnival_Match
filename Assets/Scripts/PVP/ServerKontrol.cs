using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;
using System.Drawing;
using GameVanilla.Core;
using DG.Tweening;

public class ServerKontrol : MonoBehaviourPunCallbacks
{
    private bool _IsInternetAvailable = false;
    [SerializeField] InputField _kullaniciAdi;
    [Header("GENEL")]
    [SerializeField] PVPPanelUI _pvpPanelUIScript;
    [SerializeField] GameObject _pvpLevelsContentObject;
    [SerializeField] List<GameObject> _pvpLevels = new List<GameObject>();
    [SerializeField] List<GameObject> _pvpLevelsLockedObjects = new List<GameObject>();
    [SerializeField] List<Text> _pvpLevelsAcilisBedelText=new List<Text>();
    [SerializeField] List<Text> _pvpLevelsOynamaBedelText = new List<Text>();
    [SerializeField] List<Text> _pvpLevelsRewardsAmountText = new List<Text>();
    [SerializeField] Text _rozetAmountText, _sliderRozetAmountText,_sliderBasiLevelText,_sliderSonuLevelText;
    [SerializeField] Slider _rozetBiriktirmeSlideri;
    private int _pvpContentSira;

    [Header("RAKIP ARAMA PANELI")]
    [SerializeField] GameObject _rakipAraniyorPanel;
    [SerializeField] GameObject _cancelButton;
    [SerializeField] GameObject _rakipPlayerPic;
    [SerializeField] List<Sprite> _rakipPlayerPicSprites=new List<Sprite>();
    [SerializeField] Text _rakipAraniyorText,_playerNameText,_playerTeamNameText,_playerRozetAmountText,_rakipPlayerNameText, _rakipPlayerTeamNameText, _rakipPlayerRozetAmountText;

    private void Awake()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Disconnect();
        }
        else
        {

        }
    }
    void Start()
    {
        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevels.Add(_pvpLevelsContentObject.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsLockedObjects.Add(_pvpLevelsContentObject.transform.GetChild(i).GetChild(0).GetChild(2).gameObject);
        }
        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsAcilisBedelText.Add(_pvpLevelsContentObject.transform.GetChild(i).GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<Text>());
        }
        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsOynamaBedelText.Add(_pvpLevelsContentObject.transform.GetChild(i).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<Text>());
        }
        for (int i = 0; i < _pvpLevelsContentObject.transform.childCount; i++)
        {
            _pvpLevelsRewardsAmountText.Add(_pvpLevelsContentObject.transform.GetChild(i).GetChild(0).GetChild(5).gameObject.GetComponent<Text>());
        }
        _pvpContentSira = 0;


        for (int i = 1; i < _pvpLevelsLockedObjects.Count; i++) 
        {
            if (_pvpLevelsLockedObjects[i].activeSelf)   
            {

            }
            else
            {
                _pvpContentSira++;
            }
        }
        _pvpPanelUIScript.ContentSiraDegiskeniniGuncelle(_pvpContentSira);

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
        PhotonNetwork.NickName = _kullaniciAdi.text;
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
            Invoke("RakipSorgula", .1f);
        }
        else
        {
            Debug.Log("ODADA KIMSE YOK");
            InvokeRepeating("RakipSorgula", .1f,.1f);
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

    #region // private metodlar
    private void SearchinTextAnimasyon()
    {
        if (_rakipAraniyorText.text=="Searching...")
        {
            _rakipAraniyorText.text="Searching";
            _rakipPlayerNameText.text = "";
            _rakipPlayerTeamNameText.text = "";
            _rakipPlayerRozetAmountText.text = "";
        }
        else
        {
            _rakipAraniyorText.text = _rakipAraniyorText.text + ".";
            _rakipPlayerNameText.text = _rakipPlayerNameText.text + "?";
            _rakipPlayerTeamNameText.text = _rakipPlayerTeamNameText.text + "?";
            _rakipPlayerRozetAmountText.text = _rakipPlayerRozetAmountText.text + "?";
        }
    }

    private void RakipAramaCancelButtonAktiflik()
    {
        if (PhotonNetwork.IsConnected)
        {
            _cancelButton.GetComponent<Button>().interactable = true;
            CancelInvoke("RakipAramaCancelButtonAktiflik");

        }
        else
        {
            _cancelButton.GetComponent<Button>().interactable = false;
        }
    }

    private void RakipSorgula()
    {
        if (PhotonNetwork.PlayerListOthers.Length>0)
        {
            Animator _rakipPicAnimator = _rakipPlayerPic.GetComponent<Animator>();
            _rakipPicAnimator.SetBool("run", false);
            CancelInvoke("RakipSorgula");
            CancelInvoke("SearchinTextAnimasyon");
            _rakipPlayerNameText.text = PhotonNetwork.PlayerListOthers[0].NickName;
            _rakipPlayerTeamNameText.text = "No Team";
            _playerTeamNameText.text ="No Team";
            /*
            string[] _rakipBilgileri = PhotonNetwork.PlayerListOthers[0].NickName.Split('/');
            _rakipPlayerNameText.text = _rakipBilgileri[0];
            _rakipPlayerTeamNameText.text = _rakipBilgileri[1];
            _rakipPlayerRozetAmountText.text= _rakipBilgileri[2];
            
            string[] _playerBilgileri = PhotonNetwork.NickName.Split('/');
            _playerTeamNameText.text = _playerBilgileri[1];
            */

            Invoke("PVPSahnesineGecis", 1f);
        }
        else
        {

        }
    }
    private void PVPSahnesineGecis()
    {
        GetComponent<SceneTransition>().OpenPVPScene();
    }
    #endregion

    #region //public metodlar
    public void MatchButton()
    {
        PhotonNetwork.ConnectUsingSettings();
        _rakipAraniyorPanel.SetActive(true);
        InvokeRepeating("SearchinTextAnimasyon", .01f, .375f);
        InvokeRepeating("RakipAramaCancelButtonAktiflik",.01f,.1f);

        Animator _rakipPicAnimator = _rakipPlayerPic.GetComponent<Animator>();
        _rakipPicAnimator.SetBool("run",true);
    }

    public void RakipAramaCancelButton()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();
        _rakipAraniyorPanel.SetActive(false);
    }
    #endregion
}
