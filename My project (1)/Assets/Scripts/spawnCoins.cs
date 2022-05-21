using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnCoins : MonoBehaviour
{
    public GameObject coin;
    public GameObject gem;

    int[,] arr = { 
        { 0, 1, 1, 4, 3 }, 
        { 1, 5, 1, 1, 4 }, 
        { 1, 1, 1, 1, 1 }, 
        { 1, 1, 1, 1, 5 }, 
        { 1, 2, 0, 4, 1 } 
    };


    //z-82 x-85
    // Start is called before the first frame update
    private int xPositionCoin = 18;
    private int selectCoin;
    public void generateCoinsAndGem()
    {
        for (int i = 0; i <= arr.GetLength(0) - 1; i++)
        {
            int zPositionCoin = 15;
            for (int j = 0; j <= arr.GetLength(1) - 1; j++)
            {
                switch (arr[i, j])
                {
                    case 1:
                        {
                            Instantiate(coin, new Vector3(xPositionCoin, 3, zPositionCoin), Quaternion.identity);
                            break;
                        }
                    case 5:
                        {
                            Instantiate(gem, new Vector3(xPositionCoin, 3, zPositionCoin), Quaternion.identity);
                            break;
                        }

                }
                zPositionCoin = zPositionCoin + 18;
            }
            xPositionCoin = xPositionCoin + 18;
        }
    }
}
