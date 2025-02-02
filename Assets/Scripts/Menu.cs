using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] Button playBtn;
    void Start()
    {
        playBtn.onClick.AddListener(TaskOnClick);
        playBtn.onClick.AddListener(Quit);
    }

    void TaskOnClick()
    {
        SceneManager.LoadScene("Jeu_Complet");
    }
    void Quit()
    {
        Application.Quit();

    }
}
