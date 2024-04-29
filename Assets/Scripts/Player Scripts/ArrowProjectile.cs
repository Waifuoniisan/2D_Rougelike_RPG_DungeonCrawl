using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ArrowProjectile : MonoBehaviour
{
    public string Enemies = "Enemies";

    public Rigidbody2D ArrowRB;

    public float speed = 3;

    public PolygonCollider2D poly;
    // Start is called before the first frame update
    void Start()
    {
         ArrowRB.velocity = (transform.up * speed);
         Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        
        if (collision2D.gameObject.layer == LayerMask.NameToLayer("Enemies"))
        {
            Destroy(gameObject);
        }

        if (collision2D.gameObject.CompareTag("TileMapCoill"))
        {
            Destroy(poly);
        }
    }
    
    

    
}
