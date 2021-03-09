using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class FruitItem : MonoBehaviour
{
    public GameObject collectedEffect;//收集效果，利用子对象的动画实现
    public int Score = 100;//单个分数

    private SpriteRenderer _spriteRenderer;//渲染显示图像

    private CircleCollider2D _circleCollider2D;//碰撞判定
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //碰撞后该层消失显示子对象动画
            _spriteRenderer.enabled = false;
            _circleCollider2D.enabled = false;
            collectedEffect.SetActive(true);

            GameController.Instance.totalScore += Score;
            GameController.Instance.UpdateTotalScore();
            Destroy(gameObject, 0.2f);
        }
    }
}
