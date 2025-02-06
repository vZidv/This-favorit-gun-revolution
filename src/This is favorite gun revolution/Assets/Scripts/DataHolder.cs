using UnityEngine;
public static class DataHolder 
{
    public static bool sound = true;
    public static int coins = 0;

    public static bool IsBuyskin1 = false;
    public static bool IsBuyskin2 = false;
    public static bool IsBuyskin3 = false;
    public static void CoinPlus()
    {
        coins += 1;
        PlayerPrefs.SetInt("Coin", coins);
    }
}
