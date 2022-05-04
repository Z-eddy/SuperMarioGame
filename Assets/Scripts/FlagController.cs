using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    // 碰撞了旗帜
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var mario=collision.gameObject.GetComponent<MarioController>();
            // mario碰撞上了
        if(mario!=null)
        {
            // 获得自身刚体
            var tempRigidBody=GetComponent<Rigidbody2D>();
            tempRigidBody.gravityScale = 1;
        }
    }
}
