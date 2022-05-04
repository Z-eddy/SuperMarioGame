using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMario : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene(1);
    }
}