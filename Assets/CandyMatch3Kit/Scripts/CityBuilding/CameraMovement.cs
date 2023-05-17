using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float _leftLimit;

    [SerializeField]
    private float _rightLimit;

    [SerializeField]
    private float _bottomLimit;

    [SerializeField]
    private float _upperLimit;

    [SerializeField]
    private float _yukseklikLimit;

    private Camera _cam;

    private bool _moveAllowed;

    private Vector3 _touchPos;


    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }


    private void Start()
    {

    }


    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 2)
            {

            }
            else
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                        {
                            _moveAllowed = false;
                        }
                        else
                        {
                            _moveAllowed = true;
                        }
                        _touchPos = _cam.ScreenToWorldPoint(touch.position);
                        break;
                    case TouchPhase.Moved:
                        if (_moveAllowed)
                        {
                            Vector3 direction = _touchPos - _cam.ScreenToWorldPoint(touch.position);
                            _cam.transform.position += direction;


                            transform.position = new Vector3
                                (
                                Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit),
                                _yukseklikLimit,
                                Mathf.Clamp(transform.position.z, _bottomLimit, _upperLimit)
                                );

                        }
                        break;
                }
            }
        }
    }


}
