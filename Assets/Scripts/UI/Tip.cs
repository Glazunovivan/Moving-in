using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Direction
{
    Up,
    Down
};

public class Tip : MonoBehaviour
{
    [SerializeField] private Material _materialTransparent;
    [SerializeField] private Material _mainMaterial;
    [SerializeField] private List<GameObject> _correctObjects;
    [Tooltip("Кидаем сюда любой объект с материалом Transparent")]
    [SerializeField] private Renderer _mainRenderer;

    private GameObject _correctFurnitureObject;
    private int _rand;


    //для анимации
    private bool _tipStart = false;
    public bool IsTipStart
    {
        get
        {
            return _tipStart;
        }
    }

    [SerializeField] private float _speed;
    private float _currentTransparentAlfa = 1f;
    private Direction dir = Direction.Down;

    public void ClickForTips()
    {
        //скрываем предыдущий объект
        HideTipFurnitureObject();
        
        //ищем новый объект
        _rand = Random.Range(0, _correctObjects.Count);
        _correctFurnitureObject = _correctObjects[_rand];


        _correctFurnitureObject.SetActive(true);
        _correctFurnitureObject.GetComponent<Furniture>().MainRenderer.material = _materialTransparent;
        //_correctFurnitureObject.GetComponent<Furniture>().MainRenderer.sharedMaterial.color = new Color(60/255f, 211/255f, 235/255f, 0.3f);
        _currentTransparentAlfa = 1f;
        _tipStart = true;
    }

    public void HideTipFurnitureObject()
    {
        if (_rand != -1)
        {
            _correctObjects[_rand].SetActive(false);
            _correctObjects[_rand].GetComponent<Furniture>().MainRenderer.material = _mainMaterial;
        }
    }

    private void Update()
    {
        if (_tipStart)
        {
            AnimationTransparent();
        }
    }

    private void AnimationTransparent()
    {
        float step = _speed * Time.deltaTime;

        if (dir == Direction.Down)
        {
            _currentTransparentAlfa -= step;
            _correctFurnitureObject.GetComponent<Furniture>().MainRenderer.sharedMaterial.color = new Color(60 / 255f, 211 / 255f, 235 / 255f, _currentTransparentAlfa);
            if (_currentTransparentAlfa < 0)
            {
                dir = Direction.Up;
            }
        }
        else if (dir == Direction.Up)
        {
            _currentTransparentAlfa += step;
            _correctFurnitureObject.GetComponent<Furniture>().MainRenderer.sharedMaterial.color = new Color(60 / 255f, 211 / 255f, 235 / 255f, _currentTransparentAlfa);
            if (_currentTransparentAlfa > 1)
            {
                dir = Direction.Down;
            }
        }
    }

    public void StartTip()
    {
        _tipStart = true;
    }
    public void StopTip()
    {
        _tipStart = false;
        _correctFurnitureObject.SetActive(false);
    }
}
