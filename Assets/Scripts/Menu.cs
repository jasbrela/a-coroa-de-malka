using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartGame()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        GetComponent<AudioSource>().Play();
        Application.Quit();
    }
    
    public void MainMenu()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Menu");
    }
}
