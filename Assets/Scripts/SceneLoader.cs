using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float gameOverSceneLoadDelay = 2f;

    public void LoadGameScene()
    {
        EditorSceneManager.LoadScene("Core");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverShortDelay());
        
    }

    private IEnumerator LoadGameOverShortDelay()
    {
        yield return new WaitForSeconds(gameOverSceneLoadDelay);
        EditorSceneManager.LoadScene("Game Over");
    }

    public void LoadStartMenu()
    {
        FindObjectOfType<GameSession>().ResetPlayerScore();
        EditorSceneManager.LoadScene("Start Game");
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
