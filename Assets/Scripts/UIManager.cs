using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
