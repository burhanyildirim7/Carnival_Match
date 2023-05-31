using System.Collections;
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
    [SerializeField] GameObject _baslangicPaneli;
    [SerializeField] List<GameObject> _roundObjects = new List<GameObject>();
    [SerializeField] Slider _timeSlider;
    [SerializeField] GameObject _oyuncuSirasiGorseli;


    [Header("PLAYER OBJELERI")]
    [SerializeField] public List<GameObject> _playerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _playerSkillSprites = new List<Sprite>(), _playerGoalSprites = new List<Sprite>(), _playerPictureSprites = new List<Sprite>();
    [SerializeField] public Text _playerNameText, _playerSkillText, _playerScoreText;
    [SerializeField] GameObject _playerPic, _playerGoalPic, _playerSkillButton, _playerBoosterHammer, _playerBoosterShuffle;
    [SerializeField] Slider _playerSkillSlider;
    private int _playerSkillSayac = 0;

    [Header("RAKİP PLAYER OBJELERI")]
    [SerializeField] public List<GameObject> _rakipPlayerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _rakipPlayerSkillSprites = new List<Sprite>(), _rakipplayerGoalSprites = new List<Sprite>(), _rakipPlayerPictureSprites = new List<Sprite>();
    [SerializeField] public Text _rakipPlayerNameText, _rakipPlayerSkillText, _rakipPlayerScoreText;
    [SerializeField] GameObject _rakipPlayerPic, _rakipPlayerGoalPic, _rakipPlayerSkill, _rakipPlayerBoosterHammer, _rakipPlayerBoosterShuffle;
    [SerializeField] Slider _rakipPlayerSkillSlider;
    private int _rakipPlayerSkillSayac = 0;

    [Header("SUPPORTS")]

    #region //public degiskenler
    public int _oyuncuSirasi;
    #endregion

    #region //private degiskenler
    private int _roundNo;
    private float _timerDeger;
    private int _playerGoalSecim, _rakipGoalSecim;
    private List<int> _candyColorListesi = new List<int>(){0,1,2,3,4,5};
    #endregion

    private bool _bitti;

    [SerializeField] GameScene gameScene;
    private void Awake()
    {
        _bitti = false;
        if (PhotonNetwork.MasterClient.NickName==PhotonNetwork.NickName)    
        {
            _playerGoalSecim = Random.Range(0, _candyColorListesi.Count);
            _candyColorListesi.RemoveAt(_playerGoalSecim);
            _rakipGoalSecim = Random.Range(0, _candyColorListesi.Count);
            _rakipGoalSecim = _candyColorListesi[_rakipGoalSecim];
            _playerSkillButton.GetComponent<Button>().interactable = false;
            _playerBoosterHammer.GetComponent<Button>().interactable = false;
            _playerBoosterShuffle.GetComponent<Button>().interactable = false;
            _rakipPlayerBoosterHammer.GetComponent<Button>().interactable = false;
            _rakipPlayerBoosterShuffle.GetComponent<Button>().interactable = false;

        }
        _playerSkillText.text = "0/6";
        _rakipPlayerSkillText.text = "0/6";

        _playerSkillSlider.maxValue = 6;
        _rakipPlayerSkillSlider.maxValue = 6;
        _playerSkillSlider.value = 0;
        _rakipPlayerSkillSlider.value = 0;
         StartCoroutine(PVPBaslangic());
    }

    private IEnumerator PVPBaslangic()
    {
        yield return new WaitForSeconds(2.0f);
        _baslangicPaneli.SetActive(false);
    }

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
        if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
        {
            _playerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[_playerGoalSecim];
            _rakipPlayerGoalPic.GetComponent<Image>().sprite = _rakipplayerGoalSprites[_rakipGoalSecim];
            photonView.RPC("GoalAyarlama", RpcTarget.Others, (int)_playerGoalSecim, (int)_rakipGoalSecim);
        }

    }

    [PunRPC]
    public void GoalAyarlama(int a, int b)
    {
        _playerGoalPic.GetComponent<Image>().sprite = _playerGoalSprites[b];
        _rakipPlayerGoalPic.GetComponent<Image>().sprite = _rakipplayerGoalSprites[a];
        _playerGoalSecim = b;
        _rakipGoalSecim = a;
    }

    void Update()
    {     
        _timerDeger -= Time.deltaTime;
        TimerGeriSayım(_timerDeger);       
    }

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

    private void RakipKontrol()
    {
        if (PhotonNetwork.PlayerListOthers.Length==0 && !_bitti)
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

    public void MoveTimerSifirlama()
    {
        _timerDeger = 45f;
        _timeSlider.value = 45f;
    }

    public void TimerGeriSayım(float _deger)
    {
        if (_timeSlider.value > 0)
        {
            _timeSlider.value = _deger;
        }
        else
        {
            if (GameObject.Find("GameBoard").GetComponent<GameBoard>()._hamleSirasi)    
            {
                GameObject.Find("GameBoard").GetComponent<GameBoard>().SiraDüzenlemeTetikleme();
                MoveTimerSifirlama();
            }
        }
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
        if (_roundNo==5)   
        {   
            if (int.Parse(_playerScoreText.text)>int.Parse(_rakipPlayerScoreText.text))
            {
                _bitti = true;
                gameScene.OpenPopup<RegenLevelPopup>("Popups/PvPWinPopup");
            }
            else
            {
                _bitti = true;
                gameScene.OpenPopup<RegenLevelPopup>("Popups/PvPLosePopup");
            }
        }

        if (_roundNo==2) 
        {
            _playerBoosterHammer.GetComponent<Button>().interactable = true;
            _playerBoosterShuffle.GetComponent<Button>().interactable = true;
            _rakipPlayerBoosterHammer.GetComponent<Button>().interactable = true;
            _rakipPlayerBoosterShuffle.GetComponent<Button>().interactable = true;
        }
    }

    [PunRPC]
    public void RakipGoalDuzenleme()
    {
        _rakipPlayerSkillSlider.value++;
        _rakipPlayerSkillSayac++;
        if (_rakipPlayerSkillSayac >= 6)
        {
            _rakipPlayerSkillSayac = 6;
        }

        _rakipPlayerSkillText.text = _rakipPlayerSkillSayac.ToString() + "/6";
    }

    public void GoalKontrol(int _colorGelen)
    {
        if (_playerGoalSecim==_colorGelen)
        {
            _playerSkillSayac++;
            if (_playerSkillSayac>=6)
            {
                if (PlayerPrefs.GetInt("vibration_enabled") == 0)
                {
                    MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);
                }
                else
                {

                }
                _playerSkillSayac = 6;
                _playerSkillButton.GetComponent<Button>().interactable = true;
            }
            _playerSkillText.text = _playerSkillSayac.ToString() + "/6";
            _playerSkillSlider.value++;
            GetComponent<PhotonView>().RPC("RakipGoalDuzenleme", RpcTarget.Others,null);
        }
        else
        {

        }
    }

    public void PlayerSkillSayacSifirlama()
    {
        _playerSkillSayac = 0;
        _playerSkillSlider.value = 0;
        _playerSkillText.text = "0/6";
        _playerSkillButton.GetComponent<Button>().interactable = false;
        //photonView.RPC("RakipSkillSifirlama",RpcTarget.Others,null);
    }

    [PunRPC]
    public void RakipSkillSifirlama()
    {
        _rakipGoalSecim = 0;
        _rakipPlayerSkillSlider.value = 0;
        _rakipPlayerSkillText.text = "0/6";
    }

    public void PlayerHammerKapatma()
    {
        _playerBoosterHammer.GetComponent<Button>().interactable = false;
        //photonView.RPC("RakipHammerKapatma", RpcTarget.Others, null);
    }

    [PunRPC]
    public void RakipHammerKapatma()
    {
        _rakipPlayerBoosterHammer.GetComponent<Button>().interactable = false;
    }

    public void PlayerShuffleButton()
    {
        _playerBoosterShuffle.GetComponent<Button>().interactable = false;
        GameObject.Find("GameBoard").GetComponent<GameBoard>().RegerateLevelCalistirma();
        photonView.RPC("RakipShuffleKapatma", RpcTarget.Others, null);
    }

    [PunRPC]
    public void RakipShuffleKapatma()
    {
        _rakipPlayerBoosterShuffle.GetComponent<Button>().interactable = false;
    }



}
