using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyTomato : Enemy
{	
    [Header("薯条怪专属")]
    public Vector3 startPos;//开始位置
    public Vector3 endPos;//结束位置  
    public Vector3 topPos;//最高点
    public float a;//抛物线系数1
    public float b;//抛物线系数2
    public float c;//抛物线系数3
    public bool isSample = true;//是否是样本
    
    public GameObject focusing;   

        
    
    
    void Start(){
       if(name=="")this.name="EnemyTomato";//如果没有名字，就给一个名字
        base.Start();
        colliderTriger.SetActive(false);//关闭碰撞体
        stunned=true;//一开始就处于眩晕状态
    }

    override public void moveRule(){    
       
        if(isSample)return;//如果是样本，就不移动
      //沿着计算出的抛物线移动
       timer+=Time.deltaTime;
       //角度跟随抛物线斜率
         float angle = Mathf.Atan(2*a*transform.position.x+b);
            transform.rotation=Quaternion.Euler(0,0,angle*Mathf.Rad2Deg);
        //缩放渐变
        float scaleX = Mathf.MoveTowards(  transform.localScale.x, 3,10*Time.deltaTime);
        transform.localScale =new Vector3(scaleX, transform.localScale.y,transform.localScale.z);
        //移动
        float x = Mathf.MoveTowards(  transform.position.x ,endPos.x,moveSpeed*Time.deltaTime);
        float y = a*x*x+b*x+c;
        transform.position=new Vector3(x,y,0);
        if(Vector3.Distance(transform.position,endPos)<0.1 ){
            //离终点距离<1
            //Die();
           // stunned=true;
           colliderTriger.SetActive(true);
           isSample=true;
           
           //旋转到-90度
            transform.rotation=Quaternion.Euler(0,0,-90);
            transform.localScale =new Vector3(3,transform.localScale.y,transform.localScale.z);
            GetComponent<SpriteRenderer>().enabled=false;
            GetComponent<RawImage>().enabled=false;

            Destroy(focusing);
        }




    }

   public void FindParabolaEquation(){
        //startPos.y和endPos.y最大值
        
        //计算弹道抛物线方程
        topPos=new Vector3((startPos.x+endPos.x)/2,
        Mathf.Max(startPos.y,endPos.y)+1,
        (startPos.z+endPos.z)/2);


        float x1=startPos.x;
        float y1=startPos.y;
        float x3=topPos.x;
        float y3=topPos.y;
        float x2=endPos.x;
        float y2=endPos.y;
        float denom = (x1 - x2) * (x1 - x3) * (x2 - x3);
        a =  (x3 * (y2 - y1) + x2 * (y1 - y3) + x1 * (y3 - y2)) / denom;
        b =  (x3*x3 * (y1 - y2) + x2*x2 * (y3 - y1) + x1*x1 * (y2 - y3)) / denom;
        c =  (x2 * x3 * (x2 - x3) * y1 + x3 * x1 * (x3 - x1) * y2 + x1 * x2 * (x1 - x2) * y3) / denom;
        stunned=false;//计算完毕，解除眩晕状态
        isSample=false;//不再是样本
        transform.SetParent(Regedit.r.EnemiesParent.transform);//将自己放到敌人父物体下
    }

    //拓展死亡方法
    override public void Die(){
        Debug.Log("薯条Die");

        base.Die();
        if(focusing!=null)Destroy(focusing);
    }


}

