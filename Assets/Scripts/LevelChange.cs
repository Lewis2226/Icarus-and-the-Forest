using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public string LevelName;
    public GameObject MenuWin;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MenuWin.SetActive(true);
            collision.gameObject.SetActive(false);
        } 
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(LevelName);
    } 

    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
