using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterBase
{
    public enum Type { melee, range}
    public Type type;
    public bool blocked = false;
    void Start()
    {
        
    }

    void Update()
    {
        UpdateState();
    }

    public override void UpdateState() {
        if(type == Type.range) {
            if (blocked) {
                state = State.attack;
            }
            else {
                state = State.move;
            }
        }
        else {

        }
    }
}
