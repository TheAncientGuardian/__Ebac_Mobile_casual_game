using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneHelper : MonoBehaviour
{
    // Start is called before the first frame update
    public void Load(int i)
    {
        SceneManager.LoadScene(i);
    }

    // Update is called once per frame
    public void Load(string i)
    {
        SceneManager.LoadScene(i);
    }
}
