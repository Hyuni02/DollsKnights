using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundContainer : MonoBehaviour
{
    [Header("Voice")]
    public AudioClip PickUp;
    public AudioClip BattleStart;
    public AudioClip Place;
    public AudioClip[] SkillActive;
    public AudioClip Die;
    public AudioClip Victory;

    [Header("SFX")]
    public AudioClip attack;
    public AudioClip SkillEffect;
}
