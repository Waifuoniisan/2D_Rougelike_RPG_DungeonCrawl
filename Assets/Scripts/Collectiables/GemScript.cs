using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GemScript : MonoBehaviour
{
    public int GemValue = 1;

    public TextMeshProUGUI GemTXT;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Collect();
            GameManager.Instance.IncreaseGems();
            Destroy(gameObject);
        }
    }

    private void Collect()
    {
        
        int currentCurrency = PlayerPrefs.GetInt("Currency", 0);
        currentCurrency += GemValue;
        PlayerPrefs.SetInt("Currency", currentCurrency);

        if (GemTXT != null)
        {
            GemTXT.text = ":" + currentCurrency.ToString();
        }
        Destroy(gameObject);
    }
}
