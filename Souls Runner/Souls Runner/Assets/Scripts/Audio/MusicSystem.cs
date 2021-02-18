using LesserKnown.Audio;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioClip musicIntro; 
    public AudioClip musicLoop;

    AudioSource[] musicSpeakers;

    void Start()
    {
        musicSpeakers = AudioManager.Instance.GetMusicSources();

        //quick and dirty play music MUTE/UNMUTE ... will do a nicer version later
        if(AudioManager.Instance.playMusic) 
            PlayMusicScene();
        
        //quick and dirty play music volume ... Will do a nicer version later
        foreach (AudioSource s in musicSpeakers)
        {
            s.volume = 0.5f; 
        }
    }

    void Update()
    {
    }

    //Music dynamic system
    //to blend audioCLips, stay in DSP time, for that it must swap between 2 sources
    void PlayScheduledMusic(AudioClip clip, int numero, double startTime)
    {
        //Debug.Log("speaker 1 "); 
        musicSpeakers[numero].clip = clip;
        musicSpeakers[numero].PlayScheduled(startTime);
    }
    void PlayScheduledMusicLoop(AudioClip clip,int numero,double startTime)
    {
        //Debug.Log("speaker 2 "); 
        musicSpeakers[numero].loop = true;
        musicSpeakers[numero].clip = clip;
        musicSpeakers[numero].PlayScheduled(startTime);
    }
    void PlayMusicScene()
    {
        double introDuration = (double)musicIntro.samples/musicIntro.frequency;
        double startTime = AudioSettings.dspTime + 0.2;
 
        PlayScheduledMusic(musicIntro,0,startTime);

        PlayScheduledMusicLoop(musicLoop,1,startTime+introDuration);
    }
}
