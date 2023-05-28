﻿using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

using FullSerializer;

using GameVanilla.Core;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;
using GameVanilla.Game.UI;
using UnityEngine.SocialPlatforms.Impl;
using System.Text.RegularExpressions;

using Photon.Pun;

using DG.Tweening;
using Unity.VisualScripting;

namespace GameVanilla.Game.Common
{
    public class GameBoard : MonoBehaviour
    {
#pragma warning disable 649
        [SerializeField]
        private GameScene gameScene;

        [SerializeField]
        private GameUi gameUi;

        [SerializeField]
        private BoosterBar boosterBar;

        [SerializeField]
        private TilePool tilePool;

        [SerializeField]
        public FxPool fxPool;

        [SerializeField]
        private Transform boardCenter;

        [SerializeField]
        private List<AudioClip> gameSounds;
#pragma warning restore 649

        [HideInInspector]
        public Level level;

        [HideInInspector]
        public GameState gameState = new GameState();

        [HideInInspector]
        public int currentLimit;

        public List<GameObject> tiles;
        private List<GameObject> honeys;
        private List<GameObject> ices;
        private List<GameObject> syrups1;
        private List<GameObject> syrups2;

        private readonly List<Vector3> tilePositions = new List<Vector3>();

        private bool drag;
        private GameObject selectedTile;

        private List<Swap> possibleSwaps = new List<Swap>();

        private float tileW;
        private float tileH;

        private GameObject lastSelectedTile;
        private int lastSelectedTileX;
        private int lastSelectedTileY;
        private CandyColor lastSelectedCandyColor;

        private GameObject lastOtherSelectedTile;
        private int lastOtherSelectedTileX;
        private int lastOtherSelectedTileY;
        private CandyColor lastOtherSelectedCandyColor;

        private GameObject birinciTile;
        private GameObject ikinciTile;

        private GameConfiguration gameConfig;

        private readonly ComboDetector comboDetector = new ComboDetector();

        private readonly List<GameObject> suggestedMatch = new List<GameObject>();
        private Coroutine suggestedMatchCoroutine;

        private Coroutine countdownCoroutine;

        private bool currentlyAwarding;
        public bool CurrentlyAwarding
        {
            get { return currentlyAwarding; }
        }

        private bool inputLocked;

        private readonly List<CollectableType> eligibleCollectables = new List<CollectableType>();

        private bool explodedChocolate;

        private class Swap
        {
            public GameObject tileA;
            public GameObject tileB;
        }

        private enum SwapDirection
        {
            Horizontal,
            Vertical
        }

        private SwapDirection swapDirection;

        private bool currentlySwapping;

        private readonly MatchDetector horizontalMatchDetector = new HorizontalMatchDetector();
        private readonly MatchDetector verticalMatchDetector = new VerticalMatchDetector();
        private readonly MatchDetector tShapedMatchDetector = new TshapedMatchDetector();
        private readonly MatchDetector lShapedMatchDetector = new LshapedMatchDetector();

        private int consecutiveCascades;

        public int _kalanLimit;

        public static GameBoard instance;

        public bool _colorBombAktif;

        public List<int> _rakipTileListem = new List<int>();
        public List<int> _rakipRegenTileListem = new List<int>();


        [Header("PVP Objeleri")]

        [SerializeField] GameObject _handObject;
        [SerializeField] GameObject _siraDegisikligiPaneli;
        [SerializeField] GameObject _siraDegisikligiPanelBG;

        private int _tileListeSiram,_rakipGravityTileSiram;
        private bool _tekSefer;
        private bool _hamleBasladi, _hamleBitti;
        private int _hamleAdedi,_birinciTileIdx, _ikinciTileIdx,_comboTileIdx;
        private List<int> _rakipGravityTiles = new List<int>();

        public bool _hamleSirasi, _dokunmaAktif;

        PhotonView _pView;

        int deneme,deneme2;


        /// <summary>
        /// Unity's Awake method.
        /// </summary>
        private void Awake()
        {
            _dokunmaAktif = true;
            if (PhotonNetwork.IsConnected)
            {
                _tekSefer = false;
                _handObject.SetActive(false);
                _handObject.transform.position = new Vector3(0 , 0, 0);
                _hamleBasladi = false;
                _hamleBitti = false;
                _pView = GetComponent<PhotonView>();
                if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
                {
                    _hamleSirasi = true;
                    _hamleAdedi = 2;
                }
                else
                {
                    _hamleSirasi = false;
                    _hamleAdedi = 2;
                }
            }
            else
            {
                _hamleSirasi = true;
            }

            Assert.IsNotNull(gameScene);
            Assert.IsNotNull(gameUi);
            Assert.IsNotNull(boosterBar);

            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }
            instance = this;
        }

        /// <summary>
        /// Unity's Start method.
        /// </summary>
        private void Start()
        {
            _tileListeSiram = 0;
            _rakipGravityTileSiram =0;
            SoundManager.instance.AddSounds(gameSounds);
        }

