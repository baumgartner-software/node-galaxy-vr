using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    


    public void LoadPassthroughScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadGridLightScene()
    {
        SceneManager.LoadScene(2);
    }

    public void LoadGridDarkScene()
    {
        SceneManager.LoadScene(3);
    }
}
