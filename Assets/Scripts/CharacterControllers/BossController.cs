using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    public uint stunFrame = 0;
    public Transform skillPoint;

    //public override void UpdateState() {
    //    throw new System.NotImplementedException();
    //}

    //public override void Getstun() {
    //    throw new System.NotImplementedException();
    //}

    public override void Getstun() {
        uac.animation.GotoAndStopByFrame("die", stunFrame);
    }
}
