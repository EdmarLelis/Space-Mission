using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;

    public void Setup(int score)
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.loseMusic, 0.6f, false);
        AudioManager.Instance.StopAmbientSFX();
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
        Time.timeScale = 0f;
    }
    public void Restart()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click, 0.4f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.Instance.PlayAmbietSFX(AudioManager.Instance.rocket, 0.4f);
        AudioManager.Instance.PlayMusic(AudioManager.Instance.music, 0.4f);
        GameController.Instance.isPaused = false;
    }
    public void Exit()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click, 0.4f);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
