using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rdm : MonoBehaviour
{
    // public static rdm instance;//实例化
    static public int timeee;
    // Start is called before the first frame update
    void Start()
    {
    //    instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static  public  void Randomtimeer(int minrang,int maxrang)//随机时间函数，参数为最小范围、最大范围、开始保底时间
    {
        timeee= Random.Range(minrang, maxrang);
    }

}
