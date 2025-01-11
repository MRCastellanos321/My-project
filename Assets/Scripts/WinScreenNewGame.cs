using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenNewGame : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
