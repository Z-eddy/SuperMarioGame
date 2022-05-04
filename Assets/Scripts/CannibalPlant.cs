using UnityEngine;
using UnityEngine.Assertions;

public class CannibalPlant : MonoBehaviour
{
    // 移动方向
    private int _direction = -1;

    // 最高Y
    private float _MaxY;

    // 最低Y
    private float _MinY;

    // 刚体
    private Rigidbody2D _rigidbody2D;
    [Tooltip("往下移动的距离(往返)")] public float MoveDistance = 2.0f;
    [Tooltip("移动速度")] public float Speed = 2.0f;

    // Start is called before the first frame update
    private void Start()
    {
        // 初始化
        _rigidbody2D = GetComponent<Rigidbody2D>();

        // 断言
        Assert.IsNotNull(_rigidbody2D);

        // 初始化最低最高
        _MaxY = _rigidbody2D.position.y;
        _MinY = _rigidbody2D.position.y - MoveDistance;
    }

    private void FixedUpdate()
    {
        // 获得当前的坐标
        var curPos = _rigidbody2D.position;
        // 根据y值判定移动方向
        if (curPos.y > _MaxY) _direction = -1;
        if (curPos.y < _MinY) _direction = 1;

        // 移动
        curPos.y += _direction * Time.deltaTime * Speed;

        _rigidbody2D.MovePosition(curPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 获得对象
        var obj = collision.gameObject;
        // 如果是角色则被伤害
        var mario = obj.GetComponent<MarioController>();
        if (mario != null)
        {
            mario.Hurt();
        }
    }
}