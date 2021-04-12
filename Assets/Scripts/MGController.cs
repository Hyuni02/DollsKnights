using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGController : DollController
{
    public bool reloading = false;

    //상태-장전 추가
    public override void SetState() {
        if (fs.hp <= 0)
            state = State.die;

        if (uac.animation.isCompleted) {
            now_animation = null;
            attacking = false;
            if (reloading) {
                reloading = false;
                fs.ammo = GetComponent<OriginalState>().dollstate.ammo;
            }
        }

        if (fs.ammo <= 0) {
            state = State.reload;
        }

        if (attacking || dying || reloading)
            return;

        if (stun) {
            Getstun();
            now_animation = "stun";
            float t = 1 / (fs.rateoffire * 0.02f);
            Timer_attack = t;
            return;
        }

        switch (state) {
            case State.attack:
                if (Timer_attack <= 0 && Target != null && Target.GetComponent<FinalState>().hp > 0) {
                    attacking = true;
                    attack();
                    float t = 1 / (fs.rateoffire * 0.02f);
                    PlayAnimation(state.ToString(), t, 1);
                    Timer_attack = t;
                }
                break;
            case State.die:
                dying = true;
                Invoke("die", 0.7f);
                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.move:
                move();
                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.wait:
                wait();
                PlayAnimation(state.ToString());
                break;
            case State.reload:
                if (!reloading) {
                    reload();
                    PlayAnimation(state.ToString(),1,1);
                }
                break;
            case State.victory:
                PlayAnimation(state.ToString(), 1, 1);
                break;
            case State.victoryloop:
                PlayAnimation(state.ToString());
                break;
        }

    }

    public List<GameObject> List_temp_target = new List<GameObject>();
    public GameObject temp_target;

    //체력이 많은 적 우선 공격
    public override void SearchTarget() {

        if (!gameObject.activeSelf)
            return;

        attackable = false;
        GetInRangeTarget();

        if (List_temp_target.Count > 0) {
            temp_target = List_temp_target[0];
            for (int i = 0; i < List_temp_target.Count; i++) {
                if (temp_target.GetComponent<FinalState>().hp <= List_temp_target[i].GetComponent<FinalState>().hp) {
                    temp_target = List_temp_target[i];
                }
            }
            SetTarget(temp_target);
            attackable = true;
        }
        else {
            SetTarget();
            attackable = false;
        }
    }

    public virtual void GetInRangeTarget() {
        for (int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
            if (GetDistance(InGameManager.instance.Spawned_Enemies[i]) <= fs.range
                && InGameManager.instance.Spawned_Enemies[i].activeSelf) {
                List_temp_target.Add(InGameManager.instance.Spawned_Enemies[i]);
            }
        }
    }

    void reload() {
        reloading = true;
    }

    public override void attack() {
        base.attack();
        fs.ammo--;
    }
}
