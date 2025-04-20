using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartButton);
        quitButton.onClick.AddListener(QuitButton);
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
