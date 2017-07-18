﻿using System;
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
    #endregion
    // Use this for initialization
    void Start ()
    {
        //监听
        onStateChanged += Game_OnStateChanged;

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
                    background.RandomShow();
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Ready:
                    bird.IsVisible = true;
                    background.RandomShow();
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Play:
                    bird.IsVisible = true;
                    gameUI.UpdateUI(state);
                    break;
                case GameState.Over:
                    bird.IsVisible = false;
                    gameUI.UpdateUI(state);
                    break;
        }
    }

    // Update is called once per frame
	void Update () {
		
	}
}