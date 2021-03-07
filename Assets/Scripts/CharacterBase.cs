using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour {
    public enum State { wait, move, attack, die, victory, victoryloop}
    public State state;
    public int level = 0;
    [HideInInspector]
    public string Name;
    [HideInInspector]
    public bool attacking = false;
    [HideInInspector]
    public bool placed = false;

    public List<Transform> RouteToMove;

    public void SetRoute(List<Transform> route) {
        RouteToMove = route;
    }

    public virtual void PlayAnimation() {
        switch (state) {
            case State.attack:
                break;
            case State.die:
                break;
            case State.move:

                break;
            case State.victory:
                break;
            case State.victoryloop:
                break;
        }
    }

    public abstract void UpdateState();
}