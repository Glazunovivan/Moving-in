using UnityEngine;
using UnityEngine.UI;

public class CorrectPlacement : MonoBehaviour
{
    public bool IsCorrectPlacement = true;
    private int _countOfWrongObjects;
    public int CountOfWrongObjects
    {
        get
        {
            return _countOfWrongObjects;
        }
        set
        {
            _countOfWrongObjects = value;
        }
    }
    [SerializeField] private Button _buttonNextLevel;
    [SerializeField] private Button _buttonTryAgain;

    private void Start()
    {
        _buttonNextLevel.gameObject.SetActive(false);
        _buttonTryAgain.gameObject.SetActive(false);
        _countOfWrongObjects = 0;
    }

    public void CorrectPlacmentInLevel()
    {
        //if (IsCorrectPlacement == true)
        //{
        //    _buttonNextLevel.gameObject.SetActive(true);
        //    Debug.Log("Уровень пройден!");
        //}
        //else
        //{
        //    _buttonNextLevel.gameObject.SetActive(false);
        //    Debug.Log("Уровень не пройден...");
        //}
        if (_countOfWrongObjects == 0)
        {
            _buttonNextLevel.gameObject.SetActive(true);
            Debug.Log("Уровень пройден!");
        }
        else
        {
            _buttonNextLevel.gameObject.SetActive(false);
            _buttonTryAgain.gameObject.SetActive(true);
            Debug.Log("Уровень не пройден...");
        }
    }
}
