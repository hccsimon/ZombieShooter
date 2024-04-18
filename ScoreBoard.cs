using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text Score;
    public CanvasGroup board;
    float timer;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Score.text = "Kill : " + Zombie.TotalKill;
        Cursor.visible = true;
    }
    void Update()
    {
        timer += Time.deltaTime;
        board.alpha = timer/3;

        
    }

    public void Restart()
    {
        Application.LoadLevel(0);
    }
}
