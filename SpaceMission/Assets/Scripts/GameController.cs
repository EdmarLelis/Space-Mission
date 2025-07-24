using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }
    public GameOverScreen GameOverScreen;
    public OptionsScreen OptionsScreen;
    public bool isPaused = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GameOver()
    {
        GameOverScreen.Setup(PlayerConstants.Instance.points);
        isPaused = true;
    }

    public void Options()
    {
        isPaused = true;
        OptionsScreen.Setup();
    }

    public void Resume()
    {
        isPaused = false;
        OptionsScreen.Resume();
    }

    void Start()
    {
        AudioManager.Instance.PlayMusic(AudioManager.Instance.music, 0.6f);
        AudioManager.Instance.PlayAmbietSFX(AudioManager.Instance.rocket, 0.4f);
    }
}
