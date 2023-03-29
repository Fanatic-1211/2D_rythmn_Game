using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void LoadGameOver(){
        gameObject.SetActive(true);
    }
    public void TryAgainButton(){
        SceneManager.LoadScene("GameScreen");
    }
    public void MainMenuButton(){
        SceneManager.LoadScene("Title");
    }
}
