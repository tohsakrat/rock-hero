using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAtk : Pickup 
{
    // Start is called before the first frame update
    override public void ApplyPickup ()

	{
		//攻击力提升2
        
        //如果补给包被玩家碰到，就得到补给包
		//base.ApplyPickup();
        //加入背包
        transform.parent = Bag.b.gameObject.transform;        
        Hero.r.items.Add(this);
        this.gameObject.SetActive(false);
	}


    public Hero.status item(Hero.status s){
        return new Hero.status(s.moveSpeed,s.attack+2,s.BeatsPerMinute,s.attackRate,s.bulletSpeed,s.bulletSpread,s.maxHealth);
    }




}
