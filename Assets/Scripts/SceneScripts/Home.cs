using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Home : MonoBehaviour
{
    public AudioSource As;

    public AudioClip Door;
    public void OnTriggerEnter2D(Collider2D other)
    {
        As.PlayOneShot(Door);
        Invoke("LoadScene", .15f);
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }
    
    public void loadscene (string HowToPlay)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(HowToPlay);
    }
}
