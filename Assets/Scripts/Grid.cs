using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2Int _gridSize = new Vector2Int(10, 10);
    public Vector2Int GridSize
    {
        get
        {
            return _gridSize;
        }
    }

    private Furniture[,] _grid;
    public Furniture[,] GridFurniture
    {
        get
        {
            return _grid;
        }
    }

    private Furniture _selectedFurniture;
    [SerializeField] Vector3 _selectedFurniturePosition;
    [SerializeField] private Camera _camera;

    [Tooltip("Позиция поверхности, на которую будем ставить фурнитур")]
    [SerializeField] private Transform _transformPlane;
    public Transform TransformPlane
    {
        get
        {
            return _transformPlane;
        }
    }

    [SerializeField] private GameObject _parentForFurniture;
    public GameObject ParentForFurniture
    {
        get
        {
            return _parentForFurniture;
        }
    }

    //public bool IsAvailable = true;
    private CorrectPlacement _isCorrectPlacment;

    //для подсказок
    [SerializeField] private Tip _tipComponent;
    public Tip TipComponent
    {
        get
        {
            return _tipComponent;
        }
    }




    private void Start()
    {
        transform.SetParent(_parentForFurniture.transform);
        _grid = new Furniture[_gridSize.x, _gridSize.y];
        _isCorrectPlacment = GetComponent<CorrectPlacement>();
        if (_camera == null)
        {
            Debug.LogWarning("Отстутствует ссылка на камеру в поле _camera!");
        }
    }

    void Update()
    {
        //if (_selectedFurniture != null)
        //{
        //    var groundPlane = new Plane(Vector3.up, _transformPlane.position);
        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        //    if (groundPlane.Raycast(ray, out float position))
        //    {
        //        Vector3 worldPosition = ray.GetPoint(position);

        //        //округление координат -> положение на сетке
        //        int x = Mathf.RoundToInt(worldPosition.x);
        //        int y = Mathf.RoundToInt(worldPosition.z);

        //        //по центру
        //        int centerX = x - _selectedFurniture.GetSizeX() / 2;
        //        int centerY = y + _selectedFurniture.GetSizeX() / 2;
        //        //Debug.Log($"x:{x}, y:{y}");
        //        //Debug.Log($"Center x:{centerX}, y:{centerY}");
                
        //        //проверка на границы
        //        if (x < 0 || x > _gridSize.x - _selectedFurniture.GetSizeX()) IsAvailable = false;
        //        if (y < 0 || y > _gridSize.y - _selectedFurniture.GetSizeY()) IsAvailable = false;

        //        if (IsAvailable && IsPlaceTaken(x, y, _selectedFurniture)) IsAvailable = false;

        //        _selectedFurniture.transform.position = new Vector3(centerX, 0, y);
        //        _selectedFurniture.SetTransperent();
        //        if (IsAvailable && Input.GetMouseButtonDown(0)) 
        //        {
        //            OnPlace(centerX, y);
        //        }
        //    }
        //}
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _gridSize.x; x++)
        {
            for (int z = 0; z < _gridSize.y; z++)
            {
                Gizmos.color = new Color(0,1,1,0.6f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1, 0.1f, 1));
            }
        }
    }

    //выбрать объект
    public void SelectFurniture(Furniture furniturePrefab)
    {
        if(_selectedFurniture != null)
        {
            Destroy(_selectedFurniture.gameObject);
        }
        _selectedFurniture = Instantiate(furniturePrefab);
        _selectedFurniturePosition = furniturePrefab.gameObject.transform.position;
        _selectedFurniture.gameObject.transform.SetParent(_parentForFurniture.transform);
    }

    //установить объект
    public void OnPlace(int positionX, int positionY)
    {
        for(int x = 0; x <_selectedFurniture.GetSizeX(); x++)
        {
            for(int y = 0; y < _selectedFurniture.GetSizeY(); y++)
            {
                _grid[positionX + x, positionY + y] = _selectedFurniture;
            }
        }
        CheckCorrectPosition(_selectedFurniture);
        _selectedFurniture.SetNormalColor();
        _selectedFurniture = null;
    }

    //занята ли позиция
    public bool IsPlaceTaken(int positionX, int positionY, Furniture furniture)
    {
        for (int x = 0; x < furniture.GetSizeX(); x++)
        {
            for (int y = 0; y < furniture.GetSizeY(); y++)
            {
                if (_grid[positionX + x, positionY + y] != null) return true;   //true - занято
            }
        }
        return false;   //false - не занято
    }

    //проверка правильной позиции
    public void CheckCorrectPosition(Furniture getFurniture)
    {
        //округлить до целых вниз Round.Cell();
        if(RoundToInt(getFurniture.transform.position) == getFurniture.GetComponent<Furniture>().GetCorrectPosition())
        {
            Debug.Log("Верная позиция!");
        }
        else
        {
            _isCorrectPlacment.CountOfWrongObjects++;
            Debug.Log("Неправильная позиция");
        }
    }

    //округление, для проверки на правильность
    private Vector3Int RoundToInt(Vector3 vector)
    {
        return new Vector3Int (Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), Mathf.RoundToInt(vector.z));
    }
}
