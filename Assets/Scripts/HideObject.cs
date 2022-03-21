using System.Collections.Generic;
using UnityEngine;

public class HideObject : MonoBehaviour
{
    [Tooltip("Элементы скрываемые при старте сцены")]
    [SerializeField] private List<GameObject> _hidenGameObjects1;
    [Tooltip("Элементы скрываемые при 1 повороте сцены")]
    [SerializeField] private List<GameObject> _hidenGameObjects2;
    [Tooltip("Элементы скрываемые при 2 повороте сцены")]
    [SerializeField] private List<GameObject> _hidenGameObjects3;
    [Tooltip("Элементы скрываемые при 3 повороте сцены")]
    [SerializeField] private List<GameObject> _hidenGameObjects4;


    //получаем количество поворотов
    public void HidingObjects(int countRotate)
    {
        switch (countRotate)
        {
            case 0:
                {
                    if (_hidenGameObjects1 == null)
                    {
                        return;
                    }
                    //скрываем объекты первого списка
                    foreach (GameObject go in _hidenGameObjects1)
                    {
                        go.SetActive(false);
                    }
                    //и показываем предыдущего
                    foreach (GameObject go in _hidenGameObjects4)
                    {
                        go.SetActive(true);
                    }
                }
                break;
            case 1:
                {
                    if (_hidenGameObjects2 == null)
                    {
                        return;
                    }
                    //скрываем объекты первого списка
                    foreach (GameObject go in _hidenGameObjects2)
                    {
                        go.SetActive(false);
                    }
                    //и показываем предыдущего
                    foreach (GameObject go in _hidenGameObjects1)
                    {
                        go.SetActive(true);
                    }
                }
                break;
            case 2:
                {
                    if (_hidenGameObjects3 == null)
                    {
                        return;
                    }
                    //скрываем объекты первого списка
                    foreach (GameObject go in _hidenGameObjects3)
                    {
                        go.SetActive(false);
                    }
                    //и показываем предыдущего
                    foreach (GameObject go in _hidenGameObjects2)
                    {
                        go.SetActive(true);
                    }
                }
                break;
            case 3:
                {
                    if (_hidenGameObjects4 == null)
                    {
                        return;
                    }
                    //скрываем объекты первого списка
                    foreach (GameObject go in _hidenGameObjects4)
                    {
                        go.SetActive(false);
                    }
                    //и показываем предыдущего
                    foreach (GameObject go in _hidenGameObjects3)
                    {
                        go.SetActive(true);
                    }
                }
                break;
        }
    }
}
