using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyYellow : Enemy
{	void Start(){

         if(name=="")name="EnemyYellow";//如果没有名字，就给一个名字
    	//在这里写自己的初始化逻辑
        //黄色怪有盾
        GameObject shield2 = transform.Find("Shield2").gameObject;
        int ranNum = Random.Range(1, 5);
        if(ranNum == 1)
            shield2.transform.localEulerAngles = new Vector3(0, 0, 89);
        if(ranNum == 2)
            shield2.transform.localEulerAngles = new Vector3(0, 0, -180);
        if(ranNum >= 3)
            shield2.SetActive(false);
		//调用父类的Start方法
        base.Start();

}


}

