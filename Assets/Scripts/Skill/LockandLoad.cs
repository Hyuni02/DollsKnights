using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockandLoad : SkillBase
{
    public int[] increase_dmg = { 16, 17, 19, 20, 21, 23, 24, 25, 27, 28 };

    public override void SkillActive() {
        print("use skill : " + GetSkillName() + "    LV." + skilllevel);
        //버프 부여
        skill_cool_timer = GetCoolDown();
        skill_duration_timer = GetDuration();
        SoundManager.instance.PlaySound_Voice(GetComponent<SoundContainer>().SkillActive[Random.Range(0,3)]);

        GameObject buff_iws = Instantiate(InGameManager.instance.buff);
        buff_iws.GetComponent<Buff>().Initialized(GetSkillName(), GetDuration(), GetComponent<SkillBase>().skill_icon, gameObject, gameObject, increase_dmg[skilllevel], 0, 0, 0, 0, 0, 0);
    }
    public override void SkillDescribe() {
        base.SkillDescribe();

        skill_describe = skill_describe.Replace("_dmg", increase_dmg[skilllevel].ToString());
    }
}
