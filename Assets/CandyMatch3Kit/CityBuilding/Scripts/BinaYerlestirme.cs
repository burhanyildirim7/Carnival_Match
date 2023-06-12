using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
//using static UnityEditor.PlayerSettings;


public class BinaYerlestirme : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private CameraMovement _cameraMovement;

    //[SerializeField] private LayerMask _layer;

    private bool _binaAlaniSecildi;

    public bool _cameraHareketEtti;

    //[SerializeField] private Tilemap _tileMap;

    //private BoundsInt bounds;

    //[SerializeField] private List<TileData> _tileDatas;

    //private Dictionary<TileBase, TileData> _dataFromTiles;

    private void Awake()
    {
        /*
        _dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (var tileData in _tileDatas)
        {
            foreach (var tile in tileData._mapTiles)
            {
                _dataFromTiles.Add(tile, tileData);
            }
        }
        */
    }

    void Start()
    {
        _binaAlaniSecildi = false;
        //bounds = _tileMap.cellBounds;
        //Debug.Log("bounds ----" + bounds);
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                    _cameraHareketEtti = false;
                    break;
                case TouchPhase.Moved:
                    _cameraHareketEtti = true;
                    break;
            }
        }
        else
        {

        }

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            if (!_cameraHareketEtti)
            {
                if (hit.collider != null)
                {
                    if (hit.collider != null)
                    {
                        if (!_binaAlaniSecildi)
                        {
                            Debug.Log("BINA  == " + hit.collider.gameObject.name);
                            _cameraMovement.Focus(hit.collider.gameObject.transform.position);
                            _binaAlaniSecildi = true;








                            //Vector3Int _oldGridPosition = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
                            //Vector3Int _gridPosition = new Vector3Int(_oldGridPosition.x, _oldGridPosition.y, 0);
                            //Debug.Log("Grid Position ----" + _gridPosition);
                            //TileBase clickedTile = _tileMap.GetTile(_gridPosition);
                            //_tileMap.SetTile(_gridPosition, _dataFromTiles[clickedTile]._mapTileObjects[0]);
                            /*
                            if (clickedTile != null)
                            {

                                var tileobje = _tileMap.GetTile(_gridPosition);
                                Debug.Log("amına koyam tilemap ----" + tileobje);
                                _tileMap.RefreshTile(_gridPosition);
                            }
                            */
                        }
                        else
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    _binaAlaniSecildi = false;
                }
            }
            else
            {

            }
        }
        else
        {

        }

    }


}
