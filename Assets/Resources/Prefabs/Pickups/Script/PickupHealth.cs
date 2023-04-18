using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup 
{
    // Start is called before the first frame update
    override public void ApplyPickup ()
	{
		//获得2点生命
        Hero.r.health += 2;
        
        //运行基类获取补给包事件
		base.ApplyPickup();

	}
}
