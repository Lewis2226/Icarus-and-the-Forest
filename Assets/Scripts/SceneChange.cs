using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string[] SceneNames;
    
    public void Level1() //Carga la escena 1 
    {
        SceneManager.LoadScene(SceneNames[0]);
    }

    public void Level2() //Carga la escena 2 
    {
        SceneManager.LoadScene(SceneNames[1]);
    }

    public void Level3() //Carga la escena 3
    {
        SceneManager.LoadScene(SceneNames[2]);
    }

    public void Level4() //Carga la escena 4 
    {
        SceneManager.LoadScene(SceneNames[3]);
    }

    public void Level5() //Carga la escena 5 
    {
        SceneManager.LoadScene(SceneNames[4]);
    }
}
