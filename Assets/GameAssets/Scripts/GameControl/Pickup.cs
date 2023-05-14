using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour 
{
	public int health;
	public string name;//名字
	public string commit;//描述
	public SpriteRenderer sr;//贴图对象
	public Texture2D tx;//贴图素材
	public float timeAlive;
	public bool isItem;

	public void Start(){
		Regedit.Pickups.Add(this);//注册当前补给到全局list中
		if(sr==null)sr=gameObject.GetComponent<SpriteRenderer>();//如果贴图未绑定，则绑定自己

	}
	public void Awake(){
		//如果没起名，就用对象名作为名字
		if(name == "")name = gameObject.name;
	}
	public void Update ()
	{	
		//不同于start和awake，因为涉及到时序，不采用c#的base直接继承，而是规定好生命周期，然后在子类中对不同生命周期对应的方法进行重写，这样就可以实现多态了。
		//如果未激活，直接返回
		if(!gameObject.activeSelf)return;
		liveRule();
		moveRule();
	}

	virtual public void liveRule(){
		//存活规则，每个补给都有自己的活着的规则,可以对这个方法进行拓展
		//如果超时不捡起，就消失
		timeAlive -= Time.deltaTime;
		if(timeAlive <= 3.0f)Game.g.SpriteFlash(sr);//贴图闪烁，警告
		if(timeAlive <= 0.0f)
		Destroy(gameObject);
	}
		virtual public void moveRule(){
		//移动规则，每个补给都有自己的移动规则,可以对这个方法进行拓展
		//默认固定不动

	}
	virtual public void applyName(){
		//如果没起名，就用对象名作为名字
		if(name == "")name = gameObject.name;
	}

	virtual public void saveItem(){
		//如果没起名，就用对象名作为名字
		        transform.parent = Bag.b.gameObject.transform;//加入背包    
        Hero.r.items.Add(this);
        this.gameObject.SetActive(false);

         //运行基类获取补给包事件

	}

	//如果补给包被打中，就减少血量
	virtual public void TakeDamage (int dmg)
	{
		if(health - dmg <= 0)
		{
			Destroy(gameObject);
		}
		else
		{
			health -= dmg;
			transform.localScale += new Vector3(0.2f, 0.2f, 0.0f);
		}

		//CameraController.c.Shake(0.3f, 0.5f, 50.0f);
		Game.g.SpriteFlash(sr);
	}


	//如果补给包被玩家碰到，就得到补给包
		void OnCollisionEnter2D (Collision2D col)
	{

		//
		if(col.gameObject.tag == "Hero")
		{
	
	
				ApplyPickup ();
			


		}
	}


	//得到补给包
	virtual public void ApplyPickup ()
	{
		//播放补给音效
		AudioManager.am.PlayGetPickup();
		Destroy(gameObject);

	}
	
	public static GameObject  GenerateRandomPickup ()
	{
		//随机生成一个补给包
		//先随便写，回来改
		return new GameObject();
	}
    virtual public Hero.status item(){
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

        return new Hero.status(0,0,0,0,0,0,0,0,0,0,0);//攻击力提升2
    }
	/*
	IEnumerator Shrink ()
	{
		//Scale it down overtime, then destroy it.
		while(transform.localScale.x > 0.0f)
		{
			transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.zero, Time.deltaTime * 0.5f);
			yield return null;
		}

		Destroy(gameObject);
	}*/
}
