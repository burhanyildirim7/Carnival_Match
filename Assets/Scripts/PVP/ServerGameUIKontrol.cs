using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class ServerGameUIKontrol : MonoBehaviourPunCallbacks
{
    [Header("ORTAK OBJELERI")]
    [SerializeField] List<GameObject> _roundObjects=new List<GameObject>();
    [SerializeField] Slider _timeSlider;
    [SerializeField] GameObject _oyuncuSirasiGorseli;

    [Header("PLAYER OBJELERI")] 
    [SerializeField] List<GameObject> _playerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _playerSkillPics = new List<Sprite>(), _playerGoalPics = new List<Sprite>(),_playerPictures = new List<Sprite>();
    [SerializeField] Text _playerNameText, _playerSkillText, _playerScoreText;
    [SerializeField] GameObject _playerPic, _playerSkill,_playerBoosterHammer,_playerBoosterShuffle;
    [SerializeField] Slider _playerMovesSlider;


    [Header("RAKİP PLAYER OBJELERI")]
    [SerializeField] List<GameObject> _rakipPlayerMoves = new List<GameObject>();
    [SerializeField] List<Sprite> _rakipPlayerSkillPics = new List<Sprite>(), _rakipplayerGoalPics = new List<Sprite>(), _rakipPlayerPictures = new List<Sprite>();
    [SerializeField] Text _rakipPlayerNameText, _rakipPlayerSkillText, _rakipPlayerScoreText;
    [SerializeField] GameObject _rakipPlayerPic, _rakipPlayerSkill,_rakipPlayerBoosterHammer,_rakipPlayerBoosterShuffle;
    [SerializeField] Slider _rakipPlayerMovesSlider;

    #region //public degiskenler
    public int _oyuncuSirasi;
    #endregion

    #region //private degiskenler
    private int _roundNo;
    #endregion

    void Start()
    {
        #region //Kullanıcı bilgileri
        string[] _playerBilgileri = PhotonNetwork.NickName.Split('/');
        _rakipPlayerNameText.text = _playerBilgileri[0];

        string[] _rakipBilgileri = PhotonNetwork.PlayerListOthers[0].NickName.Split('/');
        _rakipPlayerNameText.text = _rakipBilgileri[0];
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
    }

    void Update()
    {

    }

    public void DEGERDEGISIMDENEME()
    {
        RoundObjeleriniDuzenle();
    }


    #region // RPC kodlar

    [PunRPC]
    public void OrnekMetod(int _deger)
    {




    }

    [PunRPC]
    public void RoundGuncelle()
    {
        switch (_roundNo)
        {
            case 0:
                _roundNo++;
                RoundObjeleriniDuzenle();
                break;
            case 1:
                _roundNo++;
                RoundObjeleriniDuzenle();
                break;
            case 2:
                _roundNo++;
                RoundObjeleriniDuzenle();
                break;
            case 3:
                _roundNo++;
                RoundObjeleriniDuzenle();
                break;
            case 4:
                _roundNo++;
                RoundObjeleriniDuzenle();
                break;
        }
    }

    [PunRPC]
    private void RoundObjeleriniDuzenle()
    {
        _roundNo++;
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
    #endregion

}
