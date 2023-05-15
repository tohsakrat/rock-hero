using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeiMoFaSuCai : Pickup 
{
    // 捡起获得buff的道具模板
    
	override public void applyName(){

		//起名
		if(name=="")name =" 黑魔法素材 "; //上面类名英文，和文件名一致，这个写中文给玩家看
		if(commit =="")commit = "曾经有一位黑魔法术士尝试让一盆植物唱歌，这就是他所使用的魔法素材。";//道具小作文，中文
	}



    override public Hero.status item(){
        //填写数值模板的小伙伴看这里
        //这里是道具的效果，可以自己定义
        //每个位置数字含义如下

		/*public float maxHealth;//生命上限
		public float attack;//攻击力
		public float BeatsPerMinute;//每分钟拍数
		public float attackRate;//攻击速度，每拍打几下
		public float moveSpeed;//移动速度
		public float pickupRadius;//拾起半径
		public float luck;//幸运值，影响掉落率
		public float healthRecover;//生命恢复
		public float healthSteal;//吸血率
		public float bulletSpeed;//子弹速度，是一个倍率，用于乘以具体子弹的速度
		public float bulletRange ;//射程*/

        return new Hero.status(0,0,0,0,0,0,0,0,0,0,0);
    }


    override public void ApplyPickup ()

	{   //基本不用修改

        saveItem();//存入背包
		base.ApplyPickup(); //运行基类获取补给包事件，播放音效销毁等
	}


}
