using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

 
    public void OnTriggerEnter2D(Collider2D collision)
    { 
        ReloadCurrentScene();
    }
    void ReloadCurrentScene()
    {
       
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
    
        SceneManager.LoadScene(sceneIndex);
    }
}
