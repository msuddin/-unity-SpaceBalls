using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerScore;

    void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        playerScore = 0;
    }

    public void SetPlayerScoreToIncreaseBy(int amount)
    {
        this.playerScore += amount;
    }

    public int GetPlayerScore()
    {
        return this.playerScore;
    }

    public void ResetPlayerScore()
    {
        this.playerScore = 0;
    }
}
