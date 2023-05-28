using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;
    [SerializeField] private float _bottomLimit;
    [SerializeField] private float _upperLimit;
    [SerializeField] private float _yukseklikLimit;

    [SerializeField] private float _zoomMin;
    [SerializeField] private float _zoomMax;

    private Camera _cam;

    public bool _moveAllowed;

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
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                if (EventSystem.current.IsPointerOverGameObject(touchOne.fingerId) || EventSystem.current.IsPointerOverGameObject(touchZero.fingerId))
                {
                    return;
                }

                Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

                float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
                float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

                float difference = currentDistTouch - distTouch;

                Zoom(difference * 0.01f);
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


                            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit), Mathf.Clamp(transform.position.y, _bottomLimit, _upperLimit), transform.position.z);

                        }
                        break;
                }

            }
        }
    }

    private void Zoom(float increment)
    {
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize - increment, _zoomMin, _zoomMax);
    }

    public void Focus(Vector3 position)
    {
        Vector3 newPos = new Vector3(position.x, position.y, transform.position.z);
        LeanTween.move(gameObject, newPos, 0.5f);

        //_cam.orthographicSize = Mathf.Clamp(3, _zoomMin, _zoomMax);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit), Mathf.Clamp(transform.position.y, _bottomLimit, _upperLimit), transform.position.z);

        _touchPos = transform.position;
    }


}
