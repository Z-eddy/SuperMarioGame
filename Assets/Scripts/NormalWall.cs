using UnityEngine;
using UnityEngine.Assertions;

public class NormalWall : MonoBehaviour
{
    // 本身图片
    private SpriteRenderer _spriteRenderer;

    [Tooltip("墙碎裂特效")] public GameObject WallDestroy = null;

    [Tooltip("强碎裂音效")] public AudioClip WallDestroyClip = null;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        /***********断言**********/
        Assert.IsNotNull(WallDestroyClip);
        Assert.IsNotNull(WallDestroy);
    }

    // 产生碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 获得脚本
        var mario = collision.gameObject.GetComponent<MarioController>();

        // 只有当mario在砖下顶
        if (mario == null || mario.transform.position.y > transform.position.y) return;

        switch (mario.MarioStatus)
        {
            // 小时碰撞无视
            case MarioController.Status.Small:
                break;
            // 大时碰撞
            case MarioController.Status.Big:
            case MarioController.Status.Super:
                // 播放动画并碎裂
                Instantiate(WallDestroy, transform.position, Quaternion.identity);
                // 播放音效
                mario.PlaySound(WallDestroyClip);
                // 透明
                _spriteRenderer.color = new Color(0, 0, 0, 0);
                // 销毁
                Destroy(gameObject);
                break;
        }
    }
}