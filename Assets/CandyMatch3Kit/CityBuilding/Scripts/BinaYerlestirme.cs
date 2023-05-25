using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BinaYerlestirme : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private CameraMovement _cameraMovement;

    [SerializeField] private LayerMask _layer;

    private bool _binaAlaniSecildi;

    public bool _cameraHareketEtti;


    void Start()
    {
        _binaAlaniSecildi = false;
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
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (!_cameraHareketEtti)
            {
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layer))
                {
                    if (hit.collider != null)
                    {
                        if (!_binaAlaniSecildi)
                        {
                            Debug.Log("BINA  == " + hit.collider.gameObject.name);
                            _cameraMovement.Focus(hit.collider.gameObject.transform.position);
                            _binaAlaniSecildi = true;
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
