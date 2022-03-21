using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    //трансформ, который меняем по нажтию на стрелку
    [SerializeField] private Transform _transform;
    //стандратное значение
    [SerializeField] private int _transformMove = 514;   
    [Range(-1,1)]
    [SerializeField] private int _direction;
    [SerializeField] private float _speed;
    private bool _move = false;

    //получаем direction - направление, влево или вправо
    public void Click()
    {
        if(_direction == 0)
        {
            Debug.LogWarning("Направление = 0, ничего не происходит");
        }
        else
        {
            _transform.position = new Vector2(_transform.position.x + (_transformMove * _direction), _transform.position.y);
            _move = true;
        }
    }
}
