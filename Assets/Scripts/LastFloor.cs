using UnityEngine;
using UnityEngine.SceneManagement;

public class LastFloor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var mario = collision.gameObject.GetComponent<MarioController>();
        if (mario != null)
            SceneManager.LoadScene(2);
    }
}