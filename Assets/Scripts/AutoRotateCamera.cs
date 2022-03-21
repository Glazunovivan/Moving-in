using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateCamera : MonoBehaviour
{
    [Tooltip("Количество объектов для первого поворта")]
    [SerializeField] private int _countObjectsForFirstRotate;
    public int CountObjectsForFirstRotate
    {
        get
        {
            return _countObjectsForFirstRotate;
        }
    }

    [Tooltip("Количество объектов для второго поворта")]
    [SerializeField] private int _countObjectsForSecodRotate;
    public int CountObjectsForSecondRotate
    {
        get
        {
            return _countObjectsForSecodRotate;
        }
    }
    [Tooltip("Количество объектов для третьего поворта")]
    [SerializeField] private int _countObjectsForThirdRotate;
    public int CountObjectsForThirdRotate
    {
        get
        {
            return _countObjectsForThirdRotate;
        }
    }
    [Tooltip("Количество объектов для четвертого поворта")]
    [SerializeField] private int _countObjectsForFourthRotate;
    public int CountObjectsForFourthRotate
    {
        get
        {
            return _countObjectsForFourthRotate;
        }
    }

    public int CurrentCountObjects = 0;

    private RotateCamera _rotateCamera;


    private void Start()
    {
        _rotateCamera = GetComponent<RotateCamera>();
    }

    public void CheckCountObjectForRotateCamera(int positionFoRotate)
    {
        switch (positionFoRotate)
        {
            case 0:
                CurrentCountObjects++;
                if (CurrentCountObjects == CountObjectsForFirstRotate)
                {
                    _rotateCamera.StartRotate();
                    CurrentCountObjects = 0;
                }
                break;
            case 1:
                CurrentCountObjects++;
                if (CurrentCountObjects == CountObjectsForSecondRotate)
                {
                    _rotateCamera.StartRotate();
                    CurrentCountObjects = 0;
                }
                break;
            case 2:
                CurrentCountObjects++;
                if (CurrentCountObjects == CountObjectsForThirdRotate)
                {
                    _rotateCamera.StartRotate();
                    CurrentCountObjects = 0;
                }
                break;
            case 3:
                CurrentCountObjects++;
                if (CurrentCountObjects == CountObjectsForFourthRotate)
                {
                    _rotateCamera.StartRotate();
                    CurrentCountObjects = 0;
                }
                break;
        }
    }

    public void CheckCountObjectForRotateAfterRotate(int positionFoRotate)
    {
        switch (positionFoRotate)
        {
            case 0:
                if (CurrentCountObjects == CountObjectsForFirstRotate)
                {
                    _rotateCamera.StartRotate();
                }
                break;
            case 1:
                if (CurrentCountObjects == CountObjectsForSecondRotate)
                {
                    _rotateCamera.StartRotate();
                }
                break;
            case 2:
                if (CurrentCountObjects == CountObjectsForThirdRotate)
                {
                    _rotateCamera.StartRotate();
                }
                break;
            case 3:
                if (CurrentCountObjects == CountObjectsForFourthRotate)
                {
                    _rotateCamera.StartRotate();
                }
                break;
        }
    }

}
