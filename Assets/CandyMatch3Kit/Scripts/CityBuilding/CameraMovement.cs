using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour
{

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
                        }
                        break;
                }
            }
        }
    }
}
