using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    public Transform player;

    public LayerMask EnemiesLayer;

    public GameObject BuyMenu;
    // Start is called before the first frame update
    void Start()
    {
        BuyMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        LoopTalking();
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < 0.5f)
        {
            Interact();
            
        }
        else
        {
            return;
        }
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            BuyWindow();
        }
        
    }

    public void BuyWindow()
    {
        if (BuyMenu.active == true)
        {
            BuyMenu.SetActive(false);
        }
        else
        {
            BuyMenu.SetActive(true);
        }

    }

    public void LoopTalking()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        if (distanceToPlayer < 5f)
        {
            //Debug.Log("You did it");
            
        }
    }
}
