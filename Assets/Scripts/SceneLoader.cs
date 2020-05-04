using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float gameOverSceneLoadDelay = 2f;

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Core");
    }

    public void LoadGameOver()
    {
        StartCoroutine(LoadGameOverShortDelay());
    }

    private IEnumerator LoadGameOverShortDelay()
    {
        yield return new WaitForSeconds(gameOverSceneLoadDelay);
        SceneManager.LoadScene("Game Over");
    }

    public void LoadStartMenu()
    {
        ResetGameStats();
        SceneManager.LoadScene("Start Game");
    }

    public void QuiteGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        ResetGameStats();
        SceneManager.LoadScene("Core");
    }

    private void ResetGameStats()
    {
        FindObjectOfType<GameSession>().ResetPlayerScore();
    }
}
