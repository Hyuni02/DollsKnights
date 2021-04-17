using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA15Controller : DollController
{
    PerfectThrillPark ptp;

    public List<GameObject> List_temp_target = new List<GameObject>();
    public GameObject temp_target;

    public override void Start() {
        base.Start();
        ptp = GetComponent<PerfectThrillPark>();
    }

    public override void SearchTarget() {
        if (ptp.skilling) {

            if (!gameObject.activeSelf)
                return;

            attackable = false;
            GetInRangeTarget();

            if (List_temp_target.Count > 0) {
                temp_target = List_temp_target[0];
                for (int i = 0; i < List_temp_target.Count; i++) {
                    if (temp_target.GetComponent<OriginalState>().enemystate.hp <= List_temp_target[i].GetComponent<OriginalState>().enemystate.hp) {
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
        else
            base.SearchTarget();
    }

    public virtual void GetInRangeTarget() {
        List_temp_target.Clear();
        for (int i = 0; i < InGameManager.instance.Spawned_Enemies.Count; i++) {
            if (GetDistance(InGameManager.instance.Spawned_Enemies[i]) <= fs.range
                && InGameManager.instance.Spawned_Enemies[i].activeSelf) {
                List_temp_target.Add(InGameManager.instance.Spawned_Enemies[i]);
            }
        }
    }

    public override void attack() {
        SetFaceDir(Target.transform.position.x);
        if (ptp.skilling) {
            if (Target != null) {
                GameObject skillattack = Instantiate(InGameManager.instance.projectile);
                skillattack.GetComponent<Projectile>().Caster = gameObject;
                skillattack.GetComponent<Projectile>().ExplosionSetting(ptp._range[ptp.skilllevel], (int)(fs.damage * ptp.c_dmg[ptp.skilllevel]));
                skillattack.GetComponent<Projectile>().LaunchProjectile(ptp.image, GetComponent<DollController>().skillPoint, Target.transform, 0, true, false);
                ptp.skilling = false;
            }
        }
        else {
            Target.GetComponent<CharacterBase>().GetAttacked(fs.damage, fs.accuracy, fs.critrate, fs.armorpen);
        }
    }


}
