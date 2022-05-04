using UnityEngine;
using UnityEngine.Assertions;

public class MarioMove : MonoBehaviour
{
    // 是否按下跳跃
    private bool _isNeedJump;
    [Tooltip("手机方向控制摇杆")] public Joystick _joystick = null;

    // 获得自身刚体
    private Rigidbody2D _rigidbody2D;

    [Tooltip("距离地面高度,判定是否在地面上")] public float FloorDistance = 0.05f;

    [Tooltip("跳跃时音效")] public AudioClip JumpAudioClip = null;
    [Tooltip("向上的速度")] public float JumpForce = 11.5f;

    [Tooltip("速度")] public float Speed = 3.0f;

    // Start is called before the first frame update
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        /************断言************/
        Assert.IsNotNull(_rigidbody2D);
        Assert.IsNotNull(JumpAudioClip);
        Assert.IsNotNull(_joystick);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) _isNeedJump = true;
    }

    protected virtual void FixedUpdate()
    {
        // 键盘控制
        var horizontalKeyboard = Input.GetAxis("Horizontal");
        // 当输入时进行对应方向的移动
        if (horizontalKeyboard != 0)
            _rigidbody2D.velocity = new Vector2(horizontalKeyboard * Speed, _rigidbody2D.velocity.y);

        // 手机控制
        var horizontalJoystick = _joystick.Horizontal;
        // 当输入时进行对应方向的移动
        if (horizontalJoystick != 0)
            _rigidbody2D.velocity = new Vector2(horizontalJoystick * Speed, _rigidbody2D.velocity.y);

        // 按下跳跃
        if (_isNeedJump)
        {
            _isNeedJump = false;
            // 检测是否在地上
            var tempRayHit = Physics2D.Raycast(_rigidbody2D.position, Vector2.down, FloorDistance,
                LayerMask.GetMask("Ground") | LayerMask.GetMask("Collectable"));
            // 在地板上
            if (tempRayHit.collider != null) Jump();
        }
    }

    /// <summary>
    ///     进行跳跃
    /// </summary>
    private void Jump()
    {
        gameObject.GetComponent<MarioController>()?.PlaySound(JumpAudioClip);
        _rigidbody2D.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
    }

    /// <summary>
    ///     手机操控,使用按钮进行跳跃
    /// </summary>
    public void JoyBtnJump()
    {
        _isNeedJump = true;
    }
}