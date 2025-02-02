using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class Score : MonoBehaviour
{
    public int scoreValue = 0;
    [SerializeField]    TMP_Text scoreText;
    private float timer;
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            IncrementScore();
            timer = 0f; // Réinitialisation du chronomètre
        }

        // Optionnel : mettre à jour l'UI
       if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreValue;
        }
        scoreText.text = scoreValue.ToString();

        /*if(Time.timeScale == 0f )
        {
            scoreValue = 0;
        }*/

    }
    private void IncrementScore()
    {
        scoreValue++;
    }
}
