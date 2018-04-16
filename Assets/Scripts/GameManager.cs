using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    public GameObject continueBtn;
    public GameObject quitBtn;

    private Text scoreText;
    private Text besetScoreText;
    private int score = 0;
    private int bestScore = 0;




    void Awake()
    {
        _Instance = this;
    }

	// Use this for initialization
	void Start ()
	{
	    scoreText = GameObject.Find("Score").GetComponent<Text>();
	    besetScoreText = GameObject.Find("BestScore").GetComponent<Text>();
	    bestScore = PlayerPrefs.GetInt("best", 0);
        besetScoreText.text = "最高记录: " + bestScore.ToString()+ " 分";
    }
	
	// Update is called once per frame
	void Update ()
	{
	    scoreText.text = "当前得分: " + score.ToString() + " 分";

        if (Input.GetKey(KeyCode.Escape))
	    {
            Pause();
	    }
	}


    /// <summary>
    /// 分数增加
    /// </summary>
    /// <returns></returns>
    public int AddScore()
    {
        score++;
        return score;
    }

    /// <summary>
    /// 最高得分
    /// </summary>
    /// <returns></returns>
    public int AddbestScore()
    {
        if (bestScore < score) bestScore = score;
        return bestScore;
    }


    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        PlayerPrefs.SetInt("best",AddbestScore());
    }

    /// <summary>
    /// 暂停
    /// </summary>
    void Pause()
    {
        continueBtn.SetActive(true);
        quitBtn.SetActive(true);
        Time.timeScale = 0;
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    public void ContinueBtn()
    {
        continueBtn.SetActive(false);
        quitBtn.SetActive(false);
        Time.timeScale = 1;
    }

    /// <summary>
    /// 退出（重新开始）
    /// </summary>
    public void Quit()
    {
        GameOver();
        Application.LoadLevel("Start");
        Time.timeScale = 1;
    }

    public void Again()
    {
        GameOver();
        Application.LoadLevel("Main");
    }
}
