using UnityEngine;
using UnityEngine.SceneManagement;

public class pressed : MonoBehaviour
{
    void Start()
    {
    }

    public void StartButton()
    {
        Debug.Log("Start Clicked");
        SceneManager.LoadScene(0);
    }

    public void QuitButton()
    {
        Debug.Log("Quit Clicked");
        Application.Quit();
    }
}
