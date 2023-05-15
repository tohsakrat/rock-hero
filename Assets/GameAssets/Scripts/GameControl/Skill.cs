

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    /*---------技能基类*--------*/

    public string name;//技能名
    public bool canUse;//是否可以使用
    public GameObject bulletPrefab;//子弹预制体，如果未绑定则默认绑定自己
    public  Sprite icon;//技能图标
    public  int Beats;//节拍数

	public AudioClip skillAudio;
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

    virtual public void triggerWithAudio(){
        //播放音效
        if(skillAudio != null){
            AudioManager.am.Play(skillAudio);
        }
        //触发技能
        trigger();
    }
    virtual public void trigger(){
         

            GameObject bullet = Instantiate(bulletPrefab, Hero.r.transform.position, Hero.r.transform.rotation,Regedit.r.BulletLayer);

            bullet.SetActive(true);



				
			}


		}

    

