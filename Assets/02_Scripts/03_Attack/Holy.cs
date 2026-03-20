using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holy : Arrow
{
    public override void ReleaseObjectPool()
    {
        target = null;
        arrowPower = 0f;
        arrowSpeed = Constants.HOLY_SPEED;
    }
}