        private void Update()
        {
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
                {
                    return;
                }
                else
                {
                    if (_rakipTileListem.Count == 81 && !_tekSefer)
                    {
                        _tekSefer = true;
                        RakipTahtaDizebilir();
                    }
                    else
                    {

                    }
                }
            }
            else
            {

            }
        }

        [PunRPC]
        public void ListeleriEsitle(int _colorType)
        {
            _rakipTileListem.Add(_colorType);
        }

        public void RakipTahtaDizebilir()
        {
            GameObject.Find("GameScene").GetComponent<GameScene>().TahtaStart();
        }
        /// <summary>
        /// Unity's OnDestroy method.
        /// </summary>
        protected void OnDestroy()
        {
            SoundManager.instance.RemoveSounds(gameSounds);
        }

        /// <summary>
        /// Initializes the game's object pools.
        /// </summary>
        public void InitializeObjectPools()
        {
            foreach (var pool in tilePool.GetComponentsInChildren<ObjectPool>())
                pool.Initialize();

            foreach (var pool in fxPool.GetComponentsInChildren<ObjectPool>())
                pool.Initialize();
        }

        /// <summary>
        /// Loads the current level.
        /// </summary>
        public void LoadLevel()
        {
            var serializer = new fsSerializer();
            gameConfig = FileUtils.LoadJsonFile<GameConfiguration>(serializer, "game_configuration");

            ResetLevelData();
        }

        /// <summary>
        /// Updates the score of the current game.
        /// </summary>
        /// <param name="score">The score.</param>
        private void UpdateScore(int score)
        {
            gameState.score += score;
            gameUi.SetScore(gameState.score);
            gameUi.SetProgressBar(gameState.score);
            if (PhotonNetwork.IsConnected && _hamleSirasi)   
            {
                _pView.RPC("PVPUpdateScore",RpcTarget.All,(int)gameState.score);
            }
        }

        [PunRPC]
        public void PVPUpdateScore(int score)
        {
            if(_hamleSirasi)   
            {
                GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerScoreText.text = score.ToString();
            }
            else
            {
                GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerScoreText.text = score.ToString();
            }
        }

        /// <summary>
        /// Resets the current level data.
        /// </summary>
        public void ResetLevelData()
        {
            var serializer = new fsSerializer();
            level = FileUtils.LoadJsonFile<Level>(serializer,
                "Levels/" + PuzzleMatchManager.instance.lastSelectedLevel);

            boosterBar.SetData(level);

            if (suggestedMatchCoroutine != null)
            {
                StopCoroutine(suggestedMatchCoroutine);
                suggestedMatchCoroutine = null;
            }

            ClearSuggestedMatch();

            tiles = new List<GameObject>(level.width * level.height);
            honeys = new List<GameObject>(level.width * level.height);
            ices = new List<GameObject>(level.width * level.height);
            syrups1 = new List<GameObject>(level.width * level.height);
            syrups2 = new List<GameObject>(level.width * level.height);

            eligibleCollectables.Clear();
            foreach (var goal in level.goals)
            {
                if (goal is CollectCollectableGoal)
                {
                    var collectableGoal = goal as CollectCollectableGoal;
                    for (var i = 0; i < collectableGoal.amount; i++)
                    {
                        eligibleCollectables.Add(collectableGoal.collectableType);
                    }
                }
            }

            currentLimit = level.limit;
            currentlyAwarding = false;

            consecutiveCascades = 0;

            explodedChocolate = false;

            gameState.Reset();

            gameUi.SetLimitType(level.limitType);
            gameUi.SetLimit(level.limit);
            gameUi.SetGoals(level.goals, true);
            gameUi.InitializeProgressBar(level.score1, level.score2, level.score3);
            UpdateScore(0);

            foreach (var pool in tilePool.GetComponentsInChildren<ObjectPool>())
            {
                pool.Reset();
            }

            foreach (var pool in fxPool.GetComponentsInChildren<ObjectPool>())
            {
                pool.Reset();
            }

            tilePositions.Clear();
            possibleSwaps.Clear();

            const float horizontalSpacing = 0.0f;
            const float verticalSpacing = 0.0f;
            if (PhotonNetwork.IsConnected)
            {
                if (PhotonNetwork.MasterClient.NickName == PhotonNetwork.NickName)
                {
                    for (var j = 0; j < level.height; j++)
                    {
                        for (var i = 0; i < level.width; i++)
                        {
                            var levelTile = level.tiles[i + (j * level.width)];
                            var tile = CreateTileFromLevel(levelTile, i, j);
                            if (tile != null)
                            {
                                var spriteRenderer = tile.GetComponent<SpriteRenderer>();
                                tileW = spriteRenderer.bounds.size.x;
                                tileH = spriteRenderer.bounds.size.y;
                                //tileW = 20;
                                //tileH = 20;
                                tile.transform.position =
                                    new Vector2(i * (tileW + horizontalSpacing), -j * (tileH + verticalSpacing));

                                var collectable = tile.GetComponent<Collectable>();
                                if (collectable != null)
                                {
                                    var cidx = eligibleCollectables.FindIndex(x => x == collectable.type);
                                    if (cidx != -1)
                                    {
                                        eligibleCollectables.RemoveAt(cidx);
                                    }

                                }
                            }
                            GetComponent<PhotonView>().RPC("ListeleriEsitle", RpcTarget.OthersBuffered, (int)tile.GetComponent<Candy>().color);
                            tiles.Add(tile);
                        }
                    }

                }
                else
                {
                    for (var j = 0; j < level.height; j++)
                    {
                        for (var i = 0; i < level.width; i++)
                        {
                            var levelTile = level.tiles[i + (j * level.width)];

                            var tile = PVPRakipCreatTile(levelTile, i, j);
                            if (tile != null)
                            {
                                var spriteRenderer = tile.GetComponent<SpriteRenderer>();
                                tileW = spriteRenderer.bounds.size.x;
                                tileH = spriteRenderer.bounds.size.y;
                                //tileW = 20;
                                //ileH = 20;
                                tile.transform.position =
                                    new Vector2(i * (tileW + horizontalSpacing), -j * (tileH + verticalSpacing));

                                var collectable = tile.GetComponent<Collectable>();
                                if (collectable != null)
                                {
                                    var cidx = eligibleCollectables.FindIndex(x => x == collectable.type);
                                    if (cidx != -1)
                                    {
                                        eligibleCollectables.RemoveAt(cidx);
                                    }

                                }
                            }
                            tiles.Add(tile);
                        }
                    }

                }
            }
            else
            {
                for (var j = 0; j < level.height; j++)
                {
                    for (var i = 0; i < level.width; i++)
                    {
                        var levelTile = level.tiles[i + (j * level.width)];
                        var tile = CreateTileFromLevel(levelTile, i, j);
                        if (tile != null)
                        {
                            var spriteRenderer = tile.GetComponent<SpriteRenderer>();
                            tileW = spriteRenderer.bounds.size.x;
                            tileH = spriteRenderer.bounds.size.y;

                            tile.transform.position =
                                new Vector2(i * (tileW + horizontalSpacing), -j * (tileH + verticalSpacing));

                            var collectable = tile.GetComponent<Collectable>();
                            if (collectable != null)
                            {
                                var cidx = eligibleCollectables.FindIndex(x => x == collectable.type);
                                if (cidx != -1)
                                {
                                    eligibleCollectables.RemoveAt(cidx);
                                }

                            }
                        }
                        tiles.Add(tile);
                    }
                }

            }
            // GAME BOARD EKRANININ YUKSEKLIGI VE GENISLIGI BURADAN AYARLANIYOR !!!!!!!
            var totalWidth = (level.width - (1f)) * (tileW + horizontalSpacing);
            var totalHeight = (level.height - (1f)) * (tileH + verticalSpacing);
            for (var j = 0; j < level.height; j++)
            {

                for (var i = 0; i < level.width; i++)
                {
                    var tilePos = new Vector2(i * (tileW + horizontalSpacing), -j * (tileH + verticalSpacing));
                    var newPos = tilePos;
                    newPos.x -= (totalWidth / 2);
                    if (PhotonNetwork.IsConnected)
                    {
                        newPos.y += (totalHeight / 2) - 53.5f;
                    }
                    else
                    {
                        newPos.y += (totalHeight / 2) - 12.5f;
                    }
                    newPos.y += boardCenter.position.y;
                    var tile = tiles[i + (j * level.width)];
                    if (tile != null)
                    {
                        tile.transform.position = newPos;
                    }

                    tilePositions.Add(newPos);

                    var levelTile = level.tiles[i + (j * level.width)];
                    if (!(levelTile is HoleTile))
                    {
                        GameObject bgTile;
                        if (j % 2 == 0)
                        {
                            bgTile = i % 2 == 0
                                ? tilePool.darkBgTilePool.GetObject()
                                : tilePool.lightBgTilePool.GetObject();
                        }
                        else
                        {
                            bgTile = i % 2 == 0
                                ? tilePool.lightBgTilePool.GetObject()
                                : tilePool.darkBgTilePool.GetObject();
                        }

                        bgTile.transform.position = newPos;
                    }
                }
            }

            for (var j = 0; j < level.height; j++)
            {
                for (var i = 0; i < level.width; i++)
                {
                    var levelTile = level.tiles[i + (j * level.width)];
                    if (levelTile != null && levelTile.elementType == ElementType.Honey)
                    {
                        var honey = tilePool.honeyPool.GetObject();
                        honey.transform.position = tilePositions[i + (j * level.width)];
                        honey.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        honeys.Add(honey);
                        ices.Add(null);
                        syrups1.Add(null);
                        syrups2.Add(null);
                    }
                    else if (levelTile != null && levelTile.elementType == ElementType.Ice)
                    {
                        var ice = tilePool.icePool.GetObject();
                        ice.transform.position = tilePositions[i + (j * level.width)];
                        ice.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        ices.Add(ice);
                        honeys.Add(null);
                        syrups1.Add(null);
                        syrups2.Add(null);
                    }
                    else if (levelTile != null && levelTile.elementType == ElementType.Syrup1)
                    {
                        var syrup = tilePool.syrup1Pool.GetObject();
                        syrup.transform.position = tilePositions[i + (j * level.width)];
                        syrup.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        ices.Add(null);
                        honeys.Add(null);
                        syrups1.Add(syrup);
                        syrups2.Add(null);
                    }
                    else if (levelTile != null && levelTile.elementType == ElementType.Syrup2)
                    {
                        var syrup = tilePool.syrup2Pool.GetObject();
                        syrup.transform.position = tilePositions[i + (j * level.width)];
                        syrup.GetComponent<SpriteRenderer>().sortingOrder = -1;
                        ices.Add(null);
                        honeys.Add(null);
                        syrups1.Add(null);
                        syrups2.Add(syrup);
                    }
                    else
                    {
                        honeys.Add(null);
                        ices.Add(null);
                        syrups1.Add(null);
                        syrups2.Add(null);
                    }
                }
            }

            var zoomLevel = gameConfig.GetZoomLevel();
            // Camera.main.orthographicSize = ((totalWidth * zoomLevel) * (Screen.height / (float)Screen.width) * 0.5f);

            if (Screen.height / Screen.width >= 2f)
            {
                Camera.main.orthographicSize = 160 * level.width / 7;
            }
            else
            {
                if (PhotonNetwork.IsConnected)
                {
                    Camera.main.orthographicSize = 155 * level.width / 7;
                }
                else
                {
                    Camera.main.orthographicSize = 120 * level.width / 7;
                }
            }
            //Camera.main.orthographicSize = 140/(Screen.width/1284)*1.25f;
            possibleSwaps = DetectPossibleSwaps();
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void StartGame()
        {
            if (!PhotonNetwork.IsConnected)
            {
                if (level.limitType == LimitType.Time)
                {
                    countdownCoroutine = StartCoroutine(StartCountdown());
                }
            }
            suggestedMatchCoroutine = StartCoroutine(HighlightRandomMatchAsync());
        }

        /// <summary>
        /// Ends the current game.
        /// </summary>
        public void EndGame()
        {
            if (countdownCoroutine != null)
            {
                StopCoroutine(countdownCoroutine);
            }
        }

        /// <summary>
        /// Continues the current game.
        /// </summary>
        public void Continue()
        {
            if (level.limitType == LimitType.Moves)
            {
                currentLimit = gameConfig.numExtraMoves;
                gameUi.SetLimit(currentLimit);
            }
            else if (level.limitType == LimitType.Time)
            {
                currentLimit = gameConfig.numExtraTime;
                gameUi.SetLimit(currentLimit);
                countdownCoroutine = StartCoroutine(StartCountdown());
            }
        }

        /// <summary>
        /// Starts the level countdown (used only in time-limited levels).
        /// </summary>
        /// <returns>The coroutine.</returns>
        private IEnumerator StartCountdown()
        {
            while (currentLimit > 0)
            {
                --currentLimit;
                UpdateLimitText();
                yield return new WaitForSeconds(1.0f);
            }

            gameScene.CheckEndGame();
        }

        /// <summary>
        /// Updates the limit text.
        /// </summary>
        private void UpdateLimitText()
        {
            if (level.limitType == LimitType.Moves)
            {
                gameUi.SetLimit(currentLimit);
            }
            else if (level.limitType == LimitType.Time)
            {
                var timeSpan = TimeSpan.FromSeconds(currentLimit);
                gameUi.SetLimit(string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds));
            }
        }

        /// <summary>
        /// Handles the player's input.
        /// </summary>
        ///
        public void PVPHandleInputAktivate()
        {
            if (_hamleSirasi)
            {
                HandleInput();
            }
            else
            {

            }
        }

        public void HandleInput()
        {
            if (inputLocked)
            {
                return;
            }
            if (currentlySwapping)
            {
                return;
            }
            if (currentlyAwarding)
            {
                return;
            }
            if ((Input.GetMouseButtonDown(0)&& _hamleSirasi && _dokunmaAktif))
            {
                MoreMountains.NiceVibrations.MMVibrationManager.Haptic(MoreMountains.NiceVibrations.HapticTypes.MediumImpact);

                drag = true;
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Tile"))
                {
                    var idx = tiles.FindIndex(x => x == hit.collider.gameObject);
                    if (level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Ice)
                    {
                        return;
                    }
                    if (hit.collider.GetComponent<SpecialBlock>() != null)
                    {
                        return;
                    }
                    selectedTile = hit.collider.gameObject;
                    birinciTile = selectedTile;
                    _birinciTileIdx = idx;
                    _comboTileIdx = idx;
                    selectedTile.GetComponent<Animator>().SetTrigger("Pressed");
                }
            }

            if ((Input.GetMouseButtonUp(0) && _hamleSirasi && _dokunmaAktif))
            {
                drag = false;
                if (selectedTile != null && selectedTile.GetComponent<Animator>() != null && selectedTile.gameObject.activeSelf)
                {
                    selectedTile.GetComponent<Animator>().SetTrigger("Unpressed");

                    if (selectedTile.GetComponent<StripedCandy>() != null && gameScene._boosterAktif == false && gameScene._boosterColorBombAktif == false)
                    {
                        ExplodeTile(selectedTile);
                        if (PhotonNetwork.IsConnected && _hamleSirasi)
                        {
                            _pView.RPC("RakipStripedCandy", RpcTarget.Others, (int)_comboTileIdx);
                        }
                        else
                        {

                        }

                    }
                    else if (selectedTile.GetComponent<WrappedCandy>() != null && gameScene._boosterAktif == false && gameScene._boosterColorBombAktif == false)
                    {
                        ExplodeTile(selectedTile);
                        if (PhotonNetwork.IsConnected && _hamleSirasi)
                        {
                            _pView.RPC("RakipWrappedCandy", RpcTarget.Others, (int)_comboTileIdx);
                        }
                        else
                        {

                        }
                        ApplyGravity();
                        
                    }
                    else if (selectedTile.GetComponent<ColorBomb>() != null && gameScene._boosterAktif == false && gameScene._boosterColorBombAktif == false)
                    {
                        ColorBombPatlat(selectedTile);
                        
                    }
                    else
                    {
                    }
                }
            }

            if (drag && selectedTile != null)
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject != selectedTile)
                {
                    if (hit.collider.GetComponent<SpecialBlock>() != null)
                    {
                        return;
                    }
                    if (selectedTile.GetComponent<Animator>() != null && selectedTile.gameObject.activeSelf)
                    {
                        selectedTile.GetComponent<Animator>().SetTrigger("Unpressed");
                    }
                    var idx = tiles.FindIndex(x => x == hit.collider.gameObject);
                    if (level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Ice)
                    {
                        return;
                    }
                    _ikinciTileIdx = idx;
                    ikinciTile = hit.collider.gameObject;

                    var idxSelected = tiles.FindIndex(x => x == selectedTile);
                    var xSelected = idxSelected % level.width;
                    var ySelected = idxSelected / level.width;
                    var idxNew = tiles.FindIndex(x => x == hit.collider.gameObject);
                    var xNew = idxNew % level.width;
                    var yNew = idxNew / level.width;
                    if (Math.Abs(xSelected - xNew) > 1 || Math.Abs(ySelected - yNew) > 1)
                    {
                        return;
                    }
                    if (Math.Abs(xSelected - xNew) == 1 && Math.Abs(ySelected - yNew) == 1)
                    {
                        return;
                    }
                    var combo = comboDetector.GetCombo(hit.collider.gameObject.GetComponent<Tile>(),
                        selectedTile.GetComponent<Tile>());
                    if (combo != null)
                    {
                        Debug.Log("COMBO OKUNDU_1");
                        var selectedTileCopy = selectedTile;
                        selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        currentlySwapping = true;
                        LeanTween.move(selectedTile, hit.collider.gameObject.transform.position, 0.2f).setOnComplete(
                            () =>
                            {
                                currentlySwapping = false;
                                selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                combo.Resolve(this, tiles, fxPool);
                            });
                        LeanTween.move(hit.collider.gameObject, selectedTile.transform.position, 0.2f);

                        var tileA = hit.collider.gameObject;
                        var tileB = selectedTile;
                        var idxA = tiles.FindIndex(x => x == tileA);
                        var idxB = tiles.FindIndex(x => x == tileB);
                        tiles[idxA] = tileB;
                        tiles[idxB] = tileA;

                        tileA.GetComponent<Tile>().x = idxB % level.width;
                        tileA.GetComponent<Tile>().y = idxB / level.width;
                        tileB.GetComponent<Tile>().x = idxA % level.width;
                        tileB.GetComponent<Tile>().y = idxA / level.width;

                        lastSelectedTile = selectedTile;
                        lastSelectedTileX = idxA % level.width;
                        lastSelectedTileY = idxA / level.width;

                        lastOtherSelectedTile = hit.collider.gameObject;
                        lastOtherSelectedTileX = idxB % level.width;
                        lastOtherSelectedTileY = idxB / level.width;


                        selectedTile = null;

                        PerformMove();
                    }
                    else if (possibleSwaps.Find(x => x.tileA == hit.collider.gameObject && x.tileB == selectedTile) !=
                             null ||
                             possibleSwaps.Find(x => x.tileB == hit.collider.gameObject && x.tileA == selectedTile) !=
                             null)
                    {
                        Debug.Log("COMBO OKUNDU_2");

                        var selectedTileCopy = selectedTile;
                        selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        currentlySwapping = true;
                        LeanTween.move(selectedTile, hit.collider.gameObject.transform.position, 0.2f).setOnComplete(
                            () =>
                            {
                                currentlySwapping = false;
                                selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                HandleMatches(true);
                            });
                        LeanTween.move(hit.collider.gameObject, selectedTile.transform.position, 0.2f);

                        var tileA = hit.collider.gameObject;
                        var tileB = selectedTile;
                        var idxA = tiles.FindIndex(x => x == tileA);
                        var idxB = tiles.FindIndex(x => x == tileB);
                        tiles[idxA] = tileB;
                        tiles[idxB] = tileA;

                        if (tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x)
                        {
                            swapDirection = SwapDirection.Horizontal;
                        }
                        else
                        {
                            swapDirection = SwapDirection.Vertical;
                        }

                        tileA.GetComponent<Tile>().x = idxB % level.width;
                        tileA.GetComponent<Tile>().y = idxB / level.width;
                        tileB.GetComponent<Tile>().x = idxA % level.width;
                        tileB.GetComponent<Tile>().y = idxA / level.width;

                        lastSelectedTile = selectedTile;
                        lastSelectedTileX = idxA % level.width;
                        lastSelectedTileY = idxA / level.width;
                        if (selectedTileCopy.GetComponent<Candy>() != null)
                        {
                            lastSelectedCandyColor = selectedTile.GetComponent<Candy>().color;
                            //lastSelectedCandyColor = CandyColor.Black;
                        }

                        lastOtherSelectedTile = hit.collider.gameObject;
                        lastOtherSelectedTileX = idxB % level.width;
                        lastOtherSelectedTileY = idxB / level.width;
                        if (hit.collider.gameObject.GetComponent<Candy>() != null)
                        {
                            lastOtherSelectedCandyColor = hit.collider.gameObject.GetComponent<Candy>().color;
                            //lastOtherSelectedCandyColor = CandyColor.Black;
                        }

                        if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null)
                        {
                            StartCoroutine(YeniPatlamaGameObject(selectedTile));
                        }
                        else if (ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                        {
                            StartCoroutine(YeniPatlamaGameObject(ikinciTile));
                        }
                        else
                        {

                        }

                        selectedTile = null;

                        possibleSwaps = DetectPossibleSwaps();

                        PerformMove();
                    }
                    else if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null || ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                    {
                        Debug.Log("COMBO OKUNDU_3");
                        var selectedTileCopy = selectedTile;
                        selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        currentlySwapping = true;
                        LeanTween.move(selectedTile, hit.collider.gameObject.transform.position, 0.2f).setOnComplete(
                            () =>
                            {
                                currentlySwapping = false;
                                selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                HandleMatches(true);
                            });
                        LeanTween.move(hit.collider.gameObject, selectedTile.transform.position, 0.2f);

                        var tileA = hit.collider.gameObject;
                        var tileB = selectedTile;
                        var idxA = tiles.FindIndex(x => x == tileA);
                        var idxB = tiles.FindIndex(x => x == tileB);
                        tiles[idxA] = tileB;
                        tiles[idxB] = tileA;

                        if (tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x)
                        {
                            swapDirection = SwapDirection.Horizontal;
                        }
                        else
                        {
                            swapDirection = SwapDirection.Vertical;
                        }

                        tileA.GetComponent<Tile>().x = idxB % level.width;
                        tileA.GetComponent<Tile>().y = idxB / level.width;
                        tileB.GetComponent<Tile>().x = idxA % level.width;
                        tileB.GetComponent<Tile>().y = idxA / level.width;

                        lastSelectedTile = selectedTile;
                        lastSelectedTileX = idxA % level.width;
                        lastSelectedTileY = idxA / level.width;
                        if (selectedTileCopy.GetComponent<Candy>() != null)
                        {
                            lastSelectedCandyColor = selectedTile.GetComponent<Candy>().color;
                            //lastSelectedCandyColor = CandyColor.Black;
                        }

                        lastOtherSelectedTile = hit.collider.gameObject;
                        lastOtherSelectedTileX = idxB % level.width;
                        lastOtherSelectedTileY = idxB / level.width;
                        if (hit.collider.gameObject.GetComponent<Candy>() != null)
                        {
                            lastOtherSelectedCandyColor = hit.collider.gameObject.GetComponent<Candy>().color;
                            //lastOtherSelectedCandyColor = CandyColor.Black;
                        }

                        if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null)
                        {
                            StartCoroutine(YeniPatlamaGameObject(selectedTile));
                        }
                        else if (ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                        {
                            StartCoroutine(YeniPatlamaGameObject(ikinciTile));
                        }
                        else
                        {

                        }

                        //StartCoroutine(YeniPatlamaGameObject(selectedTile));

                        selectedTile = null;

                        possibleSwaps = DetectPossibleSwaps();

                        PerformMove();
                    }
                    else  // TAS HAREKET EDEBİLECEK BİR HAREKET YAPMADIYSA TASI ALDIGIN YERİNE GERİ KOYMAK ICIN
                    {
                        Debug.Log("COMBO OKUNDU_4");
                        var selectedTileCopy = selectedTile;
                        var hitTileCopy = hit.collider.gameObject;
                        selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;

                        var selectedTilePos = tilePositions[tiles.FindIndex(x => x == selectedTile)];
                        var hitTilePos = tilePositions[tiles.FindIndex(x => x == hit.collider.gameObject)];

                        var tileA = hit.collider.gameObject;
                        var tileB = selectedTile;
                        if (!(tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x &&
                              tileA.GetComponent<Tile>().y != tileB.GetComponent<Tile>().y))
                        {
                            currentlySwapping = true;
                            LeanTween.move(selectedTile, hitTilePos, 0.15f);
                            LeanTween.move(hit.collider.gameObject, selectedTilePos, 0.15f).setOnComplete(() =>
                            {
                                LeanTween.move(selectedTileCopy, selectedTilePos, 0.15f).setOnComplete(() =>
                                {
                                    currentlySwapping = false;
                                    selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                    selectedTileCopy.transform.rotation = Quaternion.identity;
                                    hit.collider.gameObject.transform.rotation = Quaternion.identity;
                                });
                                LeanTween.move(hitTileCopy, hitTilePos, 0.15f);
                            });
                        }

                        selectedTile = null;

                        SoundManager.instance.PlaySound("Error");
                    }
                }
            }
        }


        [PunRPC]
        public void RakipStripedCandy(int _tileIdx)
        {
            ExplodeTile(tiles[_tileIdx]);
            StartCoroutine(RakipStripedCandyAsyncGravity());
        }

        [PunRPC]
        public void RakipWrappedCandy(int _tileIdx)
        {
            ExplodeTile(tiles[_tileIdx]);
            StartCoroutine(RakipWrappedCandyAsyncGravity());
            ApplyGravity();
        }

        private IEnumerator RakipStripedCandyAsyncGravity()
        {
            yield return new WaitForSeconds(.5f);
            ApplyGravity();
        }
        private IEnumerator RakipWrappedCandyAsyncGravity()
        {
            yield return new WaitForSeconds(.5f);
            ApplyGravity();
        }



        ///<summary>
        ///PVP RAKİP TAŞ HAREKETLERİ
        ///<summary>
        [PunRPC]
        public void RakipTasHareket()
        {
            selectedTile = tiles[_birinciTileIdx];
            birinciTile = selectedTile;
            ikinciTile= tiles[_ikinciTileIdx];
            if (selectedTile != null)
            {
                var idxSelected = tiles.FindIndex(x => x == selectedTile);
                var xSelected = idxSelected % level.width;
                var ySelected = idxSelected / level.width;
                var idxNew = tiles.FindIndex(x => x == ikinciTile);
                var xNew = idxNew % level.width;
                var yNew = idxNew / level.width;
                if (Math.Abs(xSelected - xNew) > 1 || Math.Abs(ySelected - yNew) > 1)
                {
                    return;
                }
                if (Math.Abs(xSelected - xNew) == 1 && Math.Abs(ySelected - yNew) == 1)
                {
                    return;
                }
                var combo = comboDetector.GetCombo(ikinciTile.GetComponent<Tile>(),
                    selectedTile.GetComponent<Tile>());
                if (combo != null)
                {
                    Debug.Log("RAKIP_COMBO OKUNDU_1");
                    var selectedTileCopy = selectedTile;
                    selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    currentlySwapping = true;
                    LeanTween.move(selectedTile, ikinciTile.transform.position, 0.2f).setOnComplete(
                        () =>
                        {
                            currentlySwapping = false;
                            selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            combo.Resolve(this, tiles, fxPool);
                        });
                    LeanTween.move(ikinciTile, selectedTile.transform.position, 0.2f);

                    var tileA = ikinciTile;
                    var tileB = selectedTile;
                    var idxA = tiles.FindIndex(x => x == tileA);
                    var idxB = tiles.FindIndex(x => x == tileB);
                    tiles[idxA] = tileB;
                    tiles[idxB] = tileA;

                    tileA.GetComponent<Tile>().x = idxB % level.width;
                    tileA.GetComponent<Tile>().y = idxB / level.width;
                    tileB.GetComponent<Tile>().x = idxA % level.width;
                    tileB.GetComponent<Tile>().y = idxA / level.width;

                    lastSelectedTile = selectedTile;
                    lastSelectedTileX = idxA % level.width;
                    lastSelectedTileY = idxA / level.width;

                    lastOtherSelectedTile = ikinciTile;
                    lastOtherSelectedTileX = idxB % level.width;
                    lastOtherSelectedTileY = idxB / level.width;


                    selectedTile = null;

                }
                else if (possibleSwaps.Find(x => x.tileA == ikinciTile && x.tileB == selectedTile) !=
                         null ||
                         possibleSwaps.Find(x => x.tileB == ikinciTile && x.tileA == selectedTile) !=
                         null)
                {
                    Debug.Log("RAKIP_COMBO OKUNDU_2");

                    var selectedTileCopy = selectedTile;
                    selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    currentlySwapping = true;
                    LeanTween.move(selectedTile, ikinciTile.transform.position, 0.2f).setOnComplete(
                        () =>
                        {
                            currentlySwapping = false;
                            selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            HandleMatches(true);
                        });
                    LeanTween.move(ikinciTile, selectedTile.transform.position, 0.2f);

                    var tileA = ikinciTile;
                    var tileB = selectedTile;
                    var idxA = tiles.FindIndex(x => x == tileA);
                    var idxB = tiles.FindIndex(x => x == tileB);
                    tiles[idxA] = tileB;
                    tiles[idxB] = tileA;

                    if (tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x)
                    {
                        swapDirection = SwapDirection.Horizontal;
                    }
                    else
                    {
                        swapDirection = SwapDirection.Vertical;
                    }

                    tileA.GetComponent<Tile>().x = idxB % level.width;
                    tileA.GetComponent<Tile>().y = idxB / level.width;
                    tileB.GetComponent<Tile>().x = idxA % level.width;
                    tileB.GetComponent<Tile>().y = idxA / level.width;

                    lastSelectedTile = selectedTile;
                    lastSelectedTileX = idxA % level.width;
                    lastSelectedTileY = idxA / level.width;
                    if (selectedTileCopy.GetComponent<Candy>() != null)
                    {
                        lastSelectedCandyColor = selectedTile.GetComponent<Candy>().color;
                        //lastSelectedCandyColor = CandyColor.Black;
                    }

                    lastOtherSelectedTile = ikinciTile;
                    lastOtherSelectedTileX = idxB % level.width;
                    lastOtherSelectedTileY = idxB / level.width;
                    if (ikinciTile.GetComponent<Candy>() != null)
                    {
                        lastOtherSelectedCandyColor = ikinciTile.GetComponent<Candy>().color;
                        //lastOtherSelectedCandyColor = CandyColor.Black;
                    }

                    if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null)
                    {
                        StartCoroutine(YeniPatlamaGameObject(selectedTile));
                    }
                    else if (ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                    {
                        StartCoroutine(YeniPatlamaGameObject(ikinciTile));
                    }
                    else
                    {

                    }

                    selectedTile = null;

                    possibleSwaps = DetectPossibleSwaps();

                }
                else if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null || ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                {
                    Debug.Log("RAKIP_COMBO OKUNDU_3");
                    var selectedTileCopy = selectedTile;
                    selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                    currentlySwapping = true;
                    LeanTween.move(selectedTile, ikinciTile.transform.position, 0.2f).setOnComplete(
                        () =>
                        {
                            currentlySwapping = false;
                            selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                            HandleMatches(true);
                        });
                    LeanTween.move(ikinciTile, selectedTile.transform.position, 0.2f);

                    var tileA = ikinciTile;
                    var tileB = selectedTile;
                    var idxA = tiles.FindIndex(x => x == tileA);
                    var idxB = tiles.FindIndex(x => x == tileB);
                    tiles[idxA] = tileB;
                    tiles[idxB] = tileA;

                    if (tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x)
                    {
                        swapDirection = SwapDirection.Horizontal;
                    }
                    else
                    {
                        swapDirection = SwapDirection.Vertical;
                    }

                    tileA.GetComponent<Tile>().x = idxB % level.width;
                    tileA.GetComponent<Tile>().y = idxB / level.width;
                    tileB.GetComponent<Tile>().x = idxA % level.width;
                    tileB.GetComponent<Tile>().y = idxA / level.width;

                    lastSelectedTile = selectedTile;
                    lastSelectedTileX = idxA % level.width;
                    lastSelectedTileY = idxA / level.width;
                    if (selectedTileCopy.GetComponent<Candy>() != null)
                    {
                        lastSelectedCandyColor = selectedTile.GetComponent<Candy>().color;
                        //lastSelectedCandyColor = CandyColor.Black;
                    }

                    lastOtherSelectedTile = ikinciTile;
                    lastOtherSelectedTileX = idxB % level.width;
                    lastOtherSelectedTileY = idxB / level.width;
                    if (ikinciTile.GetComponent<Candy>() != null)
                    {
                        lastOtherSelectedCandyColor = ikinciTile.GetComponent<Candy>().color;
                        //lastOtherSelectedCandyColor = CandyColor.Black;
                    }

                    if (selectedTile.GetComponent<StripedCandy>() != null || selectedTile.GetComponent<WrappedCandy>() != null)
                    {
                        StartCoroutine(YeniPatlamaGameObject(selectedTile));
                    }
                    else if (ikinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                    {
                        StartCoroutine(YeniPatlamaGameObject(ikinciTile));
                    }
                    else
                    {

                    }

                    //StartCoroutine(YeniPatlamaGameObject(selectedTile));

                    selectedTile = null;

                    possibleSwaps = DetectPossibleSwaps();

                }
                else
                {
                    Debug.Log("RAKIP_COMBO OKUNDU_4");
                    var selectedTileCopy = selectedTile;
                    var hitTileCopy = ikinciTile;
                    selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;

                    var selectedTilePos = tilePositions[tiles.FindIndex(x => x == selectedTile)];
                    var hitTilePos = tilePositions[tiles.FindIndex(x => x == ikinciTile)];

                    var tileA = ikinciTile;
                    var tileB = selectedTile;
                    if (!(tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x &&
                          tileA.GetComponent<Tile>().y != tileB.GetComponent<Tile>().y))
                    {
                        currentlySwapping = true;
                        LeanTween.move(selectedTile, hitTilePos, 0.15f);
                        LeanTween.move(ikinciTile, selectedTilePos, 0.15f).setOnComplete(() =>
                        {
                            LeanTween.move(selectedTileCopy, selectedTilePos, 0.15f).setOnComplete(() =>
                            {
                                currentlySwapping = false;
                                selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                selectedTileCopy.transform.rotation = Quaternion.identity;
                                ikinciTile.transform.rotation = Quaternion.identity;
                            });
                            LeanTween.move(hitTileCopy, hitTilePos, 0.15f);
                        });
                    }

                    selectedTile = null;

                    SoundManager.instance.PlaySound("Error");
                }

            }
        }

        /// <summary>
        /// Handles the player's input when the game is in booster mode.
        /// </summary>
        public void HandleBoosterInput(BuyBoosterButton button)
        {
            if (Input.GetMouseButtonDown(0))
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Tile") && gameScene._boosterAktif == false)
                {
                    if (hit.collider.GetComponent<Unbreakable>() != null ||
                        hit.collider.GetComponent<Collectable>() != null)
                    {
                        return;
                    }

                    var tile = hit.collider.GetComponent<Tile>();

                    Booster booster = null;
                    switch (button.boosterType)
                    {
                        case BoosterType.Lollipop:
                            booster = new LollipopBooster();
                            if (PhotonNetwork.IsConnected)
                            {
                                _pView.RPC("RakipHandleBoosterInput", RpcTarget.Others, (int)BoosterType.Lollipop, (int)tiles.FindIndex(x => x == tile.gameObject));
                            }
                            break;

                        case BoosterType.Bomb:
                            //bombbooster = new BombBooster();
                            break;

                        case BoosterType.Switch:
                            //switchbooster = new SwitchBooster();
                            break;

                        case BoosterType.ColorBomb:
                            booster = new ColorBombBooster();
                            break;
                    }

                    if (booster != null)
                    {
                        booster.Resolve(this, tile.gameObject);
                        ConsumeBooster(button);
                    }
                    else
                    {
                        if (button.boosterType == BoosterType.Bomb)
                        {   
                            if (PhotonNetwork.IsConnected)
                            {
                                _pView.RPC("RakipHandleBoosterInput", RpcTarget.Others, (int)BoosterType.Bomb, (int)tiles.FindIndex(x => x==tile.gameObject));
                            }
                            button.GetComponent<BombBooster>().Resolve(this, tile.gameObject);
                            ConsumeBooster(button);
                        }
                        else if (button.boosterType == BoosterType.Switch)
                        {
                            button.GetComponent<SwitchBooster>().Resolve(this, tile.gameObject);
                            ConsumeBooster(button);
                        }
                    }


                    gameScene.DisableBoosterMode();

                    selectedTile = hit.collider.gameObject;
                }
                else
                {
                    gameScene.DisableBoosterMode();
                }
            }
        }

        [PunRPC]
        public void RakipHandleBoosterInput(int _type,int _index) ///////RAKİP
        {
            BuyBoosterButton button;
            var tile = tiles[_index];
            if (_type == 0)
            {
                button = GameObject.Find("PlayerHammerBooster").GetComponent<BuyBoosterButton>();
                button.boosterType = BoosterType.Lollipop;
                Booster booster = null;
                booster = new LollipopBooster();
                booster.Resolve(this, tile.gameObject);
                ConsumeBooster(button);
            }
            else if(_type == 1)
            {
                button = GameObject.Find("PlayerSkillButton").GetComponent<BuyBoosterButton>();
                button.boosterType = BoosterType.Bomb;
                button.GetComponent<BombBooster>().Resolve(this, tile.gameObject);
                ConsumeBooster(button);
            }
            else
            {

            }

        }


        public void BoosterModdanCik()
        {
            gameScene._boosterAktif = false;
            gameScene._boosterColorBombAktif = false;
        }

        public void BoosterModaGir()
        {
            gameScene._boosterAktif = true;
        }

        /// <summary>
        /// Handles the player's input when the game is in booster mode and the booster used is the switch.
        /// </summary>
        public void HandleSwitchBoosterInput(BuyBoosterButton button) //bu herhangi bir yerde kullanılmıyor-yazan kuntay----> inceleyebilir misin -->burhan
        {
            if (Input.GetMouseButtonDown(0))
            {
                drag = true;
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject.CompareTag("Tile"))
                {
                    selectedTile = hit.collider.gameObject;
                }
                else
                {
                    gameScene.DisableBoosterMode();
                    return;
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                drag = false;
            }

            if (drag && selectedTile != null)
            {
                var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject != selectedTile)
                {
                    var tileA = hit.collider.gameObject;
                    var tileB = selectedTile;

                    var idxSelected = tiles.FindIndex(x => x == selectedTile);
                    var xSelected = idxSelected % level.width;
                    var ySelected = idxSelected / level.width;
                    var idxNew = tiles.FindIndex(x => x == hit.collider.gameObject);
                    var xNew = idxNew % level.width;
                    var yNew = idxNew / level.width;
                    if (Math.Abs(xSelected - xNew) > 1 || Math.Abs(ySelected - yNew) > 1)
                    {
                        return;
                    }

                    if (Math.Abs(xSelected - xNew) == 1 && Math.Abs(ySelected - yNew) == 1)
                    {
                        return;
                    }

                    if (!(tileA.GetComponent<Tile>().x != tileB.GetComponent<Tile>().x &&
                          tileA.GetComponent<Tile>().y != tileB.GetComponent<Tile>().y))
                    {
                        var selectedTileCopy = selectedTile;
                        selectedTile.GetComponent<SpriteRenderer>().sortingOrder = 1;
                        LeanTween.move(selectedTile, hit.collider.gameObject.transform.position, 0.2f).setOnComplete(
                            () =>
                            {
                                selectedTileCopy.GetComponent<SpriteRenderer>().sortingOrder = 0;
                                gameScene.DisableBoosterMode();
                                HandleMatches(true);
                                ConsumeBooster(button);
                            });
                        LeanTween.move(hit.collider.gameObject, selectedTile.transform.position, 0.2f);

                        var idxA = tiles.FindIndex(x => x == tileA);
                        var idxB = tiles.FindIndex(x => x == tileB);
                        tiles[idxA] = tileB;
                        tiles[idxB] = tileA;

                        tileA.GetComponent<Tile>().x = idxB % level.width;
                        tileA.GetComponent<Tile>().y = idxB / level.width;
                        tileB.GetComponent<Tile>().x = idxA % level.width;
                        tileB.GetComponent<Tile>().y = idxA / level.width;

                        lastSelectedTile = selectedTile;
                        lastSelectedTileX = idxA % level.width;
                        lastSelectedTileY = idxA / level.width;
                        if (selectedTileCopy.GetComponent<Candy>() != null)
                        {
                            lastSelectedCandyColor = selectedTile.GetComponent<Candy>().color;
                            //lastSelectedCandyColor = CandyColor.Blue;
                        }

                        lastOtherSelectedTile = hit.collider.gameObject;
                        lastOtherSelectedTileX = idxB % level.width;
                        lastOtherSelectedTileY = idxB / level.width;
                        if (hit.collider.gameObject.GetComponent<Candy>() != null)
                        {
                            lastOtherSelectedCandyColor = hit.collider.gameObject.GetComponent<Candy>().color;
                            //lastOtherSelectedCandyColor = CandyColor.Blue;
                        }

                        selectedTile = null;

                        possibleSwaps = DetectPossibleSwaps();
                    }
                }
            }
        }

        /// <summary>
        /// Performs a move.
        /// </summary>
        private void PerformMove()
        {
            ClearSuggestedMatch();
            if (level.limitType == LimitType.Moves && !PhotonNetwork.IsConnected)
            {
                currentLimit -= 1;
                if (currentLimit < 0)
                {
                    currentLimit = 0;
                }

                gameUi.SetLimit(currentLimit);
            }

            if (PhotonNetwork.IsConnected)
            {
                _hamleAdedi--;
                _pView.RPC("MovesObjectKontrol", RpcTarget.All, (int)_hamleAdedi);

                if (_hamleAdedi == 0) // HAMLE HAKKI BİTİNCE RAKİBE SIRA GEÇMESİ İÇİN
                {
                    _hamleAdedi = 2;
                    _dokunmaAktif = false;
                    Invoke("SiraDüzenlemeTetikleme", 1f);
                    //ROUND OBJELERİ DUZENLEME TETIKLEYICI
                }
                else
                {

                }
                //HAND OBJECT KONTROL BASLANGIC
                 
                float _xDeger1 = birinciTile.transform.localPosition.x;
                float _yDeger1 = birinciTile.transform.localPosition.y;
                float _xDeger2 = ikinciTile.transform.localPosition.x;
                float _yDeger2 = ikinciTile.transform.localPosition.y;
                _pView.RPC("HandObjeCalistir",RpcTarget.Others,(float)_xDeger1, (float)_yDeger1, (float)_xDeger2, (float)_yDeger2);
                _pView.RPC("BirinciTileAyarlama", RpcTarget.Others,(int)_birinciTileIdx,(int)_ikinciTileIdx);

            }
        }
        //HAND OBJECT KONTROLÜ FONKSİYONLAR

        [PunRPC]
        public void BirinciTileAyarlama(int _tileIndex,int _ikinciTileIndex)
        {
            _birinciTileIdx = _tileIndex;
            _ikinciTileIdx = _ikinciTileIndex;
        }

        [PunRPC]
        public void HandObjeCalistir(float _x1, float _y1, float _x2, float _y2)
        {
            _handObject.transform.localPosition = new Vector3(_x1, _y1+15f,0);
            _handObject.SetActive(true);
            HandObjectHareket(_x1, _y1, _x2, _y2);
        }

        private void HandObjectHareket(float _x1, float _y1, float _x2, float _y2)
        {
            _handObject.transform.DOLocalMove(new Vector3(_x1, _y1, 0), .25f).OnComplete(() => HandObjectSonHareket(_x2, _y2));
        }

        private void HandObjectSonHareket( float _x2, float _y2)
        {
            RakipTasHareket();
            _handObject.transform.DOLocalMove(new Vector3(_x2, _y2, 0), .25f);
            Invoke("HandObjectSonlandir",.75f);
        }

        private void HandObjectSonlandir()
        {
            _handObject.SetActive(false);
        }

        //HAND KONTROL SONU

        public void SiraDüzenlemeTetikleme()
        {
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>().MoveTimerSifirlama();
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerMoves[0].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerMoves[1].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerMoves[0].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerMoves[1].SetActive(true);

            SiraDegisikligiPaneliAcma("Opponent's Turn");

            _pView.RPC("RakipSiraTextDuzenleme", RpcTarget.Others, null);

            if (PhotonNetwork.MasterClient.NickName!=PhotonNetwork.NickName)   
            {
                GameObject.Find("ServerGameUIKontrol").GetComponent<PhotonView>().RPC("RoundObjeleriniDuzenle",RpcTarget.All,null);
            }
        }

        [PunRPC]
        public void RakipSiraTextDuzenleme()
        {
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>().MoveTimerSifirlama();
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerMoves[0].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerMoves[1].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerMoves[0].SetActive(true);
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerMoves[1].SetActive(true);

            SiraDegisikligiPaneliAcma("Your Turn");
        }

        [PunRPC]
        public void MovesObjectKontrol(int _deger)
        {
            if (_pView.IsMine)   
            {               
                GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._playerMoves[2-(_deger+1)].SetActive(false);

            }
            else
            {
                GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>()._rakipPlayerMoves[2 - (_deger + 1)].SetActive(false);

            }
        }

        #region

        private void SiraDegisikligiPaneliAcma(string _siraSahibi) // SIRA DEGİSİKLİGİ PANELİNİN ANİMASYONU İÇİN
        {
            _siraDegisikligiPaneli.SetActive(true);
            _siraDegisikligiPanelBG.SetActive(true);
            _siraDegisikligiPaneli.transform.GetChild(0).GetComponent<Text>().text = _siraSahibi;
            _siraDegisikligiPaneli.transform.localPosition = new Vector3(0,1000,0);
            _siraDegisikligiPaneli.transform.DOLocalMove(new Vector3(0, -250, 0), .75f).OnComplete(() =>SRDPanelScaleAnimasyon());

           // Invoke("SiraDegisikligiPanelAutoKill", 2.5f);
        }
        private void SRDPanelScaleAnimasyon()
        {
            _siraDegisikligiPaneli.transform.DOScale(new Vector3(1, .5f, 1), 0.25f).OnComplete(() => SRDPanelScaleAnimasyonReverse());
        }
        private void SRDPanelScaleAnimasyonReverse()
        {
            _siraDegisikligiPaneli.transform.DOScale(new Vector3(1, 1f, 1), 0.25f).OnComplete(() => SRDPanelScaleAnimasyonGidis());
        }
        private void SRDPanelScaleAnimasyonGidis()
        {
            _siraDegisikligiPaneli.transform.DOScale(new Vector3(1, 1.2f, 1), 0.25f);
            _siraDegisikligiPaneli.transform.DOLocalMove(new Vector3(0, -2250, 0), 1f).OnComplete(() => SiraDegisikligiPanelAutoKill());
        }

        private void SiraDegisikligiPanelAutoKill()
        {
            GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>().MoveTimerSifirlama();
            _siraDegisikligiPanelBG.SetActive(false);
            _siraDegisikligiPaneli.SetActive(false);

            _hamleSirasi = !_hamleSirasi;
            _dokunmaAktif = true;
        }
        #endregion

        /// <summary>
        /// Applies the gravity to the level tiles.
        /// </summary>
        public void ApplyGravity()
        {
            StartCoroutine(ApplyGravityAsync());
        }

        /// <summary>
        /// Explodes the specified generated tiles.
        /// </summary>
        /// <param name="genTiles">The list of generated tiles.</param>
        public void ExplodeGeneratedTiles(List<GameObject> genTiles)
        {
            StartCoroutine(ExplodeGeneratedTilesAsync(genTiles));
        }

        /// <summary>
        /// Explodes the specified generated tiles.
        /// </summary>
        /// <param name="genTiles">The list of generated tiles.</param>
        /// <returns>The coroutine.</returns>
        private IEnumerator ExplodeGeneratedTilesAsync(List<GameObject> genTiles)
        {
            yield return new WaitForSeconds(1.5f);

            foreach (var tile in genTiles)
            {
                ExplodeTile(tile);
            }

            StartCoroutine(ApplyGravityAsync(0.5f));
        }

        private IEnumerator YeniPatlamaGameObject(GameObject tile)
        {
            yield return new WaitForSeconds(0.3f);

            ExplodeTile(tile);

            if (tile.GetComponent<StripedCandy>() != null)
            {

            }
            else if (tile.GetComponent<WrappedCandy>() != null)
            {
                ApplyGravity();
                //gameScene.CheckEndGame();
            }
            else
            {

            }


            //ApplyGravity();

            //gameScene.CheckEndGame();
        }



        /// <summary>
        /// Creates a new tile from the specified level data.
        /// </summary>
        /// <param name="levelTile">The level tile.</param>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <returns>The new tile created from the specified level data.</returns>
        private GameObject CreateTileFromLevel(LevelTile levelTile, int x, int y)
        {
            if (levelTile is CandyTile)
            {
                var candyTile = (CandyTile)levelTile;
                if (candyTile.type == CandyType.RandomCandy)
                {
                    return CreateTile(x, y, false);
                }
                else
                {
                    var tile = tilePool.GetCandyPool((CandyColor)((int)candyTile.type)).GetObject();
                    tile.GetComponent<Tile>().board = this;
                    tile.GetComponent<Tile>().x = x;
                    tile.GetComponent<Tile>().y = y;
                    return tile;
                }
            }

            if (levelTile is SpecialCandyTile)
            {
                GameObject tile;

                var specialCandyTile = (SpecialCandyTile)levelTile;
                var specialCandyType = (int)specialCandyTile.type;
                if (specialCandyType >= 0 &&
                    specialCandyType <= (int)SpecialCandyType.YellowCandyHorizontalStriped)
                {
                    tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, (CandyColor)(specialCandyType % 6))
                        .GetObject();
                }
                else if (specialCandyType <= (int)SpecialCandyType.YellowCandyVerticalStriped)
                {
                    tile = tilePool.GetStripedCandyPool(StripeDirection.Vertical, (CandyColor)(specialCandyType % 6))
                        .GetObject();
                }
                else if (specialCandyType <= (int)SpecialCandyType.YellowCandyWrapped)
                {
                    tile = tilePool.GetWrappedCandyPool((CandyColor)(specialCandyType % 6)).GetObject();
                }
                else
                {
                    tile = tilePool.colorBombCandyPool.GetObject();
                }

                tile.GetComponent<Tile>().board = this;
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().y = y;
                return tile;
            }

            if (levelTile is SpecialBlockTile)
            {
                var specialBlockTile = (SpecialBlockTile)levelTile;
                var block = tilePool.GetSpecialBlockPool(specialBlockTile.type).GetObject();
                block.GetComponent<Tile>().board = this;
                block.GetComponent<Tile>().x = x;
                block.GetComponent<Tile>().y = y;
                return block;
            }

            if (levelTile is CollectableTile)
            {
                var collectableTile = (CollectableTile)levelTile;
                var tile = tilePool.GetCollectablePool(collectableTile.type).GetObject();
                tile.GetComponent<Tile>().board = this;
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().y = y;
                return tile;
            }

            return null;
        }
        // PVP RAKIP TAHTASI DIZME
        private GameObject PVPRakipCreatTile(LevelTile levelTile, int x, int y)
        {
            if (levelTile is CandyTile)
            {
                var candyTile = (CandyTile)levelTile;
                var tile = tilePool.GetCandyPool((CandyColor)(_rakipTileListem[_tileListeSiram])).GetObject();
                _tileListeSiram++;
                tile.GetComponent<Tile>().board = this;
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().y = y;
                return tile;

            }

            if (levelTile is SpecialCandyTile)
            {
                GameObject tile;

                var specialCandyTile = (SpecialCandyTile)levelTile;
                var specialCandyType = (int)specialCandyTile.type;
                if (specialCandyType >= 0 &&
                    specialCandyType <= (int)SpecialCandyType.YellowCandyHorizontalStriped)
                {
                    tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, (CandyColor)(specialCandyType % 6))
                        .GetObject();
                }
                else if (specialCandyType <= (int)SpecialCandyType.YellowCandyVerticalStriped)
                {
                    tile = tilePool.GetStripedCandyPool(StripeDirection.Vertical, (CandyColor)(specialCandyType % 6))
                        .GetObject();
                }
                else if (specialCandyType <= (int)SpecialCandyType.YellowCandyWrapped)
                {
                    tile = tilePool.GetWrappedCandyPool((CandyColor)(specialCandyType % 6)).GetObject();
                }
                else
                {
                    tile = tilePool.colorBombCandyPool.GetObject();
                }

                tile.GetComponent<Tile>().board = this;
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().y = y;
                return tile;
            }

            if (levelTile is SpecialBlockTile)
            {
                var specialBlockTile = (SpecialBlockTile)levelTile;
                var block = tilePool.GetSpecialBlockPool(specialBlockTile.type).GetObject();
                block.GetComponent<Tile>().board = this;
                block.GetComponent<Tile>().x = x;
                block.GetComponent<Tile>().y = y;
                return block;
            }

            if (levelTile is CollectableTile)
            {
                var collectableTile = (CollectableTile)levelTile;
                var tile = tilePool.GetCollectablePool(collectableTile.type).GetObject();
                tile.GetComponent<Tile>().board = this;
                tile.GetComponent<Tile>().x = x;
                tile.GetComponent<Tile>().y = y;
                return tile;
            }

            return null;

        }
        /// <summary>
        /// Creates a new, random tile.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <param name="runtime">True if this tile is created during a game; false otherwise.</param>
        /// <returns>The newly created tile.</returns>
        private GameObject CreateTile(int x, int y, bool runtime)
        {
            var eligibleTiles = new List<CandyColor>();
            eligibleTiles.AddRange(level.availableColors);

            var leftTile1 = GetTile(x - 1, y);
            var leftTile2 = GetTile(x - 2, y);
            if (leftTile1 != null && leftTile2 != null &&
                leftTile1.GetComponent<Candy>() != null && leftTile2.GetComponent<Candy>() != null &&
                leftTile1.GetComponent<Candy>().color == leftTile2.GetComponent<Candy>().color)
            {
                var tileToRemove = eligibleTiles.Find(t =>
                    t == leftTile1.GetComponent<Candy>().color);
                eligibleTiles.Remove(tileToRemove);
            }

            var topTile1 = GetTile(x, y - 1);
            var topTile2 = GetTile(x, y - 2);
            if (topTile1 != null && topTile2 != null &&
                topTile1.GetComponent<Candy>() != null && topTile2.GetComponent<Candy>() != null &&
                topTile1.GetComponent<Candy>().color == topTile2.GetComponent<Candy>().color)
            {
                var tileToRemove = eligibleTiles.Find(t =>
                    t == topTile1.GetComponent<Candy>().color);
                eligibleTiles.Remove(tileToRemove);
            }

            GameObject tile;
            if (runtime && eligibleCollectables.Count > 0)//COLLECTABLE OBJE GELME IHTIMALINE GORE OBJENIN GELMESİ
            {
                var tileChance = UnityEngine.Random.Range(0, 100);
                if (tileChance <= level.collectableChance)
                {
                    var idx = UnityEngine.Random.Range(0, eligibleCollectables.Count);
                    var collectable = eligibleCollectables[idx];
                    tile = tilePool.GetCollectablePool(collectable).GetObject();
                    eligibleCollectables.RemoveAt(idx);
                }
                else
                {
                    tile = tilePool.GetCandyPool(eligibleTiles[UnityEngine.Random.Range(0, eligibleTiles.Count)])
                        .GetObject();
                }
            }
            else
            {
                tile = tilePool.GetCandyPool(eligibleTiles[UnityEngine.Random.Range(0, eligibleTiles.Count)])
                    .GetObject();
            }

            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            return tile;
        }

        /// <summary>
        /// Creates a new horizontally striped tile.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <param name="color">The color of the tile.</param>
        /// <returns>The newly created tile.</returns>
        public GameObject CreateHorizontalStripedTile(int x, int y, CandyColor color)
        {
            var tileIdx = x + (y * level.width);
            //var tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, color).GetObject();
            var tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, CandyColor.Blue).GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            tile.GetComponent<StripedCandy>()._patladim = false;
            return tile;
        }

        public GameObject CreateHorizontalStripedTileBooster(int x, int y, CandyColor color)
        {
            var tileIdx = x + (y * level.width);
            //var tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, color).GetObject();
            var tile = tilePool.GetStripedCandyPool(StripeDirection.Horizontal, CandyColor.Blue).GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            tile.GetComponent<StripedCandy>()._patladim = false;
            //PatlatHorizontalBooster(tile);
            return tile;
        }

        /// <summary>
        /// Creates a new vertically striped tile.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <param name="color">The color of the tile.</param>
        /// <returns>The newly created tile.</returns>
        public GameObject CreateVerticalStripedTile(int x, int y, CandyColor color)
        {
            var tileIdx = x + (y * level.width);
            //var tile = tilePool.GetStripedCandyPool(StripeDirection.Vertical, color).GetObject();
            var tile = tilePool.GetStripedCandyPool(StripeDirection.Vertical, CandyColor.Blue).GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            tile.GetComponent<StripedCandy>()._patladim = false;
            return tile;
        }

        /// <summary>
        /// Creates a new wrapped tile.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <param name="color">The color of the tile.</param>
        /// <returns>The newly created tile.</returns>
        public GameObject CreateWrappedTile(int x, int y, CandyColor color)
        {
            var tileIdx = x + (y * level.width);
            //var tile = tilePool.GetWrappedCandyPool(color).GetObject();
            var tile = tilePool.GetWrappedCandyPool(CandyColor.Blue).GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            return tile;
        }

        /// <summary>
        /// Creates a new color bomb.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <returns>The newly created tile.</returns>
        public GameObject CreateColorBomb(int x, int y)
        {
            var tileIdx = x + (y * level.width);
            var tile = tilePool.colorBombCandyPool.GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            return tile;
        }

        /// <summary>
        /// Creates a new chocolate.
        /// </summary>
        /// <param name="x">The x-coordinate of the tile.</param>
        /// <param name="y">The y-coordinate of the tile.</param>
        /// <returns>The newly created tile.</returns>
        public GameObject CreateChocolate(int x, int y)
        {
            var tileIdx = x + (y * level.width);
            var tile = tilePool.chocolatePool.GetObject();
            tile.GetComponent<Tile>().board = this;
            tile.GetComponent<Tile>().x = x;
            tile.GetComponent<Tile>().y = y;
            tile.transform.position = tilePositions[tileIdx];
            tiles[tileIdx] = tile;
            CreateSpawnParticles(tile.transform.position);
            return tile;
        }

        /// <summary>
        /// Creates the spawn particles at the specified position.
        /// </summary>
        /// <param name="position">The position of the particles.</param>
        private void CreateSpawnParticles(Vector2 position)
        {
            var particles = fxPool.spawnParticles.GetObject();
            particles.transform.position = position;
            foreach (var child in particles.GetComponentsInChildren<ParticleSystem>())
            {
                child.Play();
            }
        }

        /// <summary>
        /// Returns the tile at coordinates (x, y).
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>The tile at coordinates (x, y).</returns>
        public GameObject GetTile(int x, int y)
        {
            if (x >= 0 && x < level.width && y >= 0 && y < level.height)
            {
                return tiles[x + (y * level.width)];
            }

            return null;
        }

        /// <summary>
        /// Replaces the tile at coordinates (x, y).
        /// </summary>
        /// <param name="tile">The new tile.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        private void SetTile(GameObject tile, int x, int y)
        {
            if (x >= 0 && x < level.width && y >= 0 && y < level.height)
            {
                tiles[x + (y * level.width)] = tile;
            }
        }

        private void RakipSetTile(GameObject tile, int x, int y)
        {
            if (x >= 0 && x < level.width && y >= 0 && y < level.height)
            {
                tiles[x + (y * level.width)] = tile;
            }
        }


        /// <summary>
        /// Explodes the specified tile.
        /// </summary>
        /// <param name="tile">The tile to explode.</param>
        /// <param name="didAnySpecialCandyExplode">True if any special candy exploded; false otherwise.</param>
        public void ExplodeTile(GameObject tile, bool didAnySpecialCandyExplode = false)
        {


            if (tile.GetComponent<StripedCandy>() != null)
            {
                tile.GetComponent<StripedCandy>().Resolve(this, tile);
                var explodedTiles = new List<GameObject>();
                ExplodeTileRecursive(tile, explodedTiles);
                var score = 0;
                foreach (var explodedTile in explodedTiles)
                {
                    var idx = tiles.FindIndex(x => x == explodedTile);
                    if (idx != -1)
                    {
                        explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                        explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                        score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());

                        if (!PhotonNetwork.IsConnected)
                        {
                            DestroyElements(explodedTile);
                        }
                        
                        explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                        tiles[idx] = null;
                    }

                    SoundManager.instance.PlaySound("CandyMatch");
                }
                UpdateScore(score);
                gameUi.UpdateGoals(gameState);
            }
            else if (tile.GetComponent<WrappedCandy>() != null) 
            {
                var explodedTiles = new List<GameObject>();
                ExplodeTileRecursive(tile, explodedTiles);
                var score = 0;

                foreach (var explodedTile in explodedTiles)
                {
                    var idx = tiles.FindIndex(x => x == explodedTile);
                    if (idx != -1)
                    {
                        explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                        explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                        score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                        if (!PhotonNetwork.IsConnected)
                        {
                            DestroyElements(explodedTile);
                        }
                        DestroySpecialBlocks(explodedTile, didAnySpecialCandyExplode);
                        explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                        tiles[idx] = null;
                    }

                    SoundManager.instance.PlaySound("CandyMatch");
                }

                UpdateScore(score);
                gameUi.UpdateGoals(gameState);

            }
            else
            {
                var explodedTiles = new List<GameObject>();
                ExplodeTileRecursive(tile, explodedTiles);
                var score = 0;

                foreach (var explodedTile in explodedTiles)
                {
                    var idx = tiles.FindIndex(x => x == explodedTile);
                    if (idx != -1)
                    {
                        explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                        explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                        score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                        if (!PhotonNetwork.IsConnected)
                        {
                            DestroyElements(explodedTile);
                        }
                        DestroySpecialBlocks(explodedTile, didAnySpecialCandyExplode);
                        explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                        tiles[idx] = null;
                    }

                    SoundManager.instance.PlaySound("CandyMatch");
                }
               
                UpdateScore(score);
                gameUi.UpdateGoals(gameState);
            }





        }

        public void CekicIlePatlat(GameObject tile, bool didAnySpecialCandyExplode = false)
        {
            var explodedTiles = new List<GameObject>();
            ExplodeTileRecursive(tile, explodedTiles);
            var score = 0;
            foreach (var explodedTile in explodedTiles)
            {
                var idx = tiles.FindIndex(x => x == explodedTile);
                if (idx != -1)
                {
                    explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                    score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(explodedTile);
                    }
                    //DestroySpecialBlocks(explodedTile, didAnySpecialCandyExplode);
                    explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                    tiles[idx] = null;
                }

                SoundManager.instance.PlaySound("CandyMatch");
            }

            UpdateScore(score);
            gameUi.UpdateGoals(gameState);
        }

        /// <summary>
        /// Explodes the specified tile.
        /// </summary>
        /// <param name="tile">The tile to explode.</param>
        public void ExplodeTileViaBooster(GameObject tile)
        {
            var explodedTiles = new List<GameObject>();
            ExplodeTileRecursive(tile, explodedTiles);
            var score = 0;
            foreach (var explodedTile in explodedTiles)
            {
                var idx = tiles.FindIndex(x => x == explodedTile);
                if (idx != -1)
                {
                    explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(explodedTile);
                    }
                    DestroySpecialBlocks(explodedTile, false);
                    explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                    tiles[idx] = null;
                }

                SoundManager.instance.PlaySound("CandyMatch");
            }

            UpdateScore(score);
            gameUi.UpdateGoals(gameState);
        }

        public void BoosterIlePatlat(GameObject tile)
        {
            var explodedTiles = new List<GameObject>();
            ExplodeTileRecursive(tile, explodedTiles);
            var score = 0;
            foreach (var explodedTile in explodedTiles)
            {
                var idx = tiles.FindIndex(x => x == explodedTile);
                if (idx != -1)
                {
                    explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(explodedTile);
                    }
                    //DestroySpecialBlocks(explodedTile, false);
                    explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                    tiles[idx] = null;
                }

                SoundManager.instance.PlaySound("CandyMatch");
            }

            UpdateScore(score);
            gameUi.UpdateGoals(gameState);
        }

        public void RoketlePatlat(GameObject tile)
        {
            tile.GetComponent<StripedCandy>().Resolve(this, tile);
            var explodedTiles = new List<GameObject>();
            ExplodeTileRecursive(tile, explodedTiles);
            var score = 0;
            foreach (var explodedTile in explodedTiles)
            {
                var idx = tiles.FindIndex(x => x == explodedTile);
                if (idx != -1)
                {
                    explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                    score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(explodedTile);
                    }
                    //DestroySpecialBlocks(explodedTile, didAnySpecialCandyExplode);
                    explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                    tiles[idx] = null;
                }

                SoundManager.instance.PlaySound("CandyMatch");
            }

            UpdateScore(score);
            gameUi.UpdateGoals(gameState);
        }

        public void ComboRoketlePatlat(GameObject tile)
        {
            //tile.GetComponent<StripedCandy>().Resolve(this, tile);
            var explodedTiles = new List<GameObject>();
            ExplodeTileRecursive(tile, explodedTiles);
            var score = 0;
            foreach (var explodedTile in explodedTiles)
            {
                var idx = tiles.FindIndex(x => x == explodedTile);
                if (idx != -1)
                {
                    explodedTile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    explodedTile.GetComponent<Tile>().UpdateGameState(gameState);
                    score += gameConfig.GetTileScore(explodedTile.GetComponent<Tile>());
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(explodedTile);
                    }
                    //DestroySpecialBlocks(explodedTile, didAnySpecialCandyExplode);
                    explodedTile.GetComponent<PooledObject>().pool.ReturnObject(explodedTile);
                    tiles[idx] = null;
                }

                SoundManager.instance.PlaySound("CandyMatch");
            }

            UpdateScore(score);
            gameUi.UpdateGoals(gameState);
        }

        /// <summary>
        /// Explodes the specified tile non-recursively.
        /// </summary>
        /// <param name="tile">The tile to explode.</param>
        public void ExplodeTileNonRecursive(GameObject tile)
        {
            if (tile != null)
            {
                if (tile.GetComponent<Collectable>() != null)
                {
                    return;
                }

                if (tile.GetComponent<Tile>() != null && !tile.GetComponent<Tile>().destructable)
                {
                    return;
                }

                var idx = tiles.FindIndex(x => x == tile);
                if (idx != -1)
                {
                    tile.GetComponent<Tile>().ShowExplosionFx(fxPool);
                    tile.GetComponent<Tile>().UpdateGameState(gameState);
                    UpdateScore(gameConfig.GetTileScore(tile.GetComponent<Tile>()));
                    if (!PhotonNetwork.IsConnected)
                    {
                        DestroyElements(tile);
                    }
                    tile.GetComponent<PooledObject>().pool.ReturnObject(tile);
                    tiles[idx] = null;

                    var chocolates = tiles.FindAll(t => t != null && t.GetComponent<Chocolate>() != null);
                    if (chocolates.Count == 0)
                    {
                        gameState.destroyedAllChocolates = true;
                    }

                    gameUi.UpdateGoals(gameState);

                    SoundManager.instance.PlaySound("CandyMatch");
                }
            }
        }

        /// <summary>
        /// Explodes the specified tile recursively.
        /// </summary>
        /// <param name="tile">The tile to explode.</param>
        /// <param name="explodedTiles">The list of the exploded tiles so far.</param>
        private void ExplodeTileRecursive(GameObject tile, List<GameObject> explodedTiles)
        {
            if (tile != null && tile.GetComponent<Tile>() != null)
            {

                var newTilesToExplode = tile.GetComponent<Tile>().Explode();

                explodedTiles.Add(tile);

                foreach (var t in newTilesToExplode)
                {
                    if (t != null && t.GetComponent<Tile>() != null && t.GetComponent<Tile>().destructable &&
                        !explodedTiles.Contains(t))
                    {
                        if (t.GetComponent<ColorBomb>() != null)
                        {
                            ColorBombPatlat(t);
                            explodedTiles.Add(t);
                            ExplodeTileRecursive(t, explodedTiles);
                        }
                        else
                        {
                            explodedTiles.Add(t);
                            ExplodeTileRecursive(t, explodedTiles);
                        }

                    }

                    //yield return new WaitForSeconds(0.1f);
                }

                foreach (var t in newTilesToExplode)
                {
                    if (!newTilesToExplode.Contains(t))
                    {
                        newTilesToExplode.Add(t);
                    }
                }


            }
        }

        private void PatlatCombo(GameObject tile)
        {
            tile.GetComponent<Tile>().Explode();
        }

        /// <summary>
        /// Destroys the elements at the cell occupied by the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        private void DestroyElements(GameObject tile)
        {

            var idx = tile.GetComponent<Tile>().x + (tile.GetComponent<Tile>().y * level.width);
            // Check for honey.
            if (idx != -1 && level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Honey)
            {
                honeys[idx].GetComponent<PooledObject>().pool.ReturnObject(honeys[idx]);
                level.tiles[idx].elementType = ElementType.None;
                honeys[idx] = null;
                gameState.AddElement(ElementType.Honey);
                UpdateScore(gameConfig.GetElementScore(ElementType.Honey));

                var fx = fxPool.GetElementExplosion(ElementType.Honey).GetObject();
                fx.transform.position = tilePositions[idx];

                SoundManager.instance.PlaySound("Honey");
            }

            // Check for syrup x1.
            if (idx != -1 && level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Syrup1)
            {
                syrups1[idx].GetComponent<PooledObject>().pool.ReturnObject(syrups1[idx]);
                level.tiles[idx].elementType = ElementType.None;
                syrups1[idx] = null;
                gameState.AddElement(ElementType.Syrup1);
                UpdateScore(gameConfig.GetElementScore(ElementType.Syrup1));

                var fx = fxPool.GetElementExplosion(ElementType.Syrup1).GetObject();
                fx.transform.position = tilePositions[idx];

                SoundManager.instance.PlaySound("Syrup");
            }

            // Check for syrup x2.
            if (idx != -1 && level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Syrup2)
            {
                var syrup = tilePool.syrup1Pool.GetObject();
                syrup.transform.position = syrups2[idx].transform.position;
                syrup.GetComponent<SpriteRenderer>().sortingOrder = -1;

                syrups2[idx].GetComponent<PooledObject>().pool.ReturnObject(syrups2[idx]);
                level.tiles[idx].elementType = ElementType.Syrup1;
                syrups2[idx] = null;
                syrups1[idx] = syrup;

                gameState.AddElement(ElementType.Syrup2);
                UpdateScore(gameConfig.GetElementScore(ElementType.Syrup2));

                var fx = fxPool.GetElementExplosion(ElementType.Syrup2).GetObject();
                fx.transform.position = tilePositions[idx];

                SoundManager.instance.PlaySound("Syrup");
            }

            // Check for ices.
            if (idx != -1 && level.tiles[idx] != null && level.tiles[idx].elementType == ElementType.Ice)
            {
                ices[idx].GetComponent<PooledObject>().pool.ReturnObject(ices[idx]);
                level.tiles[idx].elementType = ElementType.None;
                ices[idx] = null;
                gameState.AddElement(ElementType.Ice);
                UpdateScore(gameConfig.GetElementScore(ElementType.Ice));

                var fx = fxPool.GetElementExplosion(ElementType.Ice).GetObject();
                fx.transform.position = tilePositions[idx];

                SoundManager.instance.PlaySound("Ice");
            }


        }

        /// <summary>
        /// Destroys the special blocks at the cell occupied by the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="didAnySpecialCandyExplode">True if any special candy exploded; false otherwise.</param>
        private void DestroySpecialBlocks(GameObject tile, bool didAnySpecialCandyExplode)
        {
            if (!didAnySpecialCandyExplode)
            {
                var x = tile.GetComponent<Tile>().x;
                var y = tile.GetComponent<Tile>().y;
                var leftTile = GetTile(x - 1, y);
                var rightTile = GetTile(x + 1, y);
                var topTile = GetTile(x, y + 1);
                var bottomTile = GetTile(x, y - 1);
                var neighbourTiles = new List<GameObject> { leftTile, rightTile, topTile, bottomTile };
                foreach (var neighbour in neighbourTiles)
                {
                    DestroySpecialBlocksInternal(neighbour);
                }

                DestroySpecialBlocksInternal(tile);

                var chocolates = tiles.FindAll(t => t != null && t.GetComponent<Chocolate>() != null);
                if (chocolates.Count == 0)
                {
                    gameState.destroyedAllChocolates = true;
                }
            }
        }

        /// <summary>
        /// Destroys the special blocks at the cell occupied by the specified tile.
        /// </summary>
        /// <param name="tile">The tile.</param>
        private void DestroySpecialBlocksInternal(GameObject tile)
        {
            if (tile != null && tile.GetComponent<SpecialBlock>() != null &&
                tile.GetComponent<SpecialBlock>().destructable)
            {
                var blockIdx = tiles.FindIndex(t => t == tile);
                if (blockIdx != -1)
                {
                    gameState.AddSpecialBlock(tile.GetComponent<SpecialBlock>().type);
                    UpdateScore(gameConfig.GetTileScore(tile.GetComponent<SpecialBlock>()));

                    var fx = fxPool.GetSpecialBlockExplosion(tile.GetComponent<SpecialBlock>().type).GetObject();
                    fx.transform.position = tile.transform.position;

                    tile.GetComponent<PooledObject>().pool.ReturnObject(tile);
                    tiles[blockIdx] = null;
                }

                if (tile.GetComponent<Chocolate>() != null)
                {
                    explodedChocolate = true;
                    SoundManager.instance.PlaySound("Chocolate");
                }
                else if (tile.GetComponent<Marshmallow>() != null)
                {
                    SoundManager.instance.PlaySound("Marshmallow");
                }
            }
        }

        /// <summary>
        /// Returns true if the tile at (x, y) has a match and false otherwise.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the tile at (x, y) has a match; false otherwise.</returns>
        private bool HasMatch(int x, int y)
        {
            return HasHorizontalMatch(x, y) || HasVerticalMatch(x, y);
        }

        /// <summary>
        /// Returns true if the tile at (x, y) has a horizontal match and false otherwise.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the tile at (x, y) has a horizontal match; false otherwise.</returns>
        private bool HasHorizontalMatch(int x, int y)
        {
            var tile = GetTile(x, y);
            if (tile.GetComponent<Candy>() != null)
            {
                var horzLen = 1;
                for (var i = x - 1;
                    i >= 0 && GetTile(i, y) != null && GetTile(i, y).GetComponent<Candy>() != null &&
                    GetTile(i, y).GetComponent<Candy>().color == tile.GetComponent<Candy>().color;
                    i--, horzLen++) ;
                for (var i = x + 1;
                    i < level.width && GetTile(i, y) != null && GetTile(i, y).GetComponent<Candy>() != null &&
                    GetTile(i, y).GetComponent<Candy>().color == tile.GetComponent<Candy>().color;
                    i++, horzLen++) ;
                if (horzLen >= 3) return true;
            }

            return false;
        }

        /// <summary>
        /// Returns true if the tile at (x, y) has a vertical match and false otherwise.
        /// </summary>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>True if the tile at (x, y) has a vertical match; false otherwise.</returns>
        private bool HasVerticalMatch(int x, int y)
        {
            var tile = GetTile(x, y);
            if (tile.GetComponent<Candy>() != null)
            {
                var vertLen = 1;
                for (var j = y - 1;
                    j >= 0 && GetTile(x, j) != null && GetTile(x, j).GetComponent<Candy>() != null &&
                    GetTile(x, j).GetComponent<Candy>().color == tile.GetComponent<Candy>().color;
                    j--, vertLen++) ;
                for (var j = y + 1;
                    j < level.height && GetTile(x, j) != null && GetTile(x, j).GetComponent<Candy>() != null &&
                    GetTile(x, j).GetComponent<Candy>().color == tile.GetComponent<Candy>().color;
                    j++, vertLen++) ;
                if (vertLen >= 3) return true;
            }

            return false;
        }

        /// <summary>
        /// Detects all the possible tile swaps in the current level.
        /// </summary>
        /// <returns>A list containing all the possible tile swaps in the level.</returns>
        private List<Swap> DetectPossibleSwaps()
        {
            var swaps = new List<Swap>();

            for (var j = 0; j < level.height; j++)
            {
                for (var i = 0; i < level.width; i++)
                {
                    var tile = GetTile(i, j);
                    if (tile != null)
                    {
                        if (i < level.width - 1)
                        {
                            var other = GetTile(i + 1, j);
                            if (other != null)
                            {
                                SetTile(other, i, j);
                                SetTile(tile, i + 1, j);

                                if (HasMatch(i, j) || HasMatch(i + 1, j))
                                {
                                    var swap = new Swap { tileA = tile, tileB = other };
                                    swaps.Add(swap);
                                }
                            }

                            SetTile(tile, i, j);
                            SetTile(other, i + 1, j);
                        }

                        if (j < level.height - 1)
                        {
                            var other = GetTile(i, j + 1);
                            if (other != null)
                            {
                                SetTile(other, i, j);
                                SetTile(tile, i, j + 1);

                                if (HasMatch(i, j) || HasMatch(i, j + 1))
                                {
                                    var swap = new Swap { tileA = tile, tileB = other };
                                    swaps.Add(swap);
                                }
                            }

                            SetTile(tile, i, j);
                            SetTile(other, i, j + 1);
                        }
                    }
                }
            }

            return swaps;
        }

        /// <summary>
        /// Resolves all the matches in the current level.
        /// </summary>
        /// <param name="isPlayerMatch">True if the match was caused by a player and false otherwise.</param>
        /// <returns>True if there were any matches; false otherwise.</returns>
        private bool HandleMatches(bool isPlayerMatch) //BENIM PARMALA MATCHLEDİKLERİM(TRUE) ve PARMAKSIZ MATCHLEDİKLERİM(FALSE)
        {
            var matches = new List<Match>();
            var tShapedMatches = tShapedMatchDetector.DetectMatches(this);
            var lShapedMatches = lShapedMatchDetector.DetectMatches(this);
            var horizontalMatches = horizontalMatchDetector.DetectMatches(this);
            var verticalMatches = verticalMatchDetector.DetectMatches(this);

            if (tShapedMatches.Count > 0)
            {
                matches.AddRange(tShapedMatches);
                foreach (var match in horizontalMatches)
                {
                    var found = false;
                    foreach (var match2 in tShapedMatches)
                    {
                        if (match.tiles.Find(x => match2.tiles.Contains(x)) != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        matches.Add(match);
                    }
                }

                foreach (var match in verticalMatches)
                {
                    var found = false;
                    foreach (var match2 in tShapedMatches)
                    {
                        if (match.tiles.Find(x => match2.tiles.Contains(x)) != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        matches.Add(match);
                    }
                }
            }
            else if (lShapedMatches.Count > 0)
            {
                matches.AddRange(lShapedMatches);
                foreach (var match in horizontalMatches)
                {
                    var found = false;
                    foreach (var match2 in lShapedMatches)
                    {
                        if (match.tiles.Find(x => match2.tiles.Contains(x)) != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        matches.Add(match);
                    }
                }

                foreach (var match in verticalMatches)
                {
                    var found = false;
                    foreach (var match2 in lShapedMatches)
                    {
                        if (match.tiles.Find(x => match2.tiles.Contains(x)) != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        matches.Add(match);
                    }
                }
            }
            else if (horizontalMatches.Count > 0)
            {
                matches.AddRange(horizontalMatches);
                foreach (var match in verticalMatches)
                {
                    var found = false;
                    foreach (var match2 in horizontalMatches)
                    {
                        if (match.tiles.Find(x => match2.tiles.Contains(x)) != null)
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        matches.Add(match);
                    }
                }
            }
            else
            {
                matches.AddRange(horizontalMatches);
                matches.AddRange(verticalMatches);
            }

            ///////// YUKARISI HERHANGİ BİR ŞEKİLDE EŞLEME VAR MI KONTOROLÜ YAPIYOR ve matches'a ekliyor.
            ///
            //////// AŞAĞISI BULUNAN EŞLEMELERİ PATLATIYOR - BURASI KULLANICININ KONTORLÜNDE OLMADAN PATLAYAN YER
            if (matches.Count > 0)
            {
                var didAnySpecialCandyExplode = false;
                var numSpecialCandiesGenerated = 0;

                foreach (var match in matches)
                {
                    var randomTile = match.tiles[UnityEngine.Random.Range(0, match.tiles.Count)];
                    var randomIdx = tiles.FindIndex(x => x == randomTile);
                    //var randomColor = randomTile.GetComponent<Candy>().color;
                    var randomColor = CandyColor.Blue;

                    if (match.tiles.Find(x =>
                        x.GetComponent<StripedCandy>() != null || x.GetComponent<WrappedCandy>() != null))
                    {
                        didAnySpecialCandyExplode = true;
                    }

                    foreach (var tile in match.tiles)
                    {
                        ExplodeTile(tile, didAnySpecialCandyExplode); /// PATLATILAN YER
                    }

                    if (!didAnySpecialCandyExplode && numSpecialCandiesGenerated == 0)
                    {
                        if (match.tiles.Count >= 5 && match.type != MatchType.TShaped &&
                            match.type != MatchType.LShaped)
                        {
                            if (!PhotonNetwork.IsConnected)
                            {
                                if (isPlayerMatch)
                                {
                                    if (match.tiles.Contains(lastSelectedTile))
                                    {
                                        CreateColorBomb(lastSelectedTileX, lastSelectedTileY);
                                    }
                                    else if (match.tiles.Contains(lastOtherSelectedTile))
                                    {
                                        CreateColorBomb(lastOtherSelectedTileX, lastOtherSelectedTileY);
                                    }
                                }
                                else if (randomIdx != -1)
                                {
                                    var i = randomIdx % level.width;
                                    var j = randomIdx / level.width;
                                    CreateColorBomb(i, j);
                                }

                                ++numSpecialCandiesGenerated;

                            }
                            else
                            {

                            }
                        }
                        else if (match.tiles.Count >= 5)
                        {
                            if (isPlayerMatch)
                            {
                                if (match.tiles.Contains(lastSelectedTile))
                                {
                                    CreateWrappedTile(lastSelectedTileX, lastSelectedTileY,
                                        CandyColor.Blue);
                                }
                                else if (match.tiles.Contains(lastOtherSelectedTile))
                                {
                                    CreateWrappedTile(lastOtherSelectedTileX, lastOtherSelectedTileY,
                                        CandyColor.Blue);
                                }
                            }
                            else if (randomIdx != -1)
                            {
                                var i = randomIdx % level.width;
                                var j = randomIdx / level.width;
                                CreateWrappedTile(i, j, randomColor);
                            }

                            ++numSpecialCandiesGenerated;
                        }
                        else if (match.tiles.Count >= 4)
                        {
                            if (swapDirection == SwapDirection.Horizontal)
                            {
                                if (isPlayerMatch)
                                {
                                    if (match.tiles.Contains(lastSelectedTile))
                                    {
                                        CreateHorizontalStripedTile(lastSelectedTileX, lastSelectedTileY,
                                            CandyColor.Blue);
                                    }
                                    else if (match.tiles.Contains(lastOtherSelectedTile))
                                    {
                                        CreateHorizontalStripedTile(lastOtherSelectedTileX, lastOtherSelectedTileY,
                                            CandyColor.Blue);
                                    }
                                }
                                else if (randomIdx != -1)
                                {
                                    var i = randomIdx % level.width;
                                    var j = randomIdx / level.width;
                                    CreateHorizontalStripedTile(i, j, randomColor);
                                }
                            }
                            else
                            {
                                if (isPlayerMatch)
                                {
                                    if (match.tiles.Contains(lastSelectedTile))
                                    {
                                        CreateVerticalStripedTile(lastSelectedTileX, lastSelectedTileY,
                                            CandyColor.Blue);
                                    }
                                    else if (match.tiles.Contains(lastOtherSelectedTile))
                                    {
                                        CreateVerticalStripedTile(lastOtherSelectedTileX, lastOtherSelectedTileY,
                                            CandyColor.Blue);
                                    }
                                }
                                else if (randomIdx != -1)
                                {
                                    var i = randomIdx % level.width;
                                    var j = randomIdx / level.width;
                                    CreateVerticalStripedTile(i, j, randomColor);
                                }
                            }

                            ++numSpecialCandiesGenerated;
                        }
                    }
                }

                if (isPlayerMatch)
                {
                    consecutiveCascades = 0;
                }
                else
                {
                    consecutiveCascades += 1;
                    if (consecutiveCascades == 2)
                    {
                        gameScene.ShowComplimentText(ComplimentType.Good);
                    }
                    else if (consecutiveCascades == 4)
                    {
                        gameScene.ShowComplimentText(ComplimentType.Super);
                    }
                    else if (consecutiveCascades == 6)
                    {
                        gameScene.ShowComplimentText(ComplimentType.Yummy);
                    }
                }

                if (isPlayerMatch)
                {
                    if (birinciTile.GetComponent<StripedCandy>() != null || ikinciTile.GetComponent<StripedCandy>() != null)
                    {

                    }
                    else if (birinciTile.GetComponent<WrappedCandy>() != null || ikinciTile.GetComponent<WrappedCandy>() != null)
                    {
                        //ApplyGravity();
                        //gameScene.CheckEndGame();
                    }
                    else
                    {
                        Debug.Log("En ALTTIN BIR USTU ASYNC CASLISTI");
                        StartCoroutine(ApplyGravityAsync(didAnySpecialCandyExplode ? 0.5f : 0.0f));
                    }
                }
                else
                {
                    Debug.Log("En ALTTAKİ ASYNC CASLISTI");
                    StartCoroutine(ApplyGravityAsync(didAnySpecialCandyExplode ? 0.5f : 0.0f));
                }

                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// The coroutine that applies the gravity to the current level.
        /// </summary>
        /// <param name="delay">The delay.</param>
        /// <returns>The coroutine.</returns>
        private IEnumerator ApplyGravityAsync(float delay = 0.0f)
        {
            deneme++;
            ClearSuggestedMatch();
            if (suggestedMatchCoroutine != null)
            {
                StopCoroutine(suggestedMatchCoroutine);
                suggestedMatchCoroutine = null;
            }

            inputLocked = true;
            yield return new WaitForSeconds(delay);
            if (PhotonNetwork.IsConnected)   
            {
                if (_hamleSirasi)   
                {
                    Debug.Log("ODA SAHIBI GRAVITYSI");
                    ApplyGravityInternal();
                }
                else
                {
                    Debug.Log("RAKIP GRAVITYSI");
                    RakipApplyGravityInternal();
                }
            }
            else
            {
                Debug.Log("ANA GRAVITYSI");

                ApplyGravityInternal();
            }
            possibleSwaps = DetectPossibleSwaps();
            yield return new WaitForSeconds(0.4f);
            if (currentlyAwarding)
            {
                gameScene.CheckEndGame();
            }
            else
            {
                if (!HandleMatches(false)) // GRAVITY SONRASI ESLESME KONTROL YERİ-TRUE GELİRSE OTOMATIK ESLESME PATLATMIS DEMEK
                { //HandleMatches(false) = false gelmesi kendiliğinden eşlenecek birşey yok demek/ true dönerse kendiliğinden eşleşen olmuş demektir.
                    if (suggestedMatchCoroutine != null)
                    {
                        StopCoroutine(suggestedMatchCoroutine);
                        suggestedMatchCoroutine = null;
                    }
                    ExpandChocolate();
                    inputLocked = false;
                    explodedChocolate = false;
                    if (PhotonNetwork.IsConnected)
                    {

                    }
                    else
                    {
                        suggestedMatchCoroutine = StartCoroutine(HighlightRandomMatchAsync());
                    }
                }
            }

            if (CheckCollectables())
            {
                ApplyGravity();
            }
            gameScene.CheckEndGame();
        }

        /// <summary>
        /// Checks the current level for collectables that have been collected by the player.
        /// </summary>
        /// <returns>True if there were collected collectables; false otherwise.</returns>
        private bool CheckCollectables()
        {
            var collectablesToDestroy = new List<Tile>();
            for (var i = 0; i < level.width; i++)
            {
                Tile bottom = null;
                var tileIndex = 0;
                for (var j = level.height - 1; j >= 0; j--)
                {
                    tileIndex = i + (j * level.width);
                    if (tiles[tileIndex] == null)
                    {
                        continue;
                    }

                    var tile = tiles[tileIndex].GetComponent<Tile>();
                    if (tile != null)
                    {
                        if (tile.GetComponent<Unbreakable>() != null)
                        {
                            continue;
                        }

                        bottom = tile;
                    }

                    break;
                }

                if (bottom != null && bottom.GetComponent<Collectable>() != null)
                {
                    collectablesToDestroy.Add(bottom);
                    tiles[tileIndex] = null;
                }
            }

            if (collectablesToDestroy.Count > 0)
            {
                foreach (var tile in collectablesToDestroy)
                {
                    gameState.AddCollectable(tile.GetComponent<Collectable>().type);
                    UpdateScore(gameConfig.GetTileScore(tile.GetComponent<Tile>()));

                    var fx = fxPool.collectableExplosion.GetObject();
                    fx.transform.position = tile.transform.position;

                    SoundManager.instance.PlaySound("Collectable");

                    tile.Explode();
                    tile.GetComponent<PooledObject>().pool.ReturnObject(tile.gameObject);
                }

                gameUi.UpdateGoals(gameState);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Internal method that actually applies the gravity to the current level.
        /// </summary>
        private void ApplyGravityInternal()
        {
            deneme2++;
            var fallingSoundPlayed = false;
            for (var i = 0; i < level.width; i++) //MEVCUT TASLAR ASAGI KAYMASI ICIN
            {
                for (var j = level.height - 1; j >= 0; j--)
                {
                    var tileIndex = i + (j * level.width);
                    if (GetTile(i, j) == null ||
                        GetTile(i, j).GetComponent<SpecialBlock>() != null)
                    {
                        continue;
                    }

                    // Find bottom.
                    var bottom = -1;
                    for (var k = j; k < level.height; k++)
                    {
                        var idx = i + (k * level.width);
                        if (tiles[idx] == null && !(level.tiles[idx] is HoleTile))
                        {
                            bottom = k;
                        }
                        else if (tiles[idx] != null && tiles[idx].GetComponent<SpecialBlock>() != null)
                        {
                            break;
                        }
                    }

                    if (bottom != -1)
                    {
                        var tile = GetTile(i, j);
                        if (tile != null) //EKRANDAKİ DÜŞEBİLECEK TAŞLARI DÜŞÜRME /AŞAĞI KAYDIRMA VE YENİ INDEX NO VERME-ESKİ INDEX NULL'A ÇEKİLİYOR / YENİ TASLAR İÇİN DEGİL MEVCUT İÇİN
                        {
                            var numTilesToFall = bottom - j;
                            tiles[tileIndex + (numTilesToFall * level.width)] = tiles[tileIndex];
                            var tween = LeanTween.move(tile,
                                tilePositions[tileIndex + level.width * numTilesToFall],
                                0.3f);
                            tween.setEase(LeanTweenType.easeInQuad);
                            tween.setOnComplete(() =>
                            {
                                if (tile.GetComponent<Tile>() != null)
                                {
                                    tile.GetComponent<Tile>().y += numTilesToFall;
                                    if (tile.activeSelf && tile.GetComponent<Animator>() != null)
                                    {
                                        tile.GetComponent<Animator>().SetTrigger("Falling");
                                        if (!fallingSoundPlayed)
                                        {
                                            fallingSoundPlayed = true;
                                            SoundManager.instance.PlaySound("CandyFalling");
                                        }
                                    }
                                }
                            });
                            tiles[tileIndex] = null;
                        }
                    }
                }
            }

            for (var i = 0; i < level.width; i++) // BOŞLUKLAR İÇİN YENİ TAS OLUSTURUP AŞAĞI YERİNE DÜŞÜREN VE INDEX NUMARASI VEREN YER
            {
                var numEmpties = 0;
                for (var j = 0; j < level.height; j++)
                {
                    var idx = i + (j * level.width);
                    if (tiles[idx] == null && !(level.tiles[idx] is HoleTile))
                    {
                        numEmpties += 1;
                    }
                    else if (tiles[idx] != null && tiles[idx].GetComponent<SpecialBlock>() != null)
                    {
                        break;
                    }
                }

                if (numEmpties > 0)
                {
                    for (var j = 0; j < level.height; j++)
                    {
                        var tileIndex = i + (j * level.width);
                        var isHole = level.tiles[tileIndex] is HoleTile;
                        var isBiscuit = tiles[tileIndex] != null &&
                                        tiles[tileIndex].GetComponent<SpecialBlock>() != null;
                        if (isBiscuit)
                        {
                            break;
                        }

                        if (tiles[tileIndex] == null && !isHole)
                        {
                            var tile = CreateTile(i, j, true);
                            if (PhotonNetwork.IsConnected)
                            {
                                _pView.RPC("RakipGravityEsleme", RpcTarget.OthersBuffered, (int)tile.GetComponent<Candy>().color);
                            }
                            else
                            {

                            }
                            var sourcePos = tilePositions[i];
                            var targetPos = tilePositions[tileIndex];
                            var pos = sourcePos;
                            pos.y = tilePositions[i].y + (numEmpties * (tileH));
                            --numEmpties;
                            tile.transform.position = pos;
                            var tween = LeanTween.move(tile,
                                targetPos,
                                0.3f);
                            tween.setEase(LeanTweenType.easeInQuad);
                            tween.setOnComplete(() =>
                            {
                                if (tile.activeSelf && tile.GetComponent<Animator>() != null)
                                {
                                    tile.GetComponent<Animator>().SetTrigger("Falling");
                                }
                            });
                            tiles[tileIndex] = tile;
                        }
                    }
                }
            }
        }

        [PunRPC]
        public void RakipGravityEsleme(int _tileColor)
        {
            _rakipGravityTiles.Add(_tileColor);
            Debug.Log("GRAVITY GELEN TILES_____"+ _tileColor);
        }

        private void RakipApplyGravityInternal()
        {
            var fallingSoundPlayed = false;
            for (var i = 0; i < level.width; i++) //MEVCUT TASLAR ASAGI KAYMASI ICIN ve MEVCUT TILES LISTESINDEN CIKARILMASI ICIN
            {
                for (var j = level.height - 1; j >= 0; j--)
                {
                    var tileIndex = i + (j * level.width);
                    if (GetTile(i, j) == null ||
                        GetTile(i, j).GetComponent<SpecialBlock>() != null)
                    {
                        continue;
                    }

                    // Find bottom.
                    var bottom = -1;
                    for (var k = j; k < level.height; k++)
                    {
                        var idx = i + (k * level.width);
                        if (tiles[idx] == null && !(level.tiles[idx] is HoleTile))
                        {
                            bottom = k;
                        }
                        else if (tiles[idx] != null && tiles[idx].GetComponent<SpecialBlock>() != null)
                        {
                            break;
                        }
                    }

                    if (bottom != -1)
                    {
                        var tile = GetTile(i, j);
                        if (tile != null)
                        {
                            var numTilesToFall = bottom - j;
                            tiles[tileIndex + (numTilesToFall * level.width)] = tiles[tileIndex];
                            var tween = LeanTween.move(tile,
                                tilePositions[tileIndex + level.width * numTilesToFall],
                                0.3f);
                            tween.setEase(LeanTweenType.easeInQuad);
                            tween.setOnComplete(() =>
                            {
                                if (tile.GetComponent<Tile>() != null)
                                {
                                    tile.GetComponent<Tile>().y += numTilesToFall;
                                    if (tile.activeSelf && tile.GetComponent<Animator>() != null)
                                    {
                                        tile.GetComponent<Animator>().SetTrigger("Falling");
                                        if (!fallingSoundPlayed)
                                        {
                                            fallingSoundPlayed = true;
                                            SoundManager.instance.PlaySound("CandyFalling");
                                        }
                                    }
                                }
                            });
                            tiles[tileIndex] = null;
                        }
                    }
                }
            }

            for (var i = 0; i < level.width; i++)
            {
                var numEmpties = 0;
                for (var j = 0; j < level.height; j++) //BOSALAN YERLERİN TOPLAM SAYISINI TESPİT EDİYOR
                {
                    var idx = i + (j * level.width);
                    if (tiles[idx] == null && !(level.tiles[idx] is HoleTile))
                    {
                        numEmpties += 1;
                    }
                    else if (tiles[idx] != null && tiles[idx].GetComponent<SpecialBlock>() != null)
                    {
                        break;
                    }
                }

                if (numEmpties > 0)
                {
                    for (var j = 0; j < level.height; j++)
                    {
                        var tileIndex = i + (j * level.width);
                        var isHole = level.tiles[tileIndex] is HoleTile;
                        var isBiscuit = tiles[tileIndex] != null &&
                                        tiles[tileIndex].GetComponent<SpecialBlock>() != null;
                        if (isBiscuit)
                        {
                            break;
                        }

                        if (tiles[tileIndex] == null && !isHole)
                        {
                            var tile = tilePool.GetCandyPool((CandyColor)(_rakipGravityTiles[_rakipGravityTileSiram])).GetObject();
                            _rakipGravityTileSiram++;
                            var sourcePos = tilePositions[i];
                            var targetPos = tilePositions[tileIndex];
                            var pos = sourcePos;
                            pos.y = tilePositions[i].y + (numEmpties * (tileH));
                            --numEmpties;
                            tile.transform.position = pos;
                            var tween = LeanTween.move(tile,
                                targetPos,
                                0.3f);
                            tween.setEase(LeanTweenType.easeInQuad);
                            tween.setOnComplete(() =>
                            {
                                if (tile.activeSelf && tile.GetComponent<Animator>() != null)
                                {
                                    tile.GetComponent<Animator>().SetTrigger("Falling");
                                }
                            });
                            tiles[tileIndex] = tile;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Expands the chocolate in the current level.
        /// </summary>
        private void ExpandChocolate()
        {
            if (explodedChocolate)
            {
                return;
            }

            var chocolates = tiles.FindAll(x => x != null && x.GetComponent<Chocolate>() != null);
            if (chocolates.Count > 0)
            {
                chocolates.Shuffle();

                var foundSpot = false;
                foreach (var chocolate in chocolates)
                {
                    var x = chocolate.GetComponent<Tile>().x;
                    var y = chocolate.GetComponent<Tile>().y;
                    var leftTile = GetTile(x - 1, y);
                    var rightTile = GetTile(x + 1, y);
                    var topTile = GetTile(x, y + 1);
                    var bottomTile = GetTile(x, y - 1);
                    var neighbourTiles = new List<GameObject> { leftTile, rightTile, topTile, bottomTile };
                    foreach (var neighbour in neighbourTiles)
                    {
                        if (neighbour != null &&
                            neighbour.GetComponent<SpecialBlock>() == null)
                        {
                            CreateChocolate(neighbour.GetComponent<Tile>().x, neighbour.GetComponent<Tile>().y);
                            neighbour.GetComponent<PooledObject>().pool.ReturnObject(neighbour);
                            foundSpot = true;

                            SoundManager.instance.PlaySound("ChocolateExpand");

                            break;
                        }
                    }

                    if (foundSpot)
                    {
                        possibleSwaps = DetectPossibleSwaps();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Highlights a random match as a suggestion to the player when he is idle for some time.
        /// </summary>
        /// <returns>The coroutine.</returns>
        private IEnumerator HighlightRandomMatchAsync()
        {
            yield return new WaitForSeconds(GameplayConstants.TimeBetweenRandomMatchSuggestions);
            HighlightRandomMatch();
        }

        /// <summary>
        /// Highlights a random match as a suggestion to the player when he is idle for some time.
        /// </summary>
        public void HighlightRandomMatch()
        {
            if (currentlyAwarding)
            {
                return;
            }

            ClearSuggestedMatch();

            var swapsCopy = new List<Swap>();
            swapsCopy.AddRange(possibleSwaps);
            swapsCopy.RemoveAll(x =>
            {
                var x1 = x.tileA.GetComponent<Tile>().x;
                var y1 = x.tileA.GetComponent<Tile>().y;
                var x2 = x.tileB.GetComponent<Tile>().x;
                var y2 = x.tileB.GetComponent<Tile>().y;
                var idx1 = x1 + (y1 * level.width);
                var idx2 = x2 + (y2 * level.width);
                return (level.tiles[idx1] != null && level.tiles[idx1].elementType == ElementType.Ice) ||
                       (level.tiles[idx2] != null && level.tiles[idx2].elementType == ElementType.Ice) ||
                       x.tileA.GetComponent<SpecialBlock>() != null ||
                       x.tileB.GetComponent<SpecialBlock>() != null;
            });
            if (swapsCopy.Count > 0)
            {
                var idx = UnityEngine.Random.Range(0, swapsCopy.Count);
                var swap = swapsCopy[idx];
                if (!swap.tileA || !swap.tileB)
                {
                    return;
                }

                var x1 = swap.tileA.GetComponent<Tile>().x;
                var y1 = swap.tileA.GetComponent<Tile>().y;
                var x2 = swap.tileB.GetComponent<Tile>().x;
                var y2 = swap.tileB.GetComponent<Tile>().y;
                SetTile(swap.tileA, x2, y2);
                SetTile(swap.tileB, x1, y1);

                if (HasMatch(x2, y2))
                {
                    suggestedMatch.AddRange(GetTilesToHighlight(swap.tileA, x2, y2));
                }
                else if (HasMatch(x1, y1))
                {
                    suggestedMatch.AddRange(GetTilesToHighlight(swap.tileB, x1, y1));
                }

                foreach (var tile in suggestedMatch)
                {
                    if (tile.gameObject.activeSelf && tile.GetComponent<Animator>() != null)
                    {
                        if (PhotonNetwork.IsConnected)
                        {

                        }
                        else
                        {
                            tile.GetComponent<Animator>().SetTrigger("SuggestedMatch");
                        }
                    }
                }

                SetTile(swap.tileA, x1, y1);
                SetTile(swap.tileB, x2, y2);
            }
            else
            {
                var hasPlayableColorBomb = false;
                GameObject playableColorBomb = null;
                GameObject playableNeighbour = null;
                for (var i = 0; i < level.width; i++)
                {
                    for (var j = 0; j < level.height; j++)
                    {
                        var idx = i + (j * level.width);
                        var tile = tiles[idx];
                        if (tile != null &&
                            tile.GetComponent<ColorBomb>() != null &&
                            level.tiles[idx].elementType == ElementType.None)
                        {
                            playableColorBomb = tile;
                            var left = GetTile(i - 1, j);
                            var right = GetTile(i + 1, j);
                            var top = GetTile(i, j - 1);
                            var bottom = GetTile(i, j + 1);
                            if (left != null && left.GetComponent<Candy>() != null)
                            {
                                hasPlayableColorBomb = true;
                                playableNeighbour = left;
                                break;
                            }
                            if (right != null && right.GetComponent<Candy>() != null)
                            {
                                hasPlayableColorBomb = true;
                                playableNeighbour = right;
                                break;
                            }
                            if (top != null && top.GetComponent<Candy>() != null)
                            {
                                hasPlayableColorBomb = true;
                                playableNeighbour = top;
                                break;
                            }
                            if (bottom != null && bottom.GetComponent<Candy>() != null)
                            {
                                hasPlayableColorBomb = true;
                                playableNeighbour = bottom;
                                break;
                            }
                        }
                    }

                    if (hasPlayableColorBomb)
                    {
                        break;
                    }
                }

                if (hasPlayableColorBomb)
                {
                    suggestedMatch.Add(playableColorBomb);
                    suggestedMatch.Add(playableNeighbour);
                    foreach (var tile in suggestedMatch)
                    {
                        if (tile.gameObject.activeSelf && tile.GetComponent<Animator>() != null)
                        {
                            tile.GetComponent<Animator>().SetTrigger("SuggestedMatch");
                        }
                    }
                }
                else
                {
                    gameScene.OpenPopup<RegenLevelPopup>("Popups/RegenLevelPopup");
                    StartCoroutine(RegenerateLevel());
                }
            }
        }

        public void RegerateLevelCalistirma() // PVP İÇİN
        {
            gameScene.OpenPopup<RegenLevelPopup>("Popups/RegenLevelPopup");
            StartCoroutine(RegenerateLevel());
        }

        private GameObject ColorBombCevreKontrol(GameObject patlayanColorBomb)
        {
            var hasPlayableColorBomb = false;
            GameObject playableColorBomb = null;
            GameObject playableNeighbour = null;
            for (var i = 0; i < level.width; i++)
            {
                for (var j = 0; j < level.height; j++)
                {
                    var idx = i + (j * level.width);
                    var tile = tiles[idx];
                    if (tile != null &&
                        tile.GetComponent<ColorBomb>() != null &&
                        tile.gameObject == patlayanColorBomb)
                    {
                        playableColorBomb = tile;
                        var left = GetTile(i - 1, j);
                        var right = GetTile(i + 1, j);
                        var top = GetTile(i, j - 1);
                        var bottom = GetTile(i, j + 1);
                        if (left != null && left.GetComponent<Candy>() != null)
                        {
                            hasPlayableColorBomb = true;
                            playableNeighbour = left;
                            break;
                        }
                        if (right != null && right.GetComponent<Candy>() != null)
                        {
                            hasPlayableColorBomb = true;
                            playableNeighbour = right;
                            break;
                        }
                        if (top != null && top.GetComponent<Candy>() != null)
                        {
                            hasPlayableColorBomb = true;
                            playableNeighbour = top;
                            break;
                        }
                        if (bottom != null && bottom.GetComponent<Candy>() != null)
                        {
                            hasPlayableColorBomb = true;
                            playableNeighbour = bottom;
                            break;
                        }
                    }
                }

                if (hasPlayableColorBomb)
                {
                    break;
                }
            }
            return playableNeighbour;
        }

        public void ColorBombPatlat(GameObject secilenTile)
        {
            var combo = comboDetector.GetCombo(ColorBombCevreKontrol(secilenTile).GetComponent<Tile>(),
                      secilenTile.GetComponent<Tile>());
            combo.Resolve(this, tiles, fxPool);
            if (secilenTile != null)
            {
                //ExplodeTile(secilenTile);
            }
            else
            {

            }
            if (PhotonNetwork.IsConnected && _hamleSirasi)
            {
                _pView.RPC("RakipColorBomb", RpcTarget.Others, (int)_comboTileIdx);
            }
            else
            {

            }
        }

        /// <summary>
        /// Regenerates the level when no matches are possible.
        /// </summary>
        /// <returns>The coroutine.</returns>
        private IEnumerator RegenerateLevel()
        {
            _pView.RPC("RakipRegenListesiSifirla", RpcTarget.Others, null);

            yield return new WaitForSeconds(2.0f);
            for (var i = 0; i < level.width; i++)
            {
                for (var j = 0; j < level.height; j++)
                {
                    var idx = i + (j * level.width);
                    var tile = tiles[idx];
                    if (tile != null &&
                        tile.GetComponent<Candy>() != null &&
                        !tile.GetComponent<StripedCandy>() &&
                        !tile.GetComponent<WrappedCandy>())
                    {
                        var newTile = CreateTile(i, j, false);
                        newTile.transform.position = tile.transform.position;
                        _pView.RPC("RakipRegen", RpcTarget.Others,(int)newTile.GetComponent<Candy>().color,(int)i,(int)j);
                        tile.GetComponent<PooledObject>().pool.ReturnObject(tile);
                        SetTile(newTile, i, j);
                    }
                }
            }
            if (PhotonNetwork.IsConnected)
            {

            }
            else
            {
                suggestedMatchCoroutine = StartCoroutine(HighlightRandomMatchAsync());
            }
            possibleSwaps = DetectPossibleSwaps();

        }

        [PunRPC]
        public void RakipRegenListesiSifirla()
        {
            gameScene.OpenPopup<RegenLevelPopup>("Popups/RegenLevelPopup");
            _rakipRegenTileListem.Clear();
            _tileListeSiram = 0;
        }

        [PunRPC]
        public void RakipRegen(int _colorType, int x, int y)
        {
            _rakipRegenTileListem.Add(_colorType);
            var idx = x + (y * level.width);
            var tile = tiles[idx];

            if (tile != null &&
                tile.GetComponent<Candy>() != null &&
                !tile.GetComponent<StripedCandy>() &&
                !tile.GetComponent<WrappedCandy>())
            {
                var regenTile = tilePool.GetCandyPool((CandyColor)(_rakipRegenTileListem[_tileListeSiram])).GetObject();
                regenTile.transform.position = tile.transform.position;
                tile.GetComponent<PooledObject>().pool.ReturnObject(tile);
                RakipSetTile(regenTile, x, y);
            }
            _tileListeSiram++;
        }

        /// <summary>
        /// Returns the tiles that should be highlighted for the randomly suggested match.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="x">The x-coordinate.</param>
        /// <param name="y">The y-coordinate.</param>
        /// <returns>A list containing all the tiles that should be highlighted for the randomly suggested match.</returns>
        private List<GameObject> GetTilesToHighlight(GameObject tile, int x, int y)
        {
            var tilesToHighlight = new List<GameObject>();

            tilesToHighlight.Add(tile);
            if (HasHorizontalMatch(x, y))
            {
                var i = x - 1;
                while (i >= 0 && GetTile(i, y) != null && GetTile(i, y).GetComponent<Candy>() != null &&
                       GetTile(i, y).GetComponent<Candy>().color == tile.GetComponent<Candy>().color)
                {
                    tilesToHighlight.Add(GetTile(i, y));
                    --i;
                }

                i = x + 1;
                while (i < level.width && GetTile(i, y) != null && GetTile(i, y).GetComponent<Candy>() != null &&
                       GetTile(i, y).GetComponent<Candy>().color == tile.GetComponent<Candy>().color)
                {
                    tilesToHighlight.Add(GetTile(i, y));
                    ++i;
                }
            }
            else if (HasVerticalMatch(x, y))
            {
                var j = y - 1;
                while (j >= 0 && GetTile(x, j) != null && GetTile(x, j).GetComponent<Candy>() != null &&
                       GetTile(x, j).GetComponent<Candy>().color == tile.GetComponent<Candy>().color)
                {
                    tilesToHighlight.Add(GetTile(x, j));
                    --j;
                }

                j = y + 1;
                while (j < level.height && GetTile(x, j) != null && GetTile(x, j).GetComponent<Candy>() != null &&
                       GetTile(x, j).GetComponent<Candy>().color == tile.GetComponent<Candy>().color)
                {
                    tilesToHighlight.Add(GetTile(x, j));
                    ++j;
                }
            }

            return tilesToHighlight;
        }

        /// <summary>
        /// Clears the randomly suggested match.
        /// </summary>
        private void ClearSuggestedMatch()
        {
            foreach (var tile in suggestedMatch)
            {
                if (tile.gameObject.activeSelf)
                {
                    tile.GetComponent<Animator>().SetTrigger("Reset");
                }
            }

            suggestedMatch.Clear();
        }

        /// <summary>
        /// Awards the special candies at the end of the level.
        /// </summary>
        public void AwardSpecialCandies()
        {
            StartCoroutine(AwardSpecialCandiesAsync());
        }

        /// <summary>
        /// Awards the special candies at the end of the level.
        /// </summary>
        /// <returns>The coroutine.</returns>
        private IEnumerator AwardSpecialCandiesAsync()
        {
            _kalanLimit = currentLimit;
            PlayerPrefs.SetInt("KalanLimit", _kalanLimit);
            currentlyAwarding = true;
            yield return new WaitForSeconds(1.0f);
            gameScene.OpenPopup<SpecialCandiesAwardPopup>("Popups/SpecialCandiesAwardPopup");
            yield return new WaitForSeconds(1.5f);
            while (currentLimit > 0)
            {
                int randomIdx;
                do
                {
                    randomIdx = UnityEngine.Random.Range(0, tiles.Count);
                } while (tiles[randomIdx] == null || (tiles[randomIdx] != null && !IsNormalCandy(tiles[randomIdx])));

                var tile = tiles[randomIdx];
                tiles[randomIdx] = null;
                if (tile != null)
                {
                    tile.GetComponent<PooledObject>().pool.ReturnObject(tile.gameObject);
                }

                if (level.awardedSpecialCandyType == AwardedSpecialCandyType.Striped)
                {
                    if (UnityEngine.Random.Range(0, 2) % 2 == 0)
                    {
                        CreateHorizontalStripedTile(randomIdx % level.width, randomIdx / level.width,
                            CandyColor.Blue);
                    }
                    else
                    {
                        CreateVerticalStripedTile(randomIdx % level.width, randomIdx / level.width,
                            CandyColor.Blue);
                    }
                }
                else
                {
                    CreateWrappedTile(randomIdx % level.width, randomIdx / level.width, CandyColor.Blue);
                }

                SoundManager.instance.PlaySound("BoosterAward");

                currentLimit -= 1;
                UpdateLimitText();
                yield return new WaitForSeconds(GameplayConstants.TimeBetweenRewardedCandiesCreation);
            }

            do
            {
                GameObject tileToExplode = null;
                foreach (var tile in tiles)
                {
                    if (tile != null && IsSpecialCandy(tile))
                    {
                        tileToExplode = tile;
                        break;
                    }
                }

                if (tileToExplode != null)
                {
                    ExplodeTile(tileToExplode);
                    ApplyGravity();
                    yield return new WaitForSeconds(GameplayConstants.TimeBetweenRewardedCandiesExplosion);
                }
            } while (tiles.Find(x => x != null && IsSpecialCandy(x)) != null);

            gameScene.OpenWinPopup();
        }

        /// <summary>
        /// Checks if the specified tile is a regular candy.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <returns>True if the specified tile is a regular candy; false otherwise.</returns>
        private bool IsNormalCandy(GameObject tile)
        {
            return tile.GetComponent<Candy>() != null &&
                   tile.GetComponent<StripedCandy>() == null &&
                   tile.GetComponent<WrappedCandy>() == null;
        }

        /// <summary>
        /// Checks if the specified tile is a special candy.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <returns>True if the specified tile is a special candy; false otherwise.</returns>
        private bool IsSpecialCandy(GameObject tile)
        {
            return tile.GetComponent<StripedCandy>() != null ||
                   tile.GetComponent<WrappedCandy>() != null ||
                   tile.GetComponent<ColorBomb>() != null;
        }

        /// <summary>
        /// Returns a random color from the available colors in the current level.
        /// </summary>
        /// <returns>A random color from the available colors in the current level.</returns>
        /*
        private CandyColor GetRandomCandyColor()
        {
            var eligibleColors = new List<CandyColor>();
            eligibleColors.AddRange(level.availableColors);
            var idx = UnityEngine.Random.Range(0, eligibleColors.Count);
            return eligibleColors[idx];
        }
        */

        //Ustteki Fonksiyonun Yenisi
        private CandyColor GetRandomCandyColor()
        {
            var eligibleColors = new List<CandyColor>();
            eligibleColors.AddRange(level.availableColors);
            var idx = UnityEngine.Random.Range(0, eligibleColors.Count);
            return eligibleColors[idx];
        }

        /// <summary>
        /// Called when the booster mode is enabled.
        /// </summary>
        public void OnBoosterModeEnabled()
        {
            if (suggestedMatchCoroutine != null)
            {
                StopCoroutine(suggestedMatchCoroutine);
            }
            ClearSuggestedMatch();
        }

        /// <summary>
        /// Called when the booster mode is disabled.
        /// </summary>
        public void OnBoosterModeDisabled()
        {
        }

        /// <summary>
        /// Consumes the specified booster.
        /// </summary>
        /// <param name="button">The used booster button.</param>
        private void ConsumeBooster(BuyBoosterButton button)
        {
            if (PhotonNetwork.IsConnected)
            {
                GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>().PlayerSkillSayacSifirlama();
                if ((int)button.boosterType==0)
                {
                    GameObject.Find("ServerGameUIKontrol").GetComponent<ServerGameUIKontrol>().PlayerHammerKapatma();
                }
                else
                {

                }
            }
            else
            {
                var playerPrefsKey = string.Format("num_boosters_{0}", (int)button.boosterType);
                var numBoosters = PlayerPrefs.GetInt(playerPrefsKey);
                numBoosters -= 1;
                PlayerPrefs.SetInt(playerPrefsKey, numBoosters);
                button.UpdateAmount(numBoosters);
            }
        }
    }
}
