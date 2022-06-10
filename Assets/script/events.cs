using UnityEngine.SceneManagement;
using UnityEngine;

public class events : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
