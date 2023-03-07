using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HomePageBottomButtonScript : MonoBehaviour
{
    [SerializeField] RectTransform _UICanvas;
    [SerializeField] GameObject _homePage, _teamPage, _collectionPage, _tournementPage, _friendsPage;
    [SerializeField] private GameObject _friendsPanelControlScript, _teamPanelControlScript;
    [SerializeField] RectTransform _homePageButton, _teamPageButton, _collectionPageButton, _tournementPageButton, _friendsPageButton;
    [SerializeField] Sprite _unSelectedImage, _selectedImage;
    // Start is called before the first frame update
    void Start()
    {
        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _homePageButton.GetChild(2).transform.localPosition = new Vector3(0, 200, 0);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(350, 350), .1f);
        _homePageButton.GetComponent<Image>().sprite = _selectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(175, 175), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(175, 175), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(175, 175), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(175, 175), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _unSelectedImage;


        _homePage.SetActive(true);
        _teamPage.SetActive(false);
        _collectionPage.SetActive(false);
        _tournementPage.SetActive(false);
        _friendsPage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //        Debug.Log("UI CANVAS GENİSLİĞİ= "+_UICanvas.rect.width);
    }


    public void HomePage()
    {

        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _homePageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 200, 0), .2f);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(350, 350), .1f);
        _homePageButton.GetComponent<Image>().sprite = _selectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(175, 175), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(175, 175), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(175, 175), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(175, 175), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _unSelectedImage;
        _homePage.SetActive(true);
        _teamPage.SetActive(false);
        _collectionPage.SetActive(false);
        _tournementPage.SetActive(false);
        _friendsPage.SetActive(false);
    }

    public void TeamPage()
    {

        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _homePageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(175, 175), .1f);
        _homePageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 200, 0), .2f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(350, 350), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _selectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(175, 175), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(175, 175), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(175, 175), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _unSelectedImage;
        _homePage.SetActive(false);
        _teamPage.SetActive(true);
        _collectionPage.SetActive(false);
        _tournementPage.SetActive(false);
        _friendsPage.SetActive(false);
        if (PlayerPrefs.GetInt("CreationPanel") == 1)
        {

        }
        else
        {
            _teamPanelControlScript.GetComponent<TeamPanelScript>().JoinPanelButton();

        }

    }
    public void CollectionPage()
    {

        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _homePageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(175, 175), .1f);
        _homePageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(175, 175), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 200, 0), .2f);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(350, 350), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _selectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(175, 175), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(175, 175), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _unSelectedImage;
        _homePage.SetActive(false);
        _teamPage.SetActive(false);
        _collectionPage.SetActive(true);
        _tournementPage.SetActive(false);
        _friendsPage.SetActive(false);

    }
    public void TournementPage()
    {

        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _homePageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(175, 175), .1f);
        _homePageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(175, 175), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.localPosition = new Vector3(0, 50, 0);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(175, 175), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 200, 0), .2f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(350, 350), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _selectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(175, 175), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _unSelectedImage;
        _homePage.SetActive(false);
        _teamPage.SetActive(false);
        _collectionPage.SetActive(false);
        _tournementPage.SetActive(true);
        _friendsPage.SetActive(false);

    }
    public void FriendsPage()
    {

        _homePageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _homePageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtHome = (RectTransform)_homePageButton.GetChild(2).transform;
        rtHome.DOSizeDelta(new Vector2(175, 175), .1f);
        _homePageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _teamPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _teamPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTeam = (RectTransform)_teamPageButton.GetChild(2).transform;
        rtTeam.DOSizeDelta(new Vector2(175, 175), .1f);
        _teamPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _collectionPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _collectionPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtCollection = (RectTransform)_collectionPageButton.GetChild(2).transform;
        rtCollection.DOSizeDelta(new Vector2(175, 175), .1f);
        _collectionPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _tournementPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 215 / 1262, 470);
        _tournementPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 50, 0), .1f);
        RectTransform rtTournement = (RectTransform)_tournementPageButton.GetChild(2).transform;
        rtTournement.DOSizeDelta(new Vector2(175, 175), .1f);
        _tournementPageButton.GetComponent<Image>().sprite = _unSelectedImage;

        _friendsPageButton.sizeDelta = new Vector2(_UICanvas.rect.width * 405 / 1262, 470);
        _friendsPageButton.GetChild(2).transform.DOLocalMove(new Vector3(0, 200, 0), .2f);
        RectTransform rtFriend = (RectTransform)_friendsPageButton.GetChild(2).transform;
        rtFriend.DOSizeDelta(new Vector2(350, 350), .1f);
        _friendsPageButton.GetComponent<Image>().sprite = _selectedImage;
        _homePage.SetActive(false);
        _teamPage.SetActive(false);
        _collectionPage.SetActive(false);
        _tournementPage.SetActive(false);
        _friendsPage.SetActive(true);
        _friendsPanelControlScript.GetComponent<FriendsPanelScript>().PlayerPanelButton();
    }



}
