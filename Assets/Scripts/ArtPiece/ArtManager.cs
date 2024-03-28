using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtManager : MonoBehaviour
{
    public enum ArtType
    {
        TYPE_010,
        TYPE_02,
        TYPE_03
    }
    public List<ArtSetup> artSetups;
    
}

[System.Serializable]
public class ArtSetup
{
    public ArtManager.ArtType artType;
    public GameObject gameObject;
}
