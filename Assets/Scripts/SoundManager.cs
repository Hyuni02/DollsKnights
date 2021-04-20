﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [HideInInspector]
    public AudioSource audioSource_bgm, audioSource_sfx, audioSource_voice;

    [Header("BGM")]
    public bool Bmute = false;
    public AudioClip main;
    public AudioClip factory;
    [Header("SFX")]
    public bool Smute = false;
    public AudioClip click;
    public AudioClip shutter;
    public AudioClip place;
    public AudioClip _return;
    public AudioClip alert;
    public AudioClip lifeloss;
    [Header("VOICE")]
    public bool Vmute = false;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        audioSource_bgm = GameObject.Find("BgmPlayer").GetComponent<AudioSource>();
        audioSource_sfx = GameObject.Find("SFXPlayer").GetComponent<AudioSource>();
        audioSource_voice = GameObject.Find("VoicePlayer").GetComponent<AudioSource>();

        PlaySound_Bgm(main);
    }

    public void PlaySound_Bgm(AudioClip audioClip) {
        //있던거 멈추고
        audioSource_bgm.Stop();
        //재생
        audioSource_bgm.clip = audioClip;
        audioSource_bgm.Play();
    }

    public void PlaySound_Sfx(AudioClip audioClip) {
        audioSource_sfx.PlayOneShot(audioClip);
    }

    public void PlaySound_Voice(AudioClip audioClip) {
        //있던거 멈추고
        audioSource_voice.Stop();
        //재생
        audioSource_voice.clip = audioClip;
        audioSource_voice.Play();
    }
}
