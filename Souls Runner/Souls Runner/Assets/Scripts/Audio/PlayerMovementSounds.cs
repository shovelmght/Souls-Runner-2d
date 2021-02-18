using LesserKnown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.Audio{
        public class PlayerMovementSounds : MonoBehaviour
        {

            //again, some of these public var will go private after Tests

            //PLACEHOLDER SOUNDS FOR THE MOMENT .. I WILL WORK ON BETTER ONES
            //WHEN EVERYTHING IS WORKING FINE :)

            [Header(" AudioClips Footsteps")]
            public AudioClip [] FS_terrain;
            public AudioClip [] FS_platform;
            public AudioClip[] currentFootsteps;

        [   Header(" AudioClips Player Mvt")]
            public AudioClip [] Snd_Jump;
            public AudioClip [] Snd_Climb;
            public AudioClip [] Snd_WallStick;
            public AudioClip [] Snd_Whoosh;
            public AudioClip [] Snd_Hit;
            public AudioClip [] Snd_Fall;
            public AudioClip [] Snd_PickUp;
            public AudioClip [] Snd_Throw;

            private AudioClip currentClip;

            public AudioSource currentPlayerSource;
            private int togglePlayerSource = 0;
            private string playerActive;
        
            void Start()
            {
                currentFootsteps = FS_terrain; //default start                
            }

            // Update is called once per frame
            void Update()
            {
                // if(!AudioManager.Instance.player.is_active) //useless atm
                //     playerActive = "Solver";
                // else
                //     playerActive = "Fighter";

                currentPlayerSource = AudioManager.Instance.GetPlayerSource();


                if(AudioManager.Instance.player.IsGrounded())
                {
                    //Debug.Log("SUR TERRAIN!");
                    currentFootsteps = FS_terrain;
                }

                else if(AudioManager.Instance.player.IsOnPlatform())
                {
                    //Debug.Log("SUR PLATFORM!");
                    currentFootsteps = FS_platform;
                }

                // if(AudioManager.Instance.player.is_fighter) //useless atm
                //     togglePlayerSource = 1; 
                // else
                //     togglePlayerSource = 0;

            }


            public void PlayFootsteps()
            {
                //Debug.Log("FOOTSTEP !");
                currentPlayerSource.pitch = (float) Random.Range(0.8f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(0.8f,1.2f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(currentFootsteps),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(currentFootsteps));
            }
            public void PlayClimb()
            {
                //Debug.Log("CLIMB !");
                currentPlayerSource.pitch = (float) Random.Range(0.8f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(0.8f,1.2f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Climb),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Climb));
            }
             public void PlayPunch()
            {
                //Debug.Log("PUNCH !");                
                currentPlayerSource.pitch = (float) Random.Range(0.8f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(0.8f,1.2f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Whoosh),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Whoosh));
            }
            public void PlayWallClimb()
            {
                //Debug.Log("WALL CLIMB!");
                currentPlayerSource.pitch = (float) Random.Range(0.8f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(0.8f,1.2f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_WallStick),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_WallStick));
            }
            public void PlayJump()
            {
                //Debug.Log("JUMP !");
                currentPlayerSource.pitch = (float) Random.Range(1f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(1f,1.2f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Jump),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Jump));
            }
            public void PlayHit()
            {
                //Debug.Log("HIT !");
                currentPlayerSource.pitch = (float) Random.Range(0.8f,1.2f);
                currentPlayerSource.volume = (float) Random.Range(0.8f,1.2f);
               //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Hit),togglePlayerSource);
               AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Hit));
            }
            public void PlayFall()
            {
                //Debug.Log("FALL !");
                currentPlayerSource.pitch = (float) Random.Range(0.5f,0.7f);
                currentPlayerSource.volume = (float) Random.Range(0.5f,0.7f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Fall),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Fall));
            }
            public void PlayPickUp()
            {
                //Debug.Log("FALL !");
                currentPlayerSource.pitch = (float) Random.Range(0.5f,0.7f);
                currentPlayerSource.volume = (float) Random.Range(0.5f,0.7f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Fall),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_PickUp));
            }
            public void PlayThrow()
            {
                //Debug.Log("FALL !");
                currentPlayerSource.pitch = (float) Random.Range(0.5f,0.7f);
                currentPlayerSource.volume = (float) Random.Range(0.5f,0.7f);
                //AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Fall),togglePlayerSource);
                AudioManager.Instance.PlayPlayerSource(RandomClip(Snd_Throw));
            }

            AudioClip RandomClip(AudioClip[] clipArray)
            {
                return clipArray[Random.Range(0, clipArray.Length-1)];
            }

    }
}
