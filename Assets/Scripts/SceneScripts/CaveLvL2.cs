using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CaveLvL2 : MonoBehaviour
{
    public AudioSource As;

    public AudioClip Door;
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)
    {
        As.PlayOneShot(Door);
        Invoke("LoadScene", .15f);
    }

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CaveLvL2");
    }
}
