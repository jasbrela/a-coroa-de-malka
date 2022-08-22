using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip click;
    
    public void StartGame()
    {
        source.PlayOneShot(click);
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        source.PlayOneShot(click);
        Application.Quit();
    }
    
    public void MainMenu()
    {
        source.PlayOneShot(click);
        SceneManager.LoadScene("Menu");
    }
}
