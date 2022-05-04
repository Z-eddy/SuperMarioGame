using UnityEngine;

public class DeathLine : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 获得原始碰撞组件
        var obj = collision.gameObject;
        /**********查看碰撞组件的类型并进行相应死亡处理*********/
        // mario
        var mario = obj.GetComponent<MarioController>();
        if (mario != null)
        {
            mario.Death();
            return;
        }

        // 怪
        var monster = obj.GetComponent<MonsterMove>();
        if (monster != null) Destroy(obj);
    }
}