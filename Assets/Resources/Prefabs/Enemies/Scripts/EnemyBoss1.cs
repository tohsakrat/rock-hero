using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss1 : Enemy
{	    
    public GameObject missile;//导弹对象
    public GameObject missileSample;//导弹样本
    
    public GameObject focusingSight;//瞄准准星
    
    public GameObject readyTexture;//准备发射时的贴图
    public GameObject normalTexture;//正常时的贴图
    

    public float shootSpan;//射击间隔
    public float safeRange;//射击间隔
    public float fishDis;//临时变量，用于记录敌人的距离
    public bool isShooting=false;//是否正在发射
    public bool isFocusing;//是否正在瞄准
    public bool canShoot=true;//是否可以发射
    public Vector3 missileOriginalPosition;//导弹的初始位置
    
    public Vector3 missileDirPosition;//导弹自导航前最后位置
    public float missileSpeed;//导弹速度
    public float focusingTime=2;//瞄准时间
    
    public float shootingTime=1;//射击时间
    public float shootingSpan=3;//射击间隔
    
    public float shootingRange=30;//射程

    
    void Start(){
       if(name=="")name="EnemyFriedFish";//如果没有名字，就给一个名字
       base.Start();
       missileOriginalPosition=missile.transform.position;
       
        missileSample.SetActive(false);//关闭导弹样本
    }
    
    override public void moveRule(){

        //如果正在发射，就不移动。
        if(isShooting)return;
        if(canShoot){
            if(Vector3.Distance(transform.position,Hero.r.transform.position)<=shootingRange){ //如果主角在射程内
            StartCoroutine(shootEnumerator());
            }
        }
        //安全距离内，朝着玩家移动，否则反向移动
        fishDis=Vector3.Distance(transform.position,Hero.r.transform.position);
        if(fishDis>=safeRange){
            transform.position = Vector3.MoveTowards(transform.position, Hero.r.transform.position, moveSpeed * Time.deltaTime);
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, Hero.r.transform.position, -moveSpeed * Time.deltaTime);
        }

        if(lockAngle ){
			LookAtHeroLR ();//仅左右面向玩家
		}else{
			LookAtHeroAngle();//始终面向玩家
		}



    }

    public IEnumerator shootEnumerator(){

        //瞄准阶段
        canShoot=false;
        isFocusing=true;

        readyTexture.SetActive(true);
        

        GameObject missile = Instantiate(missileSample, missileSample.transform.position, missileSample.transform.rotation,missileSample.transform.parent.transform);
        missile.SetActive(true);
        //隐藏导弹贴图（rawImage）
        //在主角的位置创建一个正放的准星
        GameObject focusingMe = Instantiate(focusingSight, Hero.r.transform.position, Quaternion.identity,Regedit.r.EnemiesParent.transform);
        focusingMe.SetActive(true);
        missile.GetComponent<EnemyTomato>().focusing= focusingMe;//导弹的脚本中，记录准星的位置
        focusingMe.GetComponent<FocusingSight>().father=missile;//准星的脚本中，记录导弹的位置
        yield return new WaitForSeconds(focusingTime);//瞄准用时三秒

        //发射阶段
        
        isFocusing=false;
        isShooting=true;
         readyTexture.SetActive(false);
        normalTexture.SetActive(true);
        focusingMe.GetComponent<FocusingSight>().stunned=true;//准星不再移动
        EnemyTomato fryTomato = missile.GetComponent<EnemyTomato>();//获取导弹的脚本

        fryTomato.startPos=fryTomato.transform.position;//设置抛物线起点,导弹的位置
        fryTomato.endPos=focusingMe.transform.position;//设置抛物线终点,玩家的位置
        fryTomato.FindParabolaEquation();//计算抛物线方程

        //发射后硬直阶段
        if(focusingMe!=null)Destroy(focusingMe);//销毁准星
        isShooting=true;
        yield return new WaitForSeconds(shootingTime);//射击硬直

        //空载状态
        isShooting=false;
        yield return new WaitForSeconds(shootingSpan);

        //重新释放，准备下一次射击
        canShoot=true;

    }


   
}

