using UnityEngine;

public class Furniture : MonoBehaviour
{
    [SerializeField] private Renderer _mainRenderer;
    public Renderer MainRenderer 
    { 
        get 
        { 
            return _mainRenderer; 
        } 
        set 
        { 
            _mainRenderer = value; 
        } 
    }

    [SerializeField] private Vector2Int _sizeInGrid = Vector2Int.one;
    [SerializeField] private Vector3Int _correctPosition;

    //на каком повороте камеры можем взаимодействовать с фурнитуром
    [SerializeField] private int _countRotate;
    public int CountRotate
    {
        get
        {
            return _countRotate;
        }
    }


    //для прозрачности 
    [SerializeField] private Material _mainMaterial;
    [SerializeField] private Material _transparentMaterial;

    private void Start()
    {
        _mainRenderer = GetComponentInChildren<MeshRenderer>();    
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < _sizeInGrid.x; x++)
        {
            for (int z = 0; z < _sizeInGrid.y; z++)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, z), new Vector3(1, 0.1f, 1));
            }
        }
    }

    public int GetSizeX()
    {
        return _sizeInGrid.x;
    }
    public int GetSizeY()
    {
        return _sizeInGrid.y;
    }

    public void SetTransperent(bool available)
    {
        if (available)
        {
            _mainRenderer.material = _transparentMaterial;
            _mainRenderer.material.color = new Color(141 / 255f, 236 / 255f, 255 / 255f, 0.6f);
        }
        else
        {
            _mainRenderer.material.color = Color.red;
        }
    }
    public void SetTransperent()
    {
        _mainRenderer.material = _transparentMaterial;
        _mainRenderer.material.color = new Color(141 / 255f, 236 / 255f, 255 / 255f, 0.6f);
    }

    public void SetNormalColor()
    {
        _mainRenderer.material = _mainMaterial;
        _mainRenderer.material.color = Color.white;
    }

    public void SetRedColor()
    {
        _mainRenderer.material.color = Color.red;
    }

    public Vector3Int GetCorrectPosition()
    {
        return _correctPosition;
    }
}
