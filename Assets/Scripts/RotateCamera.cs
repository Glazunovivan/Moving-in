using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    //вокруг чего вращаем
    [SerializeField] private GameObject _target;    
    [SerializeField] private bool _isRotate;
    //на сколько градусов вращаем
    [SerializeField] private float _angleRotate = 90f;    
    [SerializeField] private float _speed = 100f;

    //вращение вокруг оси Y
    private Vector3 _axis = new Vector3(0, 1, 0);
    //текущее значения угла
    private float _currentAngle;
    //компонент HideObject
    private HideObject _componentHideObject;
    //позиция камеры 
    private int _positionCameraRotate = 0;
    public int PositionCameraRotate
    {
        get
        {
            return _positionCameraRotate;
        }
    }

    [SerializeField] private CorrectPlacement _isCorrectPlacement;

    private void Start()
    {
        _componentHideObject = GetComponent<HideObject>();
        
        _currentAngle = 0f;
        _positionCameraRotate = 0;
        _componentHideObject.HidingObjects(_positionCameraRotate);
    }

    private void Update()
    {
        if (_isRotate)
        {
            AnimationRotate();
        }
    }

    public void StartRotate()
    {
        _isRotate = true;
    }

    private void AnimationRotate()
    {
        float angle = _speed * Time.deltaTime;
        if (_currentAngle + angle > _angleRotate)
        {
            angle = _angleRotate - _currentAngle;  // доворачиваем до угла
            transform.RotateAround(_target.transform.position, _axis, angle);
            _positionCameraRotate++;
            if(_positionCameraRotate > 3)
            {
                _positionCameraRotate = 0;
                //проверка уровня на правильность
                _isCorrectPlacement.CorrectPlacmentInLevel();
            }
            _componentHideObject.HidingObjects(_positionCameraRotate);
            
            _isRotate = false;
            _currentAngle = 0f;
            //проверка, если вдруг по текущей позиции камеры не нужно расставлять объекты
            GetComponent<AutoRotateCamera>().CheckCountObjectForRotateAfterRotate(_positionCameraRotate);
        }
        else
        {
            _currentAngle += angle;
            transform.RotateAround(_target.transform.position, _axis, angle);
        }
    }

}
