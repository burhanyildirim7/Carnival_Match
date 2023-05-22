using System.Collections.Generic;
using GameVanilla.Game.Common;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ServerGameUIKontrol : MonoBehaviourPunCallbacks
{
    [Header("ORTAK OBJELERI")]
    [SerializeField] List<GameObject> _roundObjects=new List<GameObject>();
    [SerializeField] Slider _timeSlider;
    [SerializeField] GameObject _oyuncuSirasiGorseli;


    [Header("PLAYER OBJELERI")] 
    [SerializeField] public List<GameObject> _playerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _playerSkillSprites = new List<Sprite>(), _playerGoalSprites = new List<Sprite>(),_playerPictureSprites = new List<Sprite>();
    [SerializeField] public Text _playerNameText, _playerSkillText, _playerScoreText;
    [SerializeField] GameObject _playerPic,_playerGoalPic, _playerSkill,_playerBoosterHammer,_playerBoosterShuffle;
    [SerializeField] Slider _playerMovesSlider;


    [Header("RAKİP PLAYER OBJELERI")]
    [SerializeField] public List<GameObject> _rakipPlayerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _rakipPlayerSkillSprites = new List<Sprite>(), _rakipplayerGoalSprites = new List<Sprite>(), _rakipPlayerPictureSprites = new List<Sprite>();
    [SerializeField] public Text _rakipPlayerNameText, _rakipPlayerSkillText, _rakipPlayerScoreText;
    [SerializeField] GameObject _rakipPlayerPic,_rakipPlayerGoalPic, _rakipPlayerSkill,_rakipPlayerBoosterHammer,_rakipPlayerBoosterShuffle;
    [SerializeField] Slider _rakipPlayerMovesSlider;

    [Header("SUPPORTS")]

    #region //public degiskenler
    public int _oyuncuSirasi;
    #endregion

    #region //private degiskenler
    private int _roundNo;
    private float _timerDeger;
    #endregion

    [SerializeField] GameScene gameScene;
    
    void Start()
    {
        _timerDeger = 45f;
        _timeSlider.maxValue = 45f;
        _timeSlider.value = 45f;

        #region //Kullanıcı bilgileri
        _rakipPlayerNameText.text = PhotonNetwork.NickName;
        _rakipPlayerNameText.text = PhotonNetwork.PlayerListOthers[0].NickName;

        _playerScoreText.text = "0";
        _rakipPlayerScoreText.text = "0";
        #endregion

        #region //Round Object kontrol baslangic

        _roundNo = 0;

        for (int i = 0; i < _roundObjects.Count; i++)
        {
            if (i == 0)
            {
                _roundObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(90, 130);
                _roundObjects[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
            }
            else
            {
                _roundObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(75, 110);
                _roundObjects[i].transform.GetChild(0).GetComponent<Text>().color = Color.black;
            }
        }
        #endregion

        if (PhotonNetwork.MasterClient.NickName==PhotonNetwork.NickName)   
        {
            _playerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[0];
            _rakipPlayerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[4];
        }
        else
        {
            _playerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[4];
            _rakipPlayerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[0];
        }
    }

    void FixedUpdate()
    {
        //HAMLE HAKKI DIGER KULLANICIYA GECINCE TIMER SIFIRLANMIYOR-RAKIP ve MASTER olarak iki sayac denenecek.
        //HAMLE HAKKI SORGUSU İLE SAYAC 45 E CEKILEBILIR-TEK KULLANICI UZERINDEN IKI KULLANICIYI YONETMEYE CALISMA
        //HAMLE HAKKI KIMDEYSE SAYAC SIFIRLANDIGIN O ROUNDU DEGISTIRSIN-ROUND DEGISINCE SAYACLAR 45E CEKILEBILIR
        /*
        if (PhotonNetwork.IsConnected)
        {
            _timerDeger -= Time.deltaTime;
            TimerGeriSayım(_timerDeger);
        }
        */
    }

    #region // photon metodlar

    public override void OnPlayerLeftRoom(Player otherPlayer)//bir kullıcı odaya çıktığında çalışır.
    {
        Debug.Log("BIR KULLANICI ODADAN AYRILDI-ODADAKİ OYUNCU SAYISI: " + PhotonNetwork.PlayerList.Length);
        InvokeRepeating("RakipKontrol",.01f,.1f);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("ODADAN CIKILDI");
        PhotonNetwork.Disconnect();
        CancelInvoke("RakipKontrol");
    }
    #endregion

    private void RakipKontrol()
    {
        if (PhotonNetwork.PlayerListOthers.Length==0)
        {
            //gameScene.OpenPopup<RegenLevelPopup>("Popups/RegenLevelPopup"); // buraya "rakip karşılaşmayı terk etti" popUp'ı koyulacak.
            //Aşağıdakiler "rakip karşılaşmayı terk etti" popUp'ının içindeki "devam et" buttonuna koyulacak.
            PhotonNetwork.LeaveRoom();
            gameScene.OpenPopup<RegenLevelPopup>("Popups/PvPWinPopup");
        }
        else
        {

        }

    }

    [PunRPC]
    public void TimerGeriSayım(float _deger)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
            {
                if (GameObject.Find("GameBoard").GetComponent<GameBoard>()._hamleSirasi)
                {
                    if (_timeSlider.value >= 1)
                    {
                        _timeSlider.value = _deger;
                        //GetComponent<PhotonView>().RPC("TimerGeriSayım", RpcTarget.Others, (float)_deger);
                    }
                    else
                    {
                        GetComponent<PhotonView>().RPC("RoundObjeleriniDuzenle", RpcTarget.All, null);

                        GameObject.Find("GameBoard").GetComponent<GameBoard>().SiraDüzenlemeTetikleme();
                    }
                }
            }
            else
            {
                if (!GameObject.Find("GameBoard").GetComponent<GameBoard>()._hamleSirasi)
                {
                    if (_timeSlider.value >= 1)
                    {
                        _timeSlider.value = _deger;
                        //GetComponent<PhotonView>().RPC("TimerGeriSayım", RpcTarget.Others, (float)_deger);
                    }
                    else
                    {
                        GetComponent<PhotonView>().RPC("RoundObjeleriniDuzenle", RpcTarget.All, null);

                        GameObject.Find("GameBoard").GetComponent<GameBoard>().SiraDüzenlemeTetikleme();
                    }
                }
            }
        }
    }


    #region // RPC kodlar

    public void MoveTimerSifirlama()
    {
        _timeSlider.value = 45f;
    }


    [PunRPC]
    public void RoundObjeleriniDuzenle()
    {
        _roundNo++;
        if (_roundNo<5) 
        {
            for (int i = 0; i < _roundObjects.Count; i++)
            {
                if (i == _roundNo)
                {
                    _roundObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(90, 130);
                    _roundObjects[i].transform.GetChild(0).GetComponent<Text>().color = Color.white;
                }
                else
                {
                    _roundObjects[i].GetComponent<RectTransform>().sizeDelta = new Vector2(75, 110);
                    _roundObjects[i].transform.GetChild(0).GetComponent<Text>().color = Color.black;
                }
            }

        }
    }
    #endregion

    
}
