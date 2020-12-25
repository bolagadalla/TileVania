using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    // Cached Component
    [SerializeField] Text playerLivesText;
    [SerializeField] Text playerCoinText;

    // Variables
    [SerializeField] int playerLives = 3;
    [SerializeField] int playerCoinPickup = 0;

    /// <summary>
    /// This is Singleton
    /// </summary>
    void Awake()
    {
        //Finds how many game sessions are there in the scene
        int gameSessionCount = FindObjectsOfType<GameSession>().Length;

        // If there is more then one gamesession then destroy this gameobject
        if (gameSessionCount>1)
        {
            Destroy(gameObject);
        }
        // Else it would not be destroied
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLivesText.text = playerLives.ToString();
        playerCoinText.text = playerCoinPickup.ToString();
    }

    #region Methods
    public void ProcessPlayerDeath()
    {
        if (playerLives>1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void AddToCoins(int addToCoins)
    {
        playerCoinPickup += addToCoins;
        playerCoinText.text = playerCoinPickup.ToString();
    }

    private void TakeLife()
    {
        // decraments the player lives by one and reloads the scene
        playerLives--;
        playerLivesText.text = playerLives.ToString();

        //var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        //SceneManager.LoadScene(currentSceneIndex);
    }


    private void ResetGameSession()
    {
        // Reloads the first scene and then destroies this game object so the singleton resets
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }



    #endregion
}
