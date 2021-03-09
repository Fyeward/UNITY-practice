using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpVelocity = 1.5f;
    private Rigidbody2D _rigidbody2D;
    private bool jumpRequest = false;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Jump"))
        {
            jumpRequest = true;
        }
    }

    private void FixedUpdate()
    {
        if (jumpRequest)
        {
            _rigidbody2D.AddForce(Vector2.up * jumpVelocity, ForceMode2D.Impulse);
            jumpRequest = false;
        }
        //throw new NotImplementedException();
    }
}
