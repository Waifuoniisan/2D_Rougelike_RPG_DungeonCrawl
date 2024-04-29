using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveLvL1 : MonoBehaviour
{
    public AudioSource As;

    public AudioClip Door;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        As.PlayOneShot(Door);
        if (other.CompareTag("Player"))
        {
            LoadScene();  
        }
        
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CaveLvL1");
    }
}
