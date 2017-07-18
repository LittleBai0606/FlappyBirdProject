using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
		iTween.MoveBy(
            gameObject, iTween.Hash(
                "y", 20,
            "easeType", iTween.EaseType.linear,
            "loopType", iTween.LoopType.pingPong,
            "delay", .3f,
            "time", 0.5));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
