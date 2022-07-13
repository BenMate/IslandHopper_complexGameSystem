using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public GameObject gameOverScreen;
    public DungeonGenerator.DungeonGenerator dg;
    public TextMeshProUGUI collectedText;
    public TextMeshProUGUI totalText;
    public TextMeshProUGUI goalText;

    public float deathHeight = -5;

    [HideInInspector]
    public int totalRoomCount;
 
    CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;

        totalRoomCount = dg.GetRoomCount();
        gameOverScreen.SetActive(false);
    }

    void Update()
    {
        collectedText.text = "Coins Collected: " + coinManager.coinsCollected.ToString();
        totalText.text = "TotalCoins: " + coinManager.totalCoins.ToString();
        goalText.text = "TotalToWin: " + coinManager.GetCoinsToWin().ToString();
    }

    public void EnableGameOver()
    {
        if (coinManager.EnoughCoinsCollected())
        {
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }                
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game: Exited");
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Game");
    }


}
