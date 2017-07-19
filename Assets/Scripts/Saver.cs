using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saver : MonoBehaviour
{

    public event Action<int> OnScoreChanged;

    private int m_Score = 0;

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
        get { return PlayerPrefs.GetInt("Best", 0); }
        set { PlayerPrefs.SetInt("Best", value);}
    }

    void Awake()
    {
        this.OnScoreChanged += Saver_OnScoreChanged;
    }

     void Saver_OnScoreChanged(int score)
    {
        if (score > Best)
        {
            Best = score;
        }
    }
}
