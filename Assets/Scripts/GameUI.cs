using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{

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
}
