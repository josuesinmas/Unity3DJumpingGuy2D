using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiController : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocity=2f;
    private Rigidbody2D rb2d;
    void Start()
    {
        rb2d=GetComponent<Rigidbody2D>();
        rb2d.velocity=Vector2.left*velocity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag=="destroyer"){
        Destroy(gameObject);
        }
    }
}
