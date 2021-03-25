using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FinalState))]
public class OriginalState : MonoBehaviour {
    public int level;
    //public int rank, hp, damage, accuracy, evasion, rateoffire, armor, speed, armorpen;
    //public float _hp, _damage, _accuracy, _evasion, _rateoffire, _armor, critrate;
    public DollState dollstate;
    public EnemyState enemystate;

    //temp
    public void SetState() {
        FinalState fs = GetComponent<FinalState>();
        if (GetComponent<DollController>() != null) {
            fs.accuracy = dollstate.accuracy;
            fs.armor = dollstate.armor;
            fs.armorpen = dollstate.armorpen;
            fs.critrate = dollstate.critrate;
            fs.damage = dollstate.damage;
            fs.evasion = dollstate.evasion;
            fs.hp = dollstate.hp;
            fs.rateoffire = dollstate.rateoffire;
            fs.speed = dollstate.speed;
            fs.range = dollstate.range;
            fs.block = dollstate.block;
        }
        else {
            fs.accuracy = enemystate.accuracy;
            fs.armor = enemystate.armor;
            fs.armorpen = enemystate.armorpen;
            fs.damage = enemystate.damage;
            fs.evasion = enemystate.evasion;
            fs.hp = enemystate.hp;
            fs.rateoffire = enemystate.rateoffire;
            fs.speed = enemystate.speed;
            fs.range = enemystate.range;
        }

        BuffContainer bf = GetComponent<BuffContainer>();
        if (bf.BuffList.Count > 0) {
            for(int i = 0; i < bf.BuffList.Count; i++) {
                Buff buff = bf.BuffList[i].GetComponent<Buff>();
                fs.accuracy = Mathf.RoundToInt((1 + buff.accuracy * 0.01f) * fs.accuracy);
                fs.armor = Mathf.RoundToInt((1 + buff.armor * 0.01f) * fs.armor);
                fs.damage = Mathf.RoundToInt((1 + buff.dmg * 0.01f) * fs.damage);
                fs.evasion = Mathf.RoundToInt((1 + buff.evasion * 0.01f) * fs.evasion);
                fs.speed = Mathf.RoundToInt((1 + buff.speed * 0.01f) * fs.speed);
                fs.rateoffire = Mathf.RoundToInt((1 - buff.rateoffire * 0.01f) * fs.rateoffire);
                fs.critrate = Mathf.RoundToInt((1 + buff.critrate * 0.01f) * fs.critrate);
            }
        }
    }

}