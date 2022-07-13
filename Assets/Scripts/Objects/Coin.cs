using UnityEngine;

public class Coin : MonoBehaviour
{
    CoinManager coinManager;

    void Start()
    {
        coinManager = CoinManager.Instance;

        coinManager.totalCoins++;   
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerMovement player = other.GetComponent<PlayerMovement>();
        
        if (player != null)
        {
            coinManager.coinsCollected++;
            coinManager.totalCoins--;
            GameManager.Instance.EnableGameOver();
            Destroy(gameObject);
        }        
    }
}
