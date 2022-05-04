using UnityEngine;
using UnityEngine.Assertions;

public class Projectile : MonoBehaviour
{
    // 获得刚体
    private Rigidbody2D _rigidbody2D;

    [Tooltip("发射的力度")] public float Force = 300.0f;
    [Tooltip("子弹存在的时间")] public float ElapsedTime = 3.0f;
    // 当前剩余时间
    private float curElapsedTime;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        curElapsedTime = ElapsedTime;

        /**********断言***********/
        Assert.IsNotNull(_rigidbody2D);
    }

    private void Update()
    {
        curElapsedTime -= Time.deltaTime;
        if (curElapsedTime < 0)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    ///     永远向前方发射子弹
    /// </summary>
    public void Shoot()
    {
        // 自动向右方冲
        _rigidbody2D?.AddForce(Vector2.right * Force);
    }
}