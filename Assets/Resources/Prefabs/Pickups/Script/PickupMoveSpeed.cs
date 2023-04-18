using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMoveSpeed : Pickup 
{
    // Start is called before the first frame update
    override public void ApplyPickup ()
	{
		//移动速度提升2
        Hero.r.moveSpeed += 2;
        
        //如果补给包被玩家碰到，就得到补给包
		base.ApplyPickup();

	}
}
