using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//滚动电锯
public class Saw : MonoBehaviour
{
    public float speed = 2;//移动速度

    public float moveTime = 3;//移动时间

    private bool directionRight = true;//方向判断

    private float timer;//计时器
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    //利用规定速度和时间的方式控制移动状态
    void Update()
    {
        if (directionRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        timer += Time.deltaTime;

        if (timer > moveTime)
        {
            directionRight = !directionRight;
            timer = 0;
        }
    }
}
