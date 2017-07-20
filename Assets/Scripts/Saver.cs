using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{

    public event Action<int> OnScoreChanged;

    private int m_Score = 0;

    private bool m_NewBest = false;

    public int Score
    {
        get { return m_Score; }
        set
        {
            m_Score = value;
            if (OnScoreChanged != null)
                OnScoreChanged(m_Score);
        }
    }

    public int Best
    {
        get { return PlayerPrefs.GetInt("BestScore", 0); }
        private set { PlayerPrefs.SetInt("BestScore", value);}
    }

    public bool NewBest
    {
        get { return m_NewBest; }
    }

    void Awake()
    {
        this.OnScoreChanged += Saver_OnScoreChanged;
    }

    public void Reset()
    {
        m_Score = 0;
        m_NewBest = false;
    }

    public void ClearBest()
    {
        this.Best = 0;
    }
     void Saver_OnScoreChanged(int score)
    {
        //打破纪录
        if (score > Best)
        {
            Best = score;
            m_NewBest = true;
        }
    }
}
