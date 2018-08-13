using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeleteAfterReading { 
    public class MusicController : MonoBehaviour {

        public static MusicController instance;

        public AudioSource mainSong;
        public AudioSource winSong;
        public AudioSource loseSong;
        public AudioSource solveSong;

        private AudioSource curSound;

        // Use this for initialization
        void Start () {
            instance = this;
        }
	
	    // Update is called once per frame
	    void Update () {
		
	    }

        public void PlayMainTheme()
        {
            Play(mainSong);
        }
        public void PlayWin()
        {
            Play(winSong);
        }
        public void PlayLose()
        {
            Play(loseSong);
        }
        public void PlaySolve()
        {
            Play(solveSong);
        }

        public void Play(AudioSource song)
        {
            if (curSound)
                curSound.Stop();
            curSound = song;
            curSound.Play();
        }

        public void Stop()
        {
            if(curSound)
            {
                curSound.Stop();
                curSound = null;
            }
        }
    }
}