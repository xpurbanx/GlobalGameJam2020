using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class GameManager : MonoBehaviour
{
    [SerializeField()]
    public int points = 0;
    [SerializeField()]
    public double time;
    public double gameTime;
    VRTK_HeadsetFade fade;
    TextMesh timeMesh, pointsMesh;

    void Start()
    {
        timeMesh = GameObject.FindGameObjectWithTag("Time").GetComponent<TextMesh>();
        pointsMesh = GameObject.FindGameObjectWithTag("Points").GetComponent<TextMesh>();
        fade = GetComponent<VRTK_HeadsetFade>();
    }

    bool gameEnded;
    bool gameLost; // spotted by watcher

    void Update()
    {
        Timer();
    }

    public void AddPoints(int pts)
    {
        points += pts;
    }

    public void FinishGame()
    {
        fade.Fade(Color.black, 0.5f);
    }

    public void GameLost() // spotted by watcher
    {

    }

    public void Timer()
    {
        gameTime -= Time.deltaTime;

        if (gameTime <= 0)
            FinishGame();
    }


}
