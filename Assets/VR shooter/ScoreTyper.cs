using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTyper : MonoBehaviour
{
    private char[] playerName;
    public TextMeshProUGUI playerNameTex;
    public GameObject keyboard;
    public GameObject enterScoreTitle;
    public GameObject resultsScoreboard;
    public TextMeshProUGUI[] scoresplayerNames;

    private void Start()
    {
        playerName = new char[6];
        playerName[0] = '#';
        playerName[1] = '#';
        playerName[2] = '#';
        playerName[3] = '#';
        playerName[4] = '#';
        playerName[5] = '#';
    }
    public void TypeLetter(char let)
    {
        if(let == '!' && playerName[0] != '#')
        {
            ScoreKeeper.current.SetName(playerName[0].ToString() + playerName[1].ToString() + playerName[2].ToString() + playerName[3].ToString() + playerName[4].ToString() + playerName[5].ToString());
            keyboard.SetActive(false);
            enterScoreTitle.SetActive(false);
            resultsScoreboard.SetActive(true);
            playerNameTex.text = " ";
            PopulateScoreboard();
            ScoreKeeper.current.SaveScores();
            return;
        }
        playerName[0] = playerName[1];
        playerName[1] = playerName[2];
        playerName[2] = playerName[3];
        playerName[3] = playerName[4];
        playerName[4] = playerName[5];
        playerName[5] = let;
        playerNameTex.text = playerName[0].ToString() + playerName[1].ToString() + playerName[2].ToString() + playerName[3].ToString() + playerName[4].ToString() + playerName[5].ToString();
    }

    private void PopulateScoreboard()
    {
        int[] scores = ScoreKeeper.current.GetScores();
        string[] playerNames = ScoreKeeper.current.GetNames();
        scoresplayerNames[0].text = playerNames[0];
        scoresplayerNames[1].text = scores[0].ToString();
        scoresplayerNames[2].text = playerNames[1];
        scoresplayerNames[3].text = scores[1].ToString();
        scoresplayerNames[4].text = playerNames[2];
        scoresplayerNames[5].text = scores[2].ToString();
        scoresplayerNames[6].text = playerNames[3];
        scoresplayerNames[7].text = scores[3].ToString();
        scoresplayerNames[8].text = playerNames[4];
        scoresplayerNames[9].text = scores[4].ToString();
    }
}
