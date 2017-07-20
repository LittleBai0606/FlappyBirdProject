using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public Sprite[] Medals;

    public GameObject Logo;
    public GameObject ButtonStart;
    public GameObject ButtonLadder;
    public GameObject Ready;
    public GameObject Tutorial;
    public GameObject Score;
    public GameObject Over;

    public void UpdateUI(GameState state)
    {
        switch (state)
        {
                case GameState.Init:
                    Logo.SetActive(true);
                    ButtonStart.SetActive(true);
                    ButtonLadder.SetActive(true);
                    Ready.SetActive(false);
                    Tutorial.SetActive(false);
                    Score.SetActive(false);
                    Over.SetActive(false);
                    break;

                case GameState.Ready:
                    Logo.SetActive(false);
                    ButtonStart.SetActive(false);
                    ButtonLadder.SetActive(false);
                    Ready.SetActive(true);
                    Tutorial.SetActive(true);
                    Score.SetActive(false);
                    Over.SetActive(false);
                    break;
                
                case GameState.Play:
                    Logo.SetActive(false);
                    ButtonStart.SetActive(false);
                    ButtonLadder.SetActive(false);
                    Ready.SetActive(false);
                    Tutorial.SetActive(false);
                    Score.SetActive(true);
                    Over.SetActive(false);
                    break;

                case GameState.Over:
                    Logo.SetActive(false);
                    ButtonStart.SetActive(false);
                    ButtonLadder.SetActive(false);
                    Ready.SetActive(false);
                    Tutorial.SetActive(false);
                    Score.SetActive(true);
                    Over.SetActive(true);
                    break;

            default:break;
        }
    }

    public void UpdateScore(int score)
    {
        this.Score.GetComponent<Text>().text = score.ToString();
    }

    public void UpdateResult(int score, int best, bool newBest)
    {
        //显示分数和结果
        this.Over.transform.Find("Panel/Score").GetComponent<Text>().text = score.ToString();
        this.Over.transform.Find("Panel/BestScore").GetComponent<Text>().text = best.ToString();

        //显示是否打破记录
        this.Over.transform.Find("Panel/NewLogo").GetComponent<Image>().enabled = newBest;

        Sprite thisMedal = null;
        if (score < 5)
        {
            thisMedal = Medals[0];
        }
        else if(score < 20)
        {
            thisMedal = Medals[1];
        }else if (score < 40)
        {
            thisMedal = Medals[2];
        }else
        {
            thisMedal = Medals[3];
        }
        this.Over.transform.Find("Panel/Medal").GetComponent<Image>().sprite = thisMedal;

        iTween.MoveFrom(
            Over,
            iTween.Hash(
                "y", -668,
                "easeType", iTween.EaseType.easeOutExpo,
                "loopType", iTween.LoopType.none,
                "time", 0.5f));
    }
}
