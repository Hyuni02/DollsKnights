using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainShot : SkillBase
{
    public float[] dmg_rate0 = { 0.7f, 0.9f, 1.2f, 1.4f, 1.6f, 1.9f, 2.1f, 2.3f, 2.6f, 2.8f };
    public float[] dmg_rate1 = { 0.9f, 1.2f, 1.4f, 1.6f, 1.8f, 2.0f, 2.2f, 2.5f, 2.7f, 2.9f };
    public float[] dmg_rate2 = { 1.1f, 1.4f, 1.6f, 1.8f, 2.0f, 2.2f, 2.4f, 2.7f, 2.9f, 3.1f };
    public float[] dmg_rate3 = { 1.3f, 1.5f, 1.7f, 2.0f, 2.2f, 2.4f, 2.6f, 2.9f, 3.1f, 3.3f };
    public float[] dmg_rate4 = { 1.5f, 1.7f, 1.9f, 2.2f, 2.4f, 2.6f, 2.8f, 3.1f, 3.3f, 3.5f };
    public float Selected_dmg_rate;
    public GameObject target;
    public int stack = 0;
    public float stack_timer;
    public bool skilling = false;

    public override void Update() {
        base.Update();
        if(skill_cool_timer <= 0) {
            stack_timer += Time.deltaTime;
        }
    }

    public override void SkillActive() {
        target = GetComponent<CharacterBase>().Target;
        if (target == null)
            return;

        print("use skill : " + GetSkillName() + "    LV." + skilllevel);
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();
        SoundManager.instance.PlaySound_Voice(GetComponent<SoundContainer>().SkillActive[Random.Range(0,3)]);
        SoundManager.instance.PlaySound_Sfx(GetComponent<SoundContainer>().SkillEffect);

        skilling = true;

        stack = (int)(stack_timer / 3.0f);
        switch (stack) {
            case 0:
                Selected_dmg_rate = dmg_rate0[skilllevel];
                break;
            case 1:
                Selected_dmg_rate = dmg_rate1[skilllevel];
                break;
            case 2:
                Selected_dmg_rate = dmg_rate2[skilllevel];
                break;
            case 3:
                Selected_dmg_rate = dmg_rate3[skilllevel];
                break;
            case 4:
            default:
                Selected_dmg_rate = dmg_rate4[skilllevel];
                break;
        }
    }

    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe.Replace("_dmg_rate0", dmg_rate0[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg_rate1", dmg_rate0[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg_rate2", dmg_rate0[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg_rate3", dmg_rate0[skilllevel].ToString());
        skill_describe = skill_describe.Replace("_dmg_rate4", dmg_rate0[skilllevel].ToString());
    }
}
