using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterBase : MonoBehaviour {
    [HideInInspector]
    public string Name;

    List<Transform> RouteToMove;

    public virtual void wait() {

    }
    public virtual void move() {

    }
    public virtual void attack() {

    }
    public virtual void die() {

    }

}