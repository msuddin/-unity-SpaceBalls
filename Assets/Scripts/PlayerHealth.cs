using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHealth;

    // Update is called once per frame
    void Update()
    {
        playerHealth.text = FindObjectOfType<Player>().GetHealth().ToString();
    }
}
