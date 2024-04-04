using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingPotato : MonoBehaviour
{
    [SerializeField] private TestScriptableObject testScriptableObject;

    private void Start() {
        Debug.Log(testScriptableObject.myString);
        
    }
}
