using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldCoinScript : MonoBehaviour
{
    public int GoldValue = 1;

    public TextMeshProUGUI GoldTXT;
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
            GameManager.Instance.IncreaseCoins();
            Destroy(gameObject);
        }
    }

    private void Collect()
    {
        
        int currentCurrency = PlayerPrefs.GetInt("Currency", 0);
        currentCurrency += GoldValue;
        PlayerPrefs.SetInt("Currency", currentCurrency);

        if (GoldTXT != null)
        {
            GoldTXT.text = ":" + currentCurrency.ToString();
        }
        Destroy(gameObject);
    }
}
