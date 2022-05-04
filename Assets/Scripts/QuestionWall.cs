using UnityEngine;

public class QuestionWall : MonoBehaviour
{
    // 生成的物品
    // private GameObject collectableItem;

    // 是否为白板墙
    private bool _isBlankWall;
    [Tooltip("被撞后变化的精灵")] public Sprite Change2Sprite = null;
    [Tooltip("碰撞时刷新的物体")] public GameObject CollectableClass = null;
    [Tooltip("物品在上方生成的最高位置")] public float CollectItemHeight = 0.5f;
    [Tooltip("碰撞时播放音效")] public AudioClip CollisionAudio = null;

    // 产生了碰撞
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 白板墙直接返回
        if (_isBlankWall) return;
        // 判定是mario在下面顶
        var mario = collision.gameObject?.GetComponentInParent<MarioController>();
        /********播放音乐********/
        // 只有mario在顶时
        if (mario == null || mario.transform.position.y>transform.position.y)
            return;
        else
            // 播放音乐
            mario.PlaySound(CollisionAudio);

        /********精灵替换********/
        var tempSpriteRender = GetComponent<SpriteRenderer>();
        tempSpriteRender.sprite = Change2Sprite;
        _isBlankWall = true;

        /********刷新物品********/
        Vector2 coinPos = transform.position;
        coinPos.y += CollectItemHeight;
        // 实例化
        var collectableItem = Instantiate(CollectableClass, coinPos, Quaternion.identity);
        // 处理刷新出来的物品
        DealCollectItem(mario,collectableItem);
    }
    /// <summary>
    ///     对刷新出来的物品,根据物品种类进行处理
    /// </summary>
    private void DealCollectItem(MarioController mario,GameObject collectableItem)
    {
        /*******刷新的是金币********/
        var goldCoin = collectableItem.GetComponent<GoldCoin>();
        if (goldCoin != null)
        {
            // 静态的金币
            goldCoin.IsStableCoin = false;
            // 播放金币的音效
            mario.PlaySound(goldCoin.GoldCoinAudioClip);
        }

        /*******刷新的是蘑菇********/
        var mushroom = collectableItem.GetComponent<Mushroom>();
        if (mushroom != null)
            // 播放蘑菇刷新音效
            mario.PlaySound(mushroom.SpawnAudioClip);
    }
}