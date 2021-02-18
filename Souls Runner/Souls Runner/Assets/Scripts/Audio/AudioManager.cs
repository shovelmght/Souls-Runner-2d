using LesserKnown.Player;
using LesserKnown.UI;  //maybe some of these namespace using are not necessary
using LesserKnown.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Audio
{
    //MusicSources : 2 x audioSource on Camera
    //PlayerSource : 1 x audioSource / player

    public class AudioManager : MonoBehaviour
    {

        //some variables are public for tests only will change to private later

        [Header(" Scripts Scene")]

        //public CharacterController2D[] players; //for test
        public CharacterController2D player;

        [Header(" Scripts Audio")]
        private static AudioManager _instance = null; //singleton
        public PlayerMovementSounds Snd_playerMvt;
        public MusicSystem musicSystem;
        private Dictionary<string, System.Action> snd_EvtDict = new Dictionary<string, System.Action>();
        
        //quick and dirty play music on - off .... eventually ill make a better system for mute/unmute, etc.
        public bool playMusic = false;


        [Header(" Audio Sources")]
        public AudioSource[] musicSources;

        //public AudioSource[] playerSources; //for tests
        public AudioSource playerSource;

        [Header(" Audio Clip - UI ")]
        public AudioClip Snd_Swap;

        void Start()
        {
            //musicSources = GameObject.Find("Camera").GetComponents<AudioSource>(); //note : why doesnt it work as intended ?
            musicSources = UnityEngine.Camera.main.GetComponents<AudioSource>(); //this works tho

            playerSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController2D>();
            
            Snd_playerMvt = GetComponent<PlayerMovementSounds>();
            musicSystem = GetComponent<MusicSystem>();
            InitDict();


            //for tests ...
            //playerSources = GameObject.FindGameObjectWithTag("Player").GetComponents<AudioSource>(); //somehow this doesnt work
            //players = GameObject.FindGameObjectWithTag("Player").GetComponents<CharacterController2D>(); //somehow this doesnt work
        }

        // Update is called once per frame
        void Update()
        {

            PlaySwap(); //play swap snd. probably should be somewhere else but ftm it works

        }

        void Awake()
        {
            if(_instance == null)
            {
                _instance = this;
            }
            else if(_instance !=this)
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void PlayPlayerSource(AudioClip clip)
        {
            //Debug.Log("Speaker "+numero+"Sound : "+clip );
            playerSource.PlayOneShot(clip);
        }
        public void StopPlayerSource(AudioClip clip)
        {
            playerSource.clip = clip;
            playerSource.Stop();
        }


        //Managing the Animator event calls for all anims
        public void PlaySndEvent(string call)
        {
            //Debug.Log("Audio manager Event call :"+call);
            snd_EvtDict[call]();
        }

        private void InitDict()
        {
            snd_EvtDict ["footsteps"] = Snd_playerMvt.PlayFootsteps;
            snd_EvtDict ["climb"] = Snd_playerMvt.PlayClimb;
            snd_EvtDict ["wallclimb"] = Snd_playerMvt.PlayWallClimb;
            snd_EvtDict ["jump"] = Snd_playerMvt.PlayJump;
            snd_EvtDict ["punch"] = Snd_playerMvt.PlayPunch;
            snd_EvtDict ["hit"] = Snd_playerMvt.PlayHit;
            snd_EvtDict ["fall"] = Snd_playerMvt.PlayFall;
            snd_EvtDict ["pickup"] = Snd_playerMvt.PlayPickUp;
            snd_EvtDict ["throw"] = Snd_playerMvt.PlayThrow;

        }

        //This could be somewhere else but Ftm ill leave it here
        void PlaySwap()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                PlayPlayerSource(Snd_Swap);
            }
         }

        public AudioSource[] GetMusicSources()
        {
            return musicSources;
        }

        public AudioSource GetPlayerSource()
        {
            return playerSource;
        }

        public CharacterController2D GetPlayer()
        {
            return player;
        }
        public static AudioManager Instance
        {
            get { return _instance; } 
        }  

        //FOR TESTS ... 

        // if many players AudioSources :  
        // public void PlayPlayerSource(AudioClip clip,int numero)
        // {
        //     //Debug.Log("Speaker "+numero+"Sound : "+clip );
        //     playerSources[numero].PlayOneShot(clip);
        // }
        // public void StopPlayerSource(AudioClip clip,int numero)
        // {
        //     playerSources[numero].clip = clip;
        //     playerSources[numero].Stop();
        // }

        
        // public AudioSource[] GetPlayerSources() //for tests
        // {
        //     return playerSources;
        // }
    } 

}

// Pause all Audio Sources
//AudioListener.pause = true; -- Get Listener from Camera