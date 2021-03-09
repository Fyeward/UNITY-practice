using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBox : MonoBehaviour
{
    [Range(0, 10)] public float jumpVelocity = 5f;//起跳速度
    public LayerMask mask;//图层判定
    public float boxHeight;//判定盒子的高度

    private Vector2 playerSize;//角色大小
    private Vector2 boxSize;//判定盒子大小

    private bool jumpRequest = false;
    private bool grounded = false;
    
    private Rigidbody2D _rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        playerSize = GetComponent<SpriteRenderer>().bounds.size;
        boxSize = new Vector2(playerSize.x * 0.3f, boxHeight);//初始化判定盒子大小
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && grounded)
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
            //grounded = false;
        }
        else
        {
            //设置判定盒子位置
            Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);
            
            if (Physics2D.OverlapBox(boxCenter, boxSize, 0, mask) != null)
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }
        }
    }
    //判定盒子显示
    // private void OnDrawGizmos()
    // {
    //     if (grounded)
    //     {
    //         Gizmos.color = Color.red;
    //     }
    //     else
    //     {
    //         Gizmos.color = Color.green;
    //     }
    //     Vector2 boxCenter = (Vector2) transform.position + (Vector2.down * playerSize.y * 0.5f);
    //     Gizmos.DrawWireCube(boxCenter,boxSize);
    // }
}
