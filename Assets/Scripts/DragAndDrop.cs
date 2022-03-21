using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //через инспектор
    [SerializeField] private Grid _grid;
    [SerializeField] private Furniture _furniture;
    private Furniture _selectedFurniture;
    private Image _image;
    private Camera _camera;
    private bool _isAvailable;

    private AutoRotateCamera _autoRotateCamera;

    public void Awake()
    {
        _image = GetComponent<Image>();
        _camera = GameObject.Find("Camera").GetComponent<Camera>();
        _autoRotateCamera = _camera.GetComponent<AutoRotateCamera>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_furniture.CountRotate == _camera.GetComponent<RotateCamera>().PositionCameraRotate)
        {
            //выключаем подсказку если она работает
            if (_grid.TipComponent.IsTipStart)
            {
                _grid.TipComponent.StopTip();
            }

            _isAvailable = false;
            //gameObject.SetActive(false);
            SelectFurniture(_furniture);
            Debug.Log("Begin Drag");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        var groundPlane = new Plane(Vector3.up, _grid.TransformPlane.position);
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (groundPlane.Raycast(ray, out float position))
        {
            Vector3 worldPosition = ray.GetPoint(position);

            //округление координат -> положение на сетке
            int x = Mathf.RoundToInt(worldPosition.x);
            int y = Mathf.RoundToInt(worldPosition.z);

            //центр объекта
            int centerX = x - _selectedFurniture.GetSizeX() / 2;
            int centerY = y + _selectedFurniture.GetSizeX() / 2;
            //Debug.Log($"x:{x}, y:{y}");
            //Debug.Log($"Center x:{centerX}, y:{centerY}");

            _isAvailable = true;

            //проверка на границы
            if (x < 0 || x > _grid.GridSize.x - _selectedFurniture.GetSizeX()) _isAvailable = false;
            if (y < 0 || y > _grid.GridSize.y - _selectedFurniture.GetSizeY()) _isAvailable = false;

            _selectedFurniture.SetTransperent();
            
            //if (_isAvailable && _grid.IsPlaceTaken(x, y, _selectedFurniture)) _isAvailable = false;

            _selectedFurniture.transform.position = new Vector3(x, 0, y);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        //сделать проверку на границы
        if (_isAvailable)
        {
            _selectedFurniture.SetNormalColor();
            _grid.CheckCorrectPosition(_selectedFurniture);
            OnPlace(Mathf.RoundToInt(_selectedFurniture.gameObject.transform.position.x), Mathf.RoundToInt(_selectedFurniture.gameObject.transform.position.y));
            //отключаем компонент, чтобы не было возможности установить объект снова
            GetComponent<DragAndDrop>().enabled = false;
            _image.color = new Color(1, 1, 1, 0.5f);
        }
        else
        {
            Destroy(_selectedFurniture.gameObject);
        }
        
    }

    //выбрать объект
    public void SelectFurniture(Furniture furniturePrefab)
    {
        if (_selectedFurniture != null)
        {
            Destroy(_selectedFurniture.gameObject);
        }
        _selectedFurniture = Instantiate(furniturePrefab);
        //_selectedFurniturePosition = furniturePrefab.gameObject.transform.position;
        _selectedFurniture.gameObject.transform.SetParent(_grid.ParentForFurniture.transform);
    }

    //разместить объект
    public void OnPlace(int positionX, int positionY)
    {
        for (int x = 0; x < _selectedFurniture.GetSizeX(); x++)
        {
            for (int y = 0; y < _selectedFurniture.GetSizeY(); y++)
            {
                _grid.GridFurniture[positionX + x, positionY + y] = _selectedFurniture;
            }
        }
        _autoRotateCamera.CheckCountObjectForRotateCamera(_selectedFurniture.CountRotate);
        _selectedFurniture.SetNormalColor();
        _selectedFurniture = null;
    }

}
