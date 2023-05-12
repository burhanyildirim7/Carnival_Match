using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Chat;
using Photon.Pun;
using Photon.Realtime;
using Unity.VisualScripting;

public class ServerGameUIKontrol : MonoBehaviourPunCallbacks
{
    [Header("ORTAK OBJELERI")]
    [SerializeField] List<GameObject> _roundObjects=new List<GameObject>();
    [SerializeField] Slider _timeSlider;
    [SerializeField] Text _oyuncuSirasiText;

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

        #region //oyuncu isimlerini güncelleme

        #endregion
    }

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

    #endregion

    private void RoundObjeleriniDuzenle()
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
