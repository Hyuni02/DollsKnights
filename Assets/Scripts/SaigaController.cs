using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaigaController : SGController
{
    public int skillcount = 0;
    public bool skilling = false;

    public override void attack() {
        if(!skilling)
            base.attack();

        else {
            if(skillcount == 0) {
                _attack(fs.damage * GetComponent<BigAntelopesHorn>().increase_dmg1[GetComponent<BigAntelopesHorn>().skilllevel]);
                skillcount++;
            }
            else if(skillcount == 1) {
                _attack(fs.damage * GetComponent<BigAntelopesHorn>().increase_dmg1[GetComponent<BigAntelopesHorn>().skilllevel]);
                skillcount++;
            }
            else if(skillcount == 2) {
                _attack(fs.damage * GetComponent<BigAntelopesHorn>().increase_dmg1[GetComponent<BigAntelopesHorn>().skilllevel]);
                skilling = false;
            }
            else {
                Debug.LogError("Saiga12 Skill Error");
            }
        }
    }

    void _attack(float dmg) {
        SetFaceDir(Target.transform.position.x);
        for (int i = 0; i < Blocked_Enemies.Count; i++) {
            Blocked_Enemies[i].GetComponent<CharacterBase>().GetAttacked((int)dmg, fs.accuracy, fs.critrate, fs.armorpen);
        }
        fs.ammo--;
    }

}
