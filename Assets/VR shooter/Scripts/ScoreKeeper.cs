﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class ScoreKeeper : MonoBehaviour
{
    private int[] scores;
    private string[] names;
    public static ScoreKeeper current;
    private int score;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // تأكد من عدم وجود أكثر من نسخة واحدة من ScoreKeeper في المشهد
        if (current == null)
        {
            current = this;
            // لا يتم استخدام DontDestroyOnLoad هنا، وبالتالي سيُدمّر الكائن عند الانتقال إلى مشهد آخر
        }
        else
        {
            // إذا كان هناك نسخة أخرى من الكائن، سيتم تدميره
            Destroy(gameObject);
        }

        // تحديث النص عند بداية اللعبة
        score = 0;
        scoreText.text = "Score : " + score;
    }

    private void Start()
    {
        score = 0;
    }

    public void ChangeScore(int amount)
    {
        score += amount;
        scoreText.text = "Score : " + score;
    }

    public void SaveScores()
    {
        string[] nam = GetNames(); //just to be sure it's loaded and shit
        int[] sco = GetScores(); //just to be sure it's loaded and shit
        PlayerPrefs.SetInt("S1", sco[0]);
        PlayerPrefs.SetString("B1", nam[0]);
        PlayerPrefs.SetInt("S2", sco[1]);
        PlayerPrefs.SetString("B2", nam[1]);
        PlayerPrefs.SetInt("S3", sco[2]);
        PlayerPrefs.SetString("B3", nam[2]);
        PlayerPrefs.SetInt("S4", sco[3]);
        PlayerPrefs.SetString("B4", nam[3]);
        PlayerPrefs.SetInt("S5", sco[4]);
        PlayerPrefs.SetString("B5", nam[4]);
    }

    public int[] GetScores()
    {
        if (scores == null)
            LoadData();
        return scores;
    }

    public string[] GetNames()
    {
        if (names == null)
            LoadData();
        return names;
    }

    public void ResetData()
    {
        names = new string[] { "###", "###", "###", "###", "###" };
        scores = new int[] { 0, 0, 0, 0, 0 };
        print("RESETTING DATA");
        SaveScores();
    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey("S1")) //if we set one we set them all
        {
            names = new string[5];
            names[0] = PlayerPrefs.GetString("B1");
            names[1] = PlayerPrefs.GetString("B2");
            names[2] = PlayerPrefs.GetString("B3");
            names[3] = PlayerPrefs.GetString("B4");
            names[4] = PlayerPrefs.GetString("B5");

            scores = new int[5];
            scores[0] = PlayerPrefs.GetInt("S1");
            scores[1] = PlayerPrefs.GetInt("S2");
            scores[2] = PlayerPrefs.GetInt("S3");
            scores[3] = PlayerPrefs.GetInt("S4");
            scores[4] = PlayerPrefs.GetInt("S5");
        }
        else
        {
            names = new string[] { "###", "###", "###", "###", "###" };
            scores = new int[] { 0, 0, 0, 0, 0 };
        }
    }

    public void SetName(string nam)
    {
        for (int i = 0; i < 5; i++)
        {
            if (names[i] == "!" || names[i] == null || names[i] == "")
            {
                names[i] = nam;
                return;
            }
        }
    }

    internal bool CheckHighScore()
    {
        LoadData();
        // Checking for high scores
        if (score > scores[0])
        {
            scores[4] = scores[3];
            scores[3] = scores[2];
            scores[2] = scores[1];
            scores[1] = scores[0];
            scores[0] = score;

            names[4] = names[3];
            names[3] = names[2];
            names[2] = names[1];
            names[1] = names[0];
            names[0] = "!";

            return true;
        }
        else if (score > scores[1])
        {
            scores[4] = scores[3];
            scores[3] = scores[2];
            scores[2] = scores[1];
            scores[1] = score;

            names[4] = names[3];
            names[3] = names[2];
            names[2] = names[1];
            names[1] = "!";

            return true;
        }
        else if (score > scores[2])
        {
            scores[4] = scores[3];
            scores[3] = scores[2];
            scores[2] = score;

            names[4] = names[3];
            names[3] = names[2];
            names[2] = "!";

            return true;
        }
        else if (score > scores[3])
        {
            scores[4] = scores[3];
            scores[3] = score;

            names[4] = names[3];
            names[3] = "!";

            return true;
        }
        else if (score > scores[4])
        {
            scores[4] = score;

            names[4] = "!";

            return true;
        }
        return false;
    }
}

