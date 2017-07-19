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
    #endregion
    // Use this for initialization
    void Start ()
    {
        //监听
        onStateChanged += Game_OnStateChanged;
        inputController.OnTab += InputController_OnTab;
        bird.OnDead += Bird_OnDead;
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
                    bird.IsVisible = false;
                    bird.UseGravity = false;
                    inputController.CanTab = false;
                    obstacleLoop.IsMove = true;
                    obstacleLoop.SetPipesVisible(false);
                    obstacleLoop.Reset();
                    background.RandomShow();
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Ready:
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

    // Update is called once per frame
    void Update () {
		
	}
}
