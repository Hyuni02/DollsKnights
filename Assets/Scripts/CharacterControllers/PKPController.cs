using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKPController : MGController
{
    public int possibility;

    public override void attack() {
        SetFaceDir(Target.transform.position.x);
        if (Random.Range(0, 100) < possibility) {
            Target.GetComponent<CharacterBase>().GetAttacked((int)(fs.damage * 1.5f), fs.accuracy, fs.critrate, fs.armorpen);
            if (!SoundManager.instance.audioSource_voice.isPlaying) {
                SoundManager.instance.PlaySound_Voice(GetComponent<SoundContainer>().SkillActive[0]);
            }
        }
        else {
            Target.GetComponent<CharacterBase>().GetAttacked(fs.damage, fs.accuracy, fs.critrate, fs.armorpen);
        }
    }
}
