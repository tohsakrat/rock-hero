using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{   
    public Enemy root;
    //触发Collision2D碰撞，调用root.collision方法
    void OnCollisionEnter2D(Collision2D collision){
        //Debug.Log("EnemyCollider OnCollisionEnter2D");
        
        root.collision(collision);
    }

}
