using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int numCoins;
    public int numGems;

    private void Start()
    {
        Instance = GetComponent<GameManager>();
    }

    private void Update()
    {
        //Debug.Log(Instance.numCoins);
        //Debug.Log(Instance.numGems);
    }

    public void IncreaseCoins()
    {
        Instance.numCoins++;
    }

    public void IncreaseGems()
    {
        Instance.numGems++;
    }

    public void SaveToDisk()
    {
        // have the code to save your info to disk
    }

    public void LoadFromDisk()
    {
        // have the code to load info from disk
    }

}
