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

    [SerializeField] private LayerMask _layer;

    private bool _binaAlaniSecildi;

    public bool _cameraHareketEtti;

    private Vector3 _mousePos;

    [SerializeField] private Tilemap _tileMap;

    private BoundsInt bounds;

    void Start()
    {
        _binaAlaniSecildi = false;
        bounds = _tileMap.cellBounds;
        Debug.Log("bounds ----" + bounds);
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

        //Tilemap _tilemap;


        if (Input.GetMouseButtonUp(0))
        {

            var obje = _tileMap.GetTile(new Vector3Int(1, 0, 0));

            //obje.GetTileData()
            //RaycastHit2D hit;
            //Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            //_mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.GetRayIntersection(_camera.ScreenPointToRay(Input.mousePosition));

            //Debug.Log("amına koyam tilemap ----" + obje);
            //Debug.DrawLine(Input.mousePosition, Vector2.up, Color.red);



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
                            Vector3Int _oldGridPosition = _tileMap.WorldToCell(_camera.ScreenToWorldPoint(Input.mousePosition));
                            Vector3Int _gridPosition = new Vector3Int(_oldGridPosition.x, _oldGridPosition.y, 0);
                            Debug.Log("Grid Position ----" + _gridPosition);

                            TileBase tile = _tileMap.GetTile(_gridPosition);

                            if (tile != null)
                            {

                                var tileobje = _tileMap.GetTile(_gridPosition);
                                Debug.Log("amına koyam tilemap ----" + tileobje);
                                _tileMap.RefreshTile(_gridPosition);
                            }

                            /*
                            foreach (Vector3Int pos in bounds.allPositionsWithin)
                            {
                                TileBase tile = _tileMap.GetTile(pos);
                                if (tile != null)
                                {

                                    var tileobje = _tileMap.GetTile(pos);
                                    Debug.Log("amına koyam tilemap ----" + tileobje);
                                    _tileMap.RefreshTile(pos);
                                }


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
