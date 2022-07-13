using UnityEngine;

public class CoinManager : Singleton<CoinManager>
{
    public int coinsToGet = 1;
    public int coinsCollected = 0;
    public int totalCoins;

    public bool EnoughCoinsCollected()
    {
        return coinsCollected >= coinsToGet;
    }

    public int GetCoinsToWin()
    {
        return coinsToGet;
    }
}


