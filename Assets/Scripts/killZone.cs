using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class killZone : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] TMPro.TextMeshProUGUI m_TextMeshPro;
    [SerializeField] Image m_Image;
    [SerializeField] Button reloadBtn;
    [SerializeField] Button quitBtn;
    [SerializeField] AudioSource gameOver_AudioSource;
    [SerializeField] AudioClip gameOver_AudioClip;
    Score scoreObject = FindObjectOfType<Score>();

    float nulle = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
       m_Image.enabled = true;
        gameOver_AudioSource.PlayOneShot(gameOver_AudioClip, 0.7f);
        reloadBtn.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        quitBtn.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        reloadBtn.onClick.AddListener(TaskOnClick);
        quitBtn.onClick.AddListener(Quit);
    }
    void TaskOnClick()
    {
        if (scoreObject != null)
        {
            int score = scoreObject.scoreValue;
            score = 0;
        }
        SceneManager.LoadScene("Jeu_Complet");
        Time.timeScale = 1;
    }

    void Quit ()
    {
        Application.Quit();
    

    }
}
