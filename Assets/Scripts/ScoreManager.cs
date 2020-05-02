using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = FindObjectOfType<GameSession>().GetPlayerScore().ToString();
    }
}
