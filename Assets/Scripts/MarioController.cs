using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class MarioController : MonoBehaviour
{
    // 人物状态
    public enum Status
    {
        Small,
        Big,
        Super
    }

    // 动画管理
    private Animator _animator;

    // 获得音效播放
    private AudioSource _audioSource;

    // big状态碰撞矩形框<Offset,Size>
    private KeyValuePair<Vector2, Vector2> _bigBoxCollider;

    // 碰撞矩形框变化
    private BoxCollider2D _boxCollider;

    // 是否可以发射子弹
    private bool _canShoot;

    // 刚体
    private Rigidbody2D _rigidbody2D;

    // small状态碰撞矩形框<Offset,Size>
    private KeyValuePair<Vector2, Vector2> _smallBoxCollider;

    [Tooltip("子弹类型")] public GameObject Projectile = null;
    [Tooltip("被缩小时播放音效")] public AudioClip ToSmallAudioClip = null;

    public float CurrentY => _rigidbody2D.position.y;

    /// <summary>
    ///     当前的人物状态
    /// </summary>
    public Status MarioStatus { get; private set; } = Status.Small;

    // Start is called before the first frame update
    private void Start()
    {
        // 初始化
        _audioSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _smallBoxCollider = new KeyValuePair<Vector2, Vector2>(_boxCollider.offset, _boxCollider.size);

        // 只设置big碰撞框,small与原始一致
        var bigOffset = _boxCollider.offset;
        var bigSize = _boxCollider.size;
        bigOffset.y *= 2;
        bigSize.y *= 2;
        _bigBoxCollider = new KeyValuePair<Vector2, Vector2>(bigOffset, bigSize);

        /*********断言*********/
        Assert.IsNotNull(_audioSource);
        Assert.IsNotNull(_animator);
        Assert.IsNotNull(_boxCollider);
    }

    private void Update()
    {
        if (_canShoot && Input.GetKeyDown(KeyCode.J))
        {
            var tempProjectile = Instantiate(Projectile, _rigidbody2D.position, Quaternion.identity);
            var tempProjectileScript = tempProjectile.GetComponent<Projectile>();
            tempProjectileScript.Shoot();
        }
    }

    /// <summary>
    ///     播放音效
    /// </summary>
    /// <param name="theAudioClip">具体的音效</param>
    public void PlaySound(AudioClip theAudioClip)
    {
        _audioSource.PlayOneShot(theAudioClip);
    }

    /// <summary>
    ///     生长变大
    /// </summary>
    public void Grow()
    {
        switch (MarioStatus)
        {
            case Status.Small:
                ChangeState(Status.Big);
                break;
            case Status.Big:
                ChangeState(Status.Super);
                break;
        }
    }

    /// <summary>
    ///     被伤害了
    /// </summary>
    public void Hurt()
    {
        PlaySound(ToSmallAudioClip);
        switch (MarioStatus)
        {
            case Status.Small:
                Death();
                break;
            case Status.Big:
                ChangeState(Status.Small);
                break;
            case Status.Super:
                ChangeState(Status.Big);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    /// <summary>
    ///     人物死亡,游戏结束,返回初始界面
    /// </summary>
    public void Death()
    {
        // 游戏结束
        // Debug.Log("游戏结束");
        SceneManager.LoadScene(0);
    }

    // 更改当前的状态
    private void ChangeState(Status arg)
    {
        MarioStatus = arg;
        // 根据状态切换形态
        switch (MarioStatus)
        {
            case Status.Small:
                _canShoot = false;
                _animator.SetFloat("Status", 0);
                break;
            case Status.Big:
                _canShoot = false;
                _animator.SetFloat("Status", 0.5f);
                break;
            case Status.Super:
                _canShoot = true;
                _animator.SetFloat("Status", 1);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        // 改变当前对象的碰撞矩形框
        ChangeCollider(arg);
    }

    private void ChangeCollider(Status arg)
    {
        switch (arg)
        {
            case Status.Small:
                _boxCollider.offset = _smallBoxCollider.Key;
                _boxCollider.size = _smallBoxCollider.Value;
                break;
            case Status.Big:
            case Status.Super:
                _boxCollider.offset = _bigBoxCollider.Key;
                _boxCollider.size = _bigBoxCollider.Value;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(arg), arg, null);
        }
    }

    /// <summary>
    ///     手机操控,使用按钮进行子弹发射
    /// </summary>
    public void JoyBtnShoot()
    {
        if (_canShoot)
        {
            var tempProjectile = Instantiate(Projectile, _rigidbody2D.position, Quaternion.identity);
            var tempProjectileScript = tempProjectile.GetComponent<Projectile>();
            tempProjectileScript.Shoot();
        }
    }
}