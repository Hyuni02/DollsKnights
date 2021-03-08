using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DragonBones;

public abstract class CharacterBase : MonoBehaviour {
    public enum State { wait, move, attack, die, victory, victoryloop}
    public State state;
    public int level = 0;
    [HideInInspector]
    public string Name, now_animation;
    [HideInInspector]
    public bool attacking = false;
    public bool placed = false;

    public List<UnityEngine.Transform> RouteToMove;

    public void SetRoute(List<UnityEngine.Transform> route) {
        RouteToMove = route;
    }

    UnityArmatureComponent uac;
    FinalState fs;
    public void Start() {
        uac = GetComponentInChildren<UnityArmatureComponent>();
        fs = GetComponent<FinalState>();
    }

    public virtual void Update() {
        if (state.ToString().Equals(now_animation)) {
            SetState();
        }
    }

    public virtual void SetState() {
        switch (state) {
            case State.attack:

                PlayAnimation(state.ToString(), 1 / fs.rateoffire, 1);
                break;
            case State.die:

                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.move:
                move();
                PlayAnimation(state.ToString());
                break;
            case State.wait:

                PlayAnimation(state.ToString());
                break;
            case State.victory:
                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.victoryloop:
                PlayAnimation(state.ToString());
                break;
        }
    }

    void PlayAnimation(string anim, float timescale = 1, int playtime = -1) {
        uac.animation.timeScale = timescale;
        now_animation = anim;
        uac.animation.Play(anim, playtime);
    }

    public virtual void move() {

    }

    public abstract void UpdateState();
}