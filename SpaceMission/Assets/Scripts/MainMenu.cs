using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        AudioManagerMainMenu.Instance.PlayMusic(AudioManagerMainMenu.Instance.menuMusic, 0.6f);
        AudioManagerMainMenu.Instance.StopAmbientSFX();
    }


    public void OnStart()
    {
        AudioManagerMainMenu.Instance.PlaySFX(AudioManagerMainMenu.Instance.click, 0.4f);
        Time.timeScale = 1f;
        AudioManagerMainMenu.Instance.StopMusic();
        SceneManager.LoadScene("SampleScene");
    }
    public void OnExit()
    {
        AudioManagerMainMenu.Instance.PlaySFX(AudioManagerMainMenu.Instance.click, 0.4f);
        Time.timeScale = 1f;
        Application.Quit();
        Debug.Log("O jogo foi encerrado.");
    }
}
