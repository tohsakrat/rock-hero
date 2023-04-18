using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public AVGController avg;
    // Start is called before the first frame update
    void Start()
    {
        List<json.dialogue> list = new List<json.dialogue>() ;
        list.Add(new json.dialogue(0f, 10f, "", "box","","","fadeIn",true));//第一条演出，给对话框本身也加个渐入
        list.Add(new json.dialogue(0f, 4f, "你妈走进了房间", "system"));//系统消息。这是演示一下省略后四个参数的用法。
        list.Add(new json.dialogue(.5f, 0f, "", "mom","","info","fadeIn",true));//妈妈的立绘进入，不说话
        avg.defToPlay(list);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
