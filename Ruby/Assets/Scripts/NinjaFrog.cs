using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
//敌人，忍者蛙
public class NinjaFrog : MonoBehaviour
{
    public float waitTime = 2;//奔跑一段时间后休息的时间
    public float runTime = 6;//奔跑时间
    public float speed = 5.0f;//奔跑速度
    public LayerMask layer;//检测图层

    public Transform headPoint;//头部位点
    public Transform rightUp;//判定线上端
    public Transform rightDown;//判定线下端

    private bool _collided;//碰撞判断bool
    private float timer;//计时器
    
    private Rigidbody2D _rigidbody2D;
    private CapsuleCollider2D _capsuleCollider2D;
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        //当计时器时间小于规定奔跑时间，奔跑
        if (timer < runTime)
        {
            _animator.SetBool("ninjaFrog_run",true);
            Vector3 movement = new Vector3(speed, _rigidbody2D.velocity.y);
            transform.position += movement * Time.fixedDeltaTime;//移动
            timer += Time.deltaTime;
            
            //与指定图层的碰撞检测
            _collided = Physics2D.Linecast(rightUp.position, rightDown.position,layer);
            if (_collided)
            {
                Debug.DrawLine(rightUp.position, rightDown.position,Color.red);//红色提示
                transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, 0);//转向
                speed *= -1f;
            }
            else
            {
                Debug.DrawLine(rightUp.position, rightDown.position,Color.green);
            }
        }
        //当计时器大于规定奔跑时间，停止奔跑
        if (timer > runTime)
        {
            _animator.SetBool("ninjaFrog_run",false);
            Invoke("Rerun",waitTime);//等待时间结束继续奔跑
        }
        
    }

    private void Rerun()
    {
        timer = 0;//计时器归零
    }

    private void Roll()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //判断碰撞发生位置和头部位点的相对位置
            float height = other.contacts[0].point.y - headPoint.position.y;
            //在头部碰撞（即玩家踩中怪物头部）
            if (height > 0)
            {
                //弹起玩家
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8.0f, ForceMode2D.Impulse);
                speed = 0f;
                
                //怪物消失
                _animator.SetTrigger("ninjaFrog_die");
                _capsuleCollider2D.enabled = false;
                _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
                Destroy(gameObject,1f);
            }
            //非头部碰撞
            else
            {
                //角色消失
                other.gameObject.GetComponent<Animator>().SetTrigger("die");
                Destroy(other.gameObject,0.4f);
                GameController.Instance.ShowGameOverPanel();
            }
        }
    }
}
