using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Assertions;

public class Mushroom : MonoBehaviour
{
    [Tooltip("蘑菇刷新的音效")]public AudioClip SpawnAudioClip=null;
    [Tooltip("蘑菇被吃掉的音效")]public AudioClip EatAudioClip=null;
    [Tooltip("蘑菇的速度")] public float Speed = 2.0f;

    private Rigidbody2D _rigidbody2D=null;// 当前的刚体
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        /*********断言*********/
        // 必须已经在引擎设定好
        Assert.IsNotNull(SpawnAudioClip);
        Assert.IsNotNull(EatAudioClip);
        Assert.IsNotNull(_rigidbody2D);
    }

    private void FixedUpdate()
    {
        var curPos = _rigidbody2D.position;
        curPos.x += Time.deltaTime * Speed;
        _rigidbody2D.MovePosition(curPos);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 只与 mario 进行碰撞
        var mario=collision.gameObject.GetComponent<MarioController>();
        if(mario==null)return;

        mario.PlaySound(EatAudioClip);// 播放吃掉音效
        mario.Grow();// 角色长大

        Destroy(gameObject);// 蘑菇自我销毁
    }
}
