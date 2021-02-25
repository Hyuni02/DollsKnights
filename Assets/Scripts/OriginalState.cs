using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OriginalState : MonoBehaviour
{
    public string Character_Name, belong, _class;
    public int level;
    public int rank, hp, damage, accuracy, evasion, rateoffire, armor, speed, armorpen;
    public float _hp, _damage, _accuracy, _evasion, _rateoffire, _armor, critrate;

    public void UpdateState() {
        hp += (int)(_hp * (level - 1));
        damage += (int)(_damage * (level - 1));
        accuracy += (int)(_accuracy * (level - 1));
        evasion += (int)(_evasion * (level - 1));
        rateoffire += (int)(_rateoffire * (level - 1));
        armor += (int)(_armor * (level - 1));
    }
}
