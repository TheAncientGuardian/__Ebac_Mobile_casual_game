using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> level;
    [SerializeField]private int _index;
    private GameObject _currentLevel;
    private void Awake() 
    {
        SpawnNextLevel();
    }
    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel); 
            _index++;

            if(_index >= level.Count)
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(level[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnNextLevel();
        }
    }
}
