

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    /*---------技能基类*--------*/

    public string name;//技能名
    public bool canUse;//是否可以使用
    public GameObject bulletPrefab;//子弹预制体，如果未绑定则默认绑定自己
    public float bulletSpeed;//子弹速度
    public  SpriteRenderer icon;//技能图标


	public void Start(){}
	public void Awake(){
        
        //在这里写自己的初始化逻辑 
        //如果子弹预制体未绑定，则绑定gameObject
        if(bulletPrefab == null){
            bulletPrefab = gameObject;
        }
		//如果没起名，就用对象名作为名字
		if(name == "")name = gameObject.name;
	}

    virtual public void trigger(){
         

            GameObject bullet = Instantiate(bulletPrefab, Hero.r.transform.position, Hero.r.transform.rotation,Regedit.blt);

            bullet.SetActive(true);


           // Vector3 dir1 = ((mouseDetection.pos- Hero.r.transform.position).normalized) ;
         //   dir1 *=  (bulletSpeed * Random.Range(0.8f, 1.1f));
         //   bullet1.GetComponent<Rigidbody2D>().velocity = dir1;

				
			}


		}

    

