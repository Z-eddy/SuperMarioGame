using UnityEngine;
using UnityEngine.Assertions;

public class GoldCoin : MonoBehaviour
{
    // "金币的消失时间"
    private readonly float _coinElapsedTime = 1.0f;

    // "金币上移的高度" 
    private readonly float _coinMoveHeight = 0.8f;

    // 金币的剩余显示时间
    private float _goldShowTime;

    [Tooltip("金币刷新/被吃的音效")] public AudioClip GoldCoinAudioClip = null;

    /// <summary>
    ///     是否是静态等待吃的金币
    /// </summary>
    public bool IsStableCoin { get; set; } = true;

    // Start is called before the first frame update
    private void Start()
    {
        // 初始化金币的总显示时间
        _goldShowTime = _coinElapsedTime;

        Assert.IsNotNull(GoldCoinAudioClip);
    }

    // Update is called once per frame
    private void Update()
    {
        // 动态的金币
        if (!IsStableCoin)
        {
            if (_goldShowTime > 0)
            {
                /******金币位移********/
                // 金币当前位置
                var curPos = transform.position;
                curPos.y += _coinMoveHeight * Time.deltaTime;
                transform.position = curPos;
                _goldShowTime -= Time.deltaTime;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}