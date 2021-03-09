using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//中控，控制游戏本身的脚本
public class GameController : MonoBehaviour
{
    public int totalScore;//总分数
    public Text scoreText;//UI中的分数text
    public GameObject gameOverPanel;//与游戏结束的UI绑定

    public static GameController Instance;//本游戏控制器
    // Start is called before the first frame update
    void Start()
    {
        //初始化自己，便于其他脚本调用
        Instance = this;
    }

    // Update is called once per frame
    //更新得分/
    public void UpdateTotalScore()
    {
        this.scoreText.text = totalScore.ToString();
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.GetComponentsInChildren<Text>()[2].text = totalScore.ToString();//深度搜索某类型子对象
        gameOverPanel.SetActive(true);
    }
    
    //重载游戏场景
    public void RestartLevel(string leverName)
    {
        SceneManager.LoadScene(leverName);
    }
}
