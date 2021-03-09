using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
//弹射蹦床
public class Trampoline : MonoBehaviour
{
    public float jumpForceY = 22;//纵向力
    public float jumpForceX = 15;//横向力
    
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _animator.SetTrigger("jumped");
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpForceX,jumpForceY),ForceMode2D.Impulse);
        }
    }
}
