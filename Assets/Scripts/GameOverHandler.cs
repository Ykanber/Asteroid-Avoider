using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject gameOverDisplay;
    [SerializeField] private AsteroidSpawner asteroidSpawner;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private ScoreSystem scoreSystem;

    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        int count = scoreSystem.EndTimer();
        gameOverText.text = $"Your Score: {count}";

        gameOverDisplay.gameObject.SetActive(true);
    } 

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

   public void Continue()
    {
        AdManager.instance.ShowAd(this);

        continueButton.interactable = false;

    }
    public void PlayAgain(){
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;

        asteroidSpawner.enabled = true;

        gameOverDisplay.SetActive(false);
    }
}
