using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FinalState))]
public class OriginalState : MonoBehaviour
{
    public int level;
    //public int rank, hp, damage, accuracy, evasion, rateoffire, armor, speed, armorpen;
    //public float _hp, _damage, _accuracy, _evasion, _rateoffire, _armor, critrate;
    public DollState dollstate;

    //temp
    public void SetState() {
        FinalState fs = GetComponent<FinalState>();
        fs.accuracy = dollstate.accuracy + (int)(dollstate._accuracy * (level - 1));
        fs.armor = dollstate.armor + (int)(dollstate._armor * (level - 1));
        fs.armorpen = dollstate.armorpen;
        fs.critrate = dollstate.critrate;
        fs.damage = dollstate.damage + (int)(dollstate._damage * (level - 1));
        fs.evasion = dollstate.evasion + (int)(dollstate._evasion * (level - 1));
        fs.hp = dollstate.hp + (int)(dollstate._hp * (level - 1));
        fs.rateoffire = dollstate.rateoffire + (int)(dollstate._rateoffire * (level - 1));
        fs.speed = dollstate.speed;
    }

    public void UpdateState() {
        //hp += (int)(_hp * (level - 1));
        //damage += (int)(_damage * (level - 1));
        //accuracy += (int)(_accuracy * (level - 1));
        //evasion += (int)(_evasion * (level - 1));
        //rateoffire += (int)(_rateoffire * (level - 1));
        //armor += (int)(_armor * (level - 1));
    }
}
