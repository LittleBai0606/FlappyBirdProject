using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class Game : MonoBehaviour {

    private static Game m_Instance = null;

    public static Game Instance
    {
        get { return m_Instance; }
    }

    void Awake()
    {
        m_Instance = this;
    }

    public event Action<GameState> onStateChanged;
    #region 变量

    GameState m_State = GameState.Init;
    public GameState GameState
    {
        get{return m_State;}
        private set
        {
            m_State = value;
            if (onStateChanged != null)
                onStateChanged(m_State);
        }
    }
    #endregion

    #region 演员

    public GameUI gameUI= null;
    public Bird bird = null;
    public Background background = null;
    public InputController inputController = null;
    public ObstacleLoop obstacleLoop = null;
    public Saver saver = null;
    #endregion
    // Use this for initialization
    void Start ()
    {
        //监听
        onStateChanged += Game_OnStateChanged;
        inputController.OnTab += InputController_OnTab;
        bird.OnDead += Bird_OnDead;
        bird.OnHit += Bird_OnHit;
        saver.OnScoreChanged += Saver_OnScoreChanged;
        //初始进入Init
        GotoInit();
    }

    public void GotoInit()
    {
        this.GameState = GameState.Init;
    }

    public void GotoReady()
    {
        this.GameState = GameState.Ready;
    }

    public void GotoPlay()
    {
        this.GameState = GameState.Play;
    }

    public void GotoOver()
    {
        this.GameState = GameState.Over;
    }

    private void Game_OnStateChanged(GameState state)
    {
        switch (state)
        {
                case GameState.Init:
                    saver.Score = 0;
                    bird.IsVisible = false;
                    bird.UseGravity = false;
                    inputController.CanTab = false;
                    obstacleLoop.IsMove = true;
                    obstacleLoop.SetPipesVisible(false);
                    obstacleLoop.Restore();
                    background.RandomShow();
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Ready:
                    saver.Score = 0;
                    bird.Reset();
                    inputController.CanTab = true;
                    obstacleLoop.IsMove = true;
                    obstacleLoop.SetPipesVisible(false);
                    background.RandomShow();
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Play:
                    bird.IsVisible = true;
                    bird.UseGravity = true;
                    inputController.CanTab = true;
                    obstacleLoop.IsMove = true;
                    obstacleLoop.IsShowPipes = true;
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Over:
                    bird.IsVisible = false;
                    bird.UseGravity = true;
                    inputController.CanTab = false;
                    obstacleLoop.IsMove = false;
                    obstacleLoop.IsShowPipes = false;
                    gameUI.UpdateUI(state);
                    break;
        }
    }
    void InputController_OnTab()
    {
        //状态切换， Ready ----> Play
        if (this.GameState == GameState.Ready)
        {
            GotoPlay();
        }
        //小鸟跳跃
        bird.Jump();
    }

    private void Bird_OnDead()
    {
        GotoOver();
    }

    private void Bird_OnHit()
    {
        saver.Score++;
    }

    private void Saver_OnScoreChanged(int score)
    {
        gameUI.UpdateScore(score);
    }

}
