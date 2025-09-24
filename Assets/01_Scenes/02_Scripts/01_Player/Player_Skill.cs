using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_Skill : ClassSkill
{

    public abstract void DoSkill(Vector3 mousePosition);




    public abstract void DoUlti(Vector3 mousePosition);

		public abstract void DoUlti(Soldier target);


	
		
}
