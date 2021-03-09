using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallingTime = 1;//站立此时间后落下

    private TargetJoint2D _targetJoint2D;
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _targetJoint2D = GetComponent<TargetJoint2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
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
            Invoke("Falling",fallingTime);
        }

        if (other.gameObject.CompareTag("Spike"))
        {
            Destroy(gameObject);
        }
    }

    private void Falling()
    {
        _animator.SetBool("on",false);
        _targetJoint2D.enabled = false;
        _boxCollider2D.isTrigger = false;
    }
}
