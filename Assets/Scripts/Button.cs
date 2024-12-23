using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuFunctions : MonoBehaviour
{
    public void StartButton()
    {
      SceneManager.LoadScene("SampleScene");
    }
}
