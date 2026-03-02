using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : Arrow
{
    public override void ReleaseObjectPool()
    {
        target = null;
        arrowPower = 0f;
        arrowSpeed = Constants.DART_SPEED;
    }
}
