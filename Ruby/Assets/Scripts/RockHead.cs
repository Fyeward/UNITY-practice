using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    public Transform rockHeadPoint;//判定碰撞位置
    
    private Rigidbody2D _rigidbody2D;

    private Animator _animator;

    private float _timer;//计时器，控制石头归位
    private const float RockHeadWaitingTimeSpan = 2f;//落下后等待时间
    private const float RockHeadBackTimeSpan = 0.3f;//返回时间
    private bool _isGroundHit = false;//位于地面的判定

    private Vector3 _originalPosition;//初始位置
    private Vector3 _velocity = Vector3.zero;

    private static readonly int IsBottomHit = Animator.StringToHash("isBottomHit");
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _originalPosition = _rigidbody2D.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isGroundHit)
        {
            _timer += Time.deltaTime;
            //超过等待时间且此时不在原位
            if (_timer >= RockHeadWaitingTimeSpan && transform.position != _originalPosition)
            {
                _rigidbody2D.isKinematic = true;
                _rigidbody2D.gravityScale = 1;
                //平滑移动
                transform.position = Vector3.SmoothDamp(transform.position, _originalPosition,
                    ref _velocity, RockHeadBackTimeSpan);
            }
            //在原位
            else if(transform.position == _originalPosition)
            {
                _isGroundHit = false;
                _timer = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        //触发后调整刚体类型，使其运动不受代码控制而设为引擎内的默认值
        if (other.gameObject.CompareTag("Player"))
        {
            _rigidbody2D.isKinematic = false;
            _rigidbody2D.gravityScale = 2f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //碰撞到头部才算是石头击中角色
            float height = other.contacts[0].point.y - rockHeadPoint.position.y;
            if (height < 0)
            {
                other.gameObject.GetComponent<Animator>().SetTrigger("die");
                Destroy(other.gameObject,0.4f);
                GameController.Instance.ShowGameOverPanel();
            }
        }
        if (other.gameObject.layer == 8)
        {
            _isGroundHit = true;
            _animator.SetTrigger(IsBottomHit);
        }
    }
}
