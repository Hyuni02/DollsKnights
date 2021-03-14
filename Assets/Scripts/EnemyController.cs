using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuffContainer))]
public class EnemyController : CharacterBase {
    public enum Type { melee, range }
    public Type type;
    public bool blocked = false;
    public GameObject Blocker;

    //public override void Start() {

    //}

    //public override void Update() {
        
    //}

    public override void UpdateState() {
        switch (type) {
            case Type.melee:
                if (blocked) {
                    state = State.attack;
                }
                else {
                    state = State.move;
                }
                break;

            case Type.range:

                break;
        }

        CheckBlocked();
    }

    void CheckBlocked() {
        if (Blocker == null)
            blocked = false;
        else {
            if (Blocker.activeSelf == true)
                blocked = true;
            else
                Blocker = null;
        }
    }
}
