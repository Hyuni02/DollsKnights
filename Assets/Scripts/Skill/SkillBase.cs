using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public enum Type { boost, action}

    public int level;
    public string skill_name;
    [TextArea]
    public string skill_describe_form;
    [TextArea]
    public string skill_describe;
    public Sprite skill_icon;
    public Type type;
    public float skill_InitCooltime;
    public float[] skill_Cooltime;
    public float[] skill_Duration;
    public bool autoActive;

    //[HideInInspector]
    public float skill_cool_timer, skill_duration_timer;

    public void Start() {
        skill_cool_timer = skill_InitCooltime;

        GetLevel();
    }
    public void Update() {
        if(skill_cool_timer >= 0)
            skill_cool_timer -= Time.deltaTime;
        if(skill_duration_timer >= 0)
            skill_duration_timer -= Time.deltaTime;
    }

    public virtual string GetSkillName() {
        return skill_name;
    }

    public void GetLevel() {
        for (int i = 0; i < GetData.instance.List_DollData.Count; i++) {
            if (GetData.instance.List_DollData[i].name.Equals(transform.name.Replace("(Clone)", ""))) {
                level = GetData.instance.List_DollData[i].level;
                break;
            }
        }
    }
    public virtual void SkillDescribe() {
        GetLevel();
    }
    public virtual float GetDuration() {
        return skill_Duration[level - 1];
    }
    public virtual float GetCoolDown() {
        return skill_Cooltime[level - 1];
    }

    public abstract void SkillActive();

}
