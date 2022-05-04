using UnityEngine;
using UnityEngine.Assertions;

public class ChestnutMonster : MonoBehaviour
{
    // 刚体
    private Rigidbody2D _rigidbody2D;

    // [Tooltip("被打后变形")] public Sprite DamageSprite;
    [Tooltip("mario跳跃高度大于此值将被踩死")] public float DamageHeight = 0.5f;
    [Tooltip("被打死后播放音效")]public AudioClip DamageAudio=null;

    // Start is called before the first frame update
    private void Start()
    {
        // 初始化
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 断言
        Assert.IsNotNull(_rigidbody2D);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 获得实例化的对象
        var itemObj = collision.gameObject;
        /********碰撞到子弹*********/
        var projectile = itemObj.GetComponent<Projectile>();
        if (projectile != null)
        {
            Destroy(itemObj); // 子弹消失
            Death(); // 自身死亡
            return;
        }

        /********碰撞到角色*********/
        var mario = itemObj.GetComponent<MarioController>();
        if (mario?.CurrentY > _rigidbody2D.position.y + DamageHeight)
        {
            // mario跳踩
            mario.PlaySound(DamageAudio);
            Death(); // 自身死亡
        }
        else
            // mario被撞
            mario?.Hurt();
    }

    // 死亡
    private void Death()
    {
        Destroy(gameObject);
    }
}