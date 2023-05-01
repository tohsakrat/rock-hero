using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sweep : Skill
{   

    public Vector3 v;

    public void Start(){
       if(name=="")this.name="Sweep";//如果没有名字，就给一个名字
        //在这里写自己的初始化逻辑
        //调用父类的Start方法
       base.Start();
    }

    override public void trigger(){
        
      // v=v*bulletSpeed;
        Even();
        Invoke("Odd",60f/Hero.r.BeatsPerMinute/Hero.r.attackRate/8);
        Invoke("Even",60f/Hero.r.BeatsPerMinute/Hero.r.attackRate/4);
        Invoke("Odd",3f*60f/Hero.r.BeatsPerMinute/Hero.r.attackRate/3);
    
    }

    private void Even(){

        GameObject bullet1 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        
        bullet1.GetComponent<BulletSweep>().setSpeed(v*bulletSpeed);
        bullet1.SetActive(true);
        GameObject bullet2 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        bullet2.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,90));
        bullet2.SetActive(true);
        GameObject bullet3 = Instantiate(bulletPrefab,Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        bullet3.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,180));
        bullet3.SetActive(true);
        GameObject bullet4 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        bullet4.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,270));
        bullet4.SetActive(true);

    }

     private void Odd(){

        GameObject bullet1 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        bullet1.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,45));
        bullet1.SetActive(true);
        GameObject bullet2 = Instantiate(bulletPrefab,Hero.r.transform.position, new Quaternion(0,0,0,0),Regedit.blt);
        bullet2.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,135));
        bullet2.SetActive(true);
        GameObject bullet3 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0),Regedit.blt);
        bullet3.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,225));
        bullet3.SetActive(true);
        GameObject bullet4 = Instantiate(bulletPrefab, Hero.r.transform.position,new Quaternion(0,0,0,0), Regedit.blt);
        bullet4.GetComponent<BulletSweep>().setSpeed(RotateRound(v*bulletSpeed,315));
        bullet4.SetActive(true);
    }

    /// <summary>
    /// 计算一个Vector3绕指定轴旋转指定角度后所得到的向量。
    /// </summary>
    /// <param name="source">旋转前的源Vector3</param>
    /// <param name="axis">旋转轴</param>
    /// <param name="angle">旋转角度</param>
    /// <returns>旋转后得到的新Vector3</returns>
    
    public Vector3 RotateRound(Vector3 source,  float angle)
    {
        Vector3 axis = new  Vector3(0,0,1);
        Quaternion q = Quaternion.AngleAxis(angle, axis);// 旋转系数
        return q * source;// 返回目标点
    }

}
