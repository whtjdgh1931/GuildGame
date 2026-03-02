using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : Arrow
{

		public override void ReleaseObjectPool()
        {
                target = null;
                arrowPower = 0f;
                arrowSpeed = Constants.FIREBALL_SPEED;
        }
		
}
