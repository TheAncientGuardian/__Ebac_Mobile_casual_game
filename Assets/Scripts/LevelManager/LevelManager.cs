using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;
    public List<GameObject> levels;

    [Header("Pieces")]
    public List<LevelPieceBase> levelPiecesStart;
    public List<LevelPieceBase> levelPieces;
    public List<LevelPieceBase> levelPiecesEnd;
    public int piecesStartNumber = 3;
    public int piecesNumber = 5;
    public int piecesEndNumber = 1;
    public float timeBetweenPieces = .3f;
    [SerializeField] private int _index;
    private GameObject _currentLevel;
    public List<LevelPieceBase> _spawnedPieces;
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
    _spawnedPieces = new List<LevelPieceBase>();

    // Adding the start piece - only one piece is added, so no loop is needed
    CreateLevelPiece(levelPiecesStart);

    // Creating middle pieces
    for (int i = 0; i < piecesNumber; i++)
    {
        CreateLevelPiece(levelPieces);
    }

    // Adding the end piece - only one piece is added, so no loop is needed
    CreateLevelPiece(levelPiecesEnd);
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

    IEnumerator CreateLevelPiecesCoroutine()
    {
        _spawnedPieces = new List<LevelPieceBase>();
        
        for(int i = 0; i < piecesNumber; i++)
        {
            CreateLevelPiece(levelPieces);
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
