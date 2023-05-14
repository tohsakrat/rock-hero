using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHealth : Pickup 
{
    // 捡起立即生效的道具模板
	public void applyName(){

		//起名
		if(name=="")name ="health pickup";

	}
    override public void ApplyPickup ()
	{
        //打印
        Debug.Log("Health pickup applied");
		//获得2点生命
        Hero.r.health += 2;
        
        //运行基类获取补给包事件，播放音效销毁等
		base.ApplyPickup();

        //主角播放治愈动画
        Hero.r.healAnimate();

	}
}
