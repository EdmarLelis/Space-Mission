using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsScreen : MonoBehaviour
{

    public void Setup()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.Instance.musicSource.volume = .2f;
        AudioManager.Instance.StopAmbientSFX();
    }
    public void Resume()
    {
        gameObject.SetActive(false);
        AudioManager.Instance.musicSource.volume = .6f;
        AudioManager.Instance.PlayAmbietSFX(AudioManager.Instance.rocket, 0.4f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click, 0.4f);
        Time.timeScale = 1f;
    }
    public void Exit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click, 0.4f);
        AudioManager.Instance.StopAmbientSFX();
        AudioManager.Instance.StopMusic();
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
