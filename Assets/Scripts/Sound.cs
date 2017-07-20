using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour {

    //声音集合
    public AudioClip[] clips;


    public void Play(string AudioName)
    {
        AudioClip clip = null;
        foreach (AudioClip c in clips)
        {
            if (c.name == AudioName)
            {
                clip = c;
                break;
            }
        }
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, transform.position);
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
