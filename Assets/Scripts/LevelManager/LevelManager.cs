using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    public List<LevelPieceBaseSetup> levelPieceBaseSetups;
    public float timeBetweenPieces = .3f;
    [SerializeField] private int _index;
    private GameObject _currentLevel;
    public List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBaseSetup _currSetup;

    private void Awake() 
    {
        //SpawnNextLevel();
        CreateLevelPieces();
    }
    private void SpawnNextLevel()
    {
        if(_currentLevel != null)
        {
            Destroy(_currentLevel); 
            _index++;

            if(_index >= levels.Count)
            {
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.zero;
    }

    private void ResetLevelIndex()
    {
        _index = 0;
    }
    #region 
    /*private void CreateLevelPieces()
    {
        //StartCoroutine(CreateLevelPiecesCoroutine());
        _spawnedPieces = new List<LevelPieceBase>();
        
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPiecesStart);
        }
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
        }
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPiecesEnd);
        }
    }*/

    private void CreateLevelPieces()
{
    CleanSpawnedPieces();

    if(_currSetup != null)
    {
        _index++;
        if(_index >= levelPieceBaseSetups.Count)
        {
            ResetLevelIndex();
        }
    }

    _currSetup = levelPieceBaseSetups[_index];

    for(int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesStart);
        }
        for(int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
        }
        /*for(int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd);
        }*/
        
        //i only need one ( 1) Final piece.
        bool hasSpawned = false;
        for(int i = 0; i < _currSetup.piecesNumber; i++)
        {
            if (!hasSpawned)
        {
            CreateLevelPiece(_currSetup.levelPiecesEnd);
            hasSpawned = true;
        }
}

}


    private void   CreateLevelPiece(List<LevelPieceBase> list)
    {
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPieces = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0)
        {
            var lastPiece = _spawnedPieces[_spawnedPieces.Count-1];

            spawnedPieces.transform.position = lastPiece.endPiece.position;
        }

        _spawnedPieces.Add(spawnedPieces);
    }

    private void CleanSpawnedPieces()
    {
        for(int i = _spawnedPieces.Count -1; i >= 0;i--)
        {
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }
    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();
        
        for(int i = 0; i < _currSetup.piecesNumber; i++)
        {
            CreateLevelPiece(_currSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }
    #endregion
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SpawnNextLevel();
        }
    }
}
