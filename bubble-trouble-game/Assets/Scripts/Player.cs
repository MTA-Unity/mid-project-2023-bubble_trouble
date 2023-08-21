using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movement;
    [SerializeField] private float speed = 4;
    [SerializeField] private Rigidbody2D rb;


    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
        
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + new Vector2(movement*speed*Time.deltaTime, 0));
    }

}
