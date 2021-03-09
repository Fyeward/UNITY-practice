using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//角色的基本控制脚本
public class Player : MonoBehaviour
{
    public float speed = 5;//移动速度
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;

    private float x;
    private float y;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        x = Input.GetAxisRaw("Horizontal");//检测水平方向键
        y = Input.GetAxisRaw("Vertical");//检测垂直方向键

        if (x > 0)//向右移动
        {
            _rigidbody2D.transform.eulerAngles = new Vector3(0f, 0f, 0f);
            _animator.SetBool("run",true);
        }
        if (x < 0)//向左移动
        {
            _rigidbody2D.transform.eulerAngles = new Vector3(0f, 180f, 0f);//180度转向
            _animator.SetBool("run",true);
        }
        if (x < 0.001f && x > -0.001f)
        {
            _animator.SetBool("run",false);
        }
        
        _animator.SetFloat("y",_rigidbody2D.velocity.y);//利用blendTree实现的跳跃动画的转换（在此基础上取消了垂直向下方向按键的检测以避免）
        
        if (_rigidbody2D.velocity.y < -0.001f || _rigidbody2D.velocity.y > 0.001f)//角色起跳后必然为跳跃动作无论左右移动与否
        {
            _animator.SetBool("run",false);
        }
        Run();
    }

    private void Run()
    {
        Vector3 movement = new Vector3(x, y,0);
        _rigidbody2D.transform.position += movement * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //与地刺发生碰撞
        if (other.gameObject.CompareTag("Spike"))
        {
            _animator.SetTrigger("die");
            Destroy(gameObject,0.4f);
            GameController.Instance.ShowGameOverPanel();
        }
        //与电锯发生碰撞
        if (other.gameObject.CompareTag("Saw"))
        {
            _animator.SetTrigger("die");
            Destroy(gameObject,0.4f);
            GameController.Instance.ShowGameOverPanel();
        }
    }
}
