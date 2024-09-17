using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/************************************************************************
* This class is a component of the Scorekeeper GameObject, an in-world 
* UI Canvas. It is designed to keep track of and display the score for
* the game. Functionalities are listed above each method declaration
************************************************************************/
public class Scorekeeper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreboardText;  // Reference to the UI TextMeshProUGUI component
    private float score = 0f;                                 // Player's current score
    private int penalty = 40;                                 // Penalty for running into an obstacle

    public static Scorekeeper Instance;                       // Singleton instance

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    // Adds to the score based on player input
    public void AddToScore(float inputFromPlayer)
    {
        // Apply exponential function to the input and add to the score
        float scoreToAdd = Mathf.Clamp(Mathf.Exp(inputFromPlayer) - 1, 0f, 0.35f);
        score += scoreToAdd;
        DisplayScore();
    }

    // Subtracts penalty from the score when the player runs into an obstacle
    public void SubtractFromScore()
    {
        Debug.Log("Current Score Before Subtraction: " + score);
        Debug.Log("Penalty: " + penalty);

        score = Mathf.Max(0, score - penalty);

        Debug.Log("New Score After Subtraction: " + score);

        DisplayScore();
    }

    // Displays the score on the UI
    public void DisplayScore()
    {
        scoreboardText.text = "Score: " + Mathf.RoundToInt(score).ToString();
    }
}
