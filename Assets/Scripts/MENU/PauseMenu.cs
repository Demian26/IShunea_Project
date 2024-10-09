using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    private AudioSource audioSource;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // Получаем компонент AudioSource для воспроизведения звуков
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame(!pauseScreen.activeInHierarchy);
        }
    }

    #region Game Over
    public void GameOver()
    {
        // Включаем экран Game Over
        gameOverScreen.SetActive(true);

        // Воспроизводим звук Game Over (если назначен)
        if (gameOverSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        // Останавливаем время
        Time.timeScale = 0;
    }

    public void Restart()
    {
        if (gameOverScreen.activeInHierarchy)
        {
            gameOverScreen.SetActive(false); // Скрыть экран Game Over, если он активен
        }

        Time.timeScale = 1; // Убедитесь, что время восстановлено
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // Возвращаем нормальную скорость времени
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);

        // Приостановка времени при паузе
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion
}
