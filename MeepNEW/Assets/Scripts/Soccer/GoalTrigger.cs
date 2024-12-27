using UnityEngine;
using TMPro;

public class GoalTrigger : MonoBehaviour
{
    public string goalTag; // Tag for the goal
    public int scoreToReset = 5; // Score threshold to trigger the reset
    public TextMeshPro redScoreText; // Text for displaying red team score
    public TextMeshPro blueScoreText; // Text for displaying blue team score

    private static int redTeamScore = 0; // Score of the red team
    private static int blueTeamScore = 0; // Score of the blue team

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BeachBall") && gameObject.CompareTag(goalTag)) // Check if the collider is tagged as "BeachBall" and it's a goal
        {
            if (goalTag == "RedGoal")
                redTeamScore++; // Increment the red team's score
            else if (goalTag == "BlueGoal")
                blueTeamScore++; // Increment the blue team's score

            UpdateScoreTexts();
            CheckScore();
        }
    }

    private void UpdateScoreTexts()
    {
        redScoreText.text = redTeamScore.ToString(); // Update the red team's score text
        blueScoreText.text = blueTeamScore.ToString(); // Update the blue team's score text
    }

    private void CheckScore()
    {
        if (redTeamScore >= scoreToReset || blueTeamScore >= scoreToReset)
        {
            ResetScores();
        }
    }

    private void ResetScores()
    {
        redTeamScore = 0; // Reset the red team's score to 0
        blueTeamScore = 0; // Reset the blue team's score to 0
        UpdateScoreTexts(); // Update score texts after resetting scores
    }
}
