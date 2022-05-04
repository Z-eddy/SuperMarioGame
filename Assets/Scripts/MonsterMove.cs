using UnityEngine;
using UnityEngine.Assertions;

public class MonsterMove : MonoBehaviour
{
    // 移动方向
    private int _direction = -1; // 默认左移

    // 刚体
    private Rigidbody2D _rigidbody2D;

    [Tooltip("视线长度")] public float SightLength = 0.3f;
    [Tooltip("移动速度")] public float Speed = 2.5f;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        Assert.IsNotNull(_rigidbody2D);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 查看是否碰撞到其他怪
        var monster = collision.gameObject;
        if (monster == null) return;
        if (monster.transform.position.x < transform.position.x)
            // 当遇到的怪在左边
            _direction = 1;
        else
            // 当遇到的怪在右边
            _direction = -1;
    }

    private void FixedUpdate()
    {
        CheckEnvLeftRightCollision();
    }

    // 查看地形的左右碰撞
    private void CheckEnvLeftRightCollision()
    {
        // 发起的坐标
        var origiPos = _rigidbody2D.position;
        origiPos.y += 0.5f;

        // 左边碰撞
        var leftPos = origiPos;
        var raySightLeft = Physics2D.Raycast(leftPos, Vector2.left, SightLength,
            LayerMask.GetMask("Ground"));
        Debug.DrawRay(leftPos, Vector2.left * SightLength, Color.blue);

        // 右边碰撞
        var rightPos = origiPos;
        var raySightRight = Physics2D.Raycast(rightPos, Vector2.right, SightLength,
            LayerMask.GetMask("Ground"));
        Debug.DrawRay(rightPos, Vector2.right * SightLength, Color.red);

        // 碰撞后反移动
        if (raySightLeft.collider != null) _direction = 1;
        if (raySightRight.collider != null) _direction = -1;

        // 移动
        _rigidbody2D.velocity = new Vector2(Speed * _direction, _rigidbody2D.velocity.y);
    }
}