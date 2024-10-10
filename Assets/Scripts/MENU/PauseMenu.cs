using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip deathSound; // Добавляем звук смерти
    private AudioSource audioSource;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    // Добавим задержку в секундах для Game Over
    [SerializeField] private float delayBeforeGameOver = 3f; // 3 секунды задержки перед появлением экрана Game Over
    [SerializeField] private float delayBeforeMainMenu = 3f; // 3 секунды задержки перед переходом в главное меню
    [SerializeField] private float delayBeforeDeathSound = 1f; // 1 секунда задержки перед звуком смерти

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
        // Останавливаем главную музыку
        FindObjectOfType<MusicPlayer>().StopMusic();

        // Запускаем задержку перед показом экрана Game Over
        Invoke("ShowGameOverScreen", delayBeforeGameOver); // Показать экран Game Over через 3 секунды

        // Запускаем задержку перед воспроизведением звука смерти
        Invoke("PlayDeathSound", delayBeforeDeathSound); // Показать звук смерти через 1 секунду

        // Останавливаем время
        Time.timeScale = 1;

        // Запускаем переход в главное меню через задержку
        Invoke("MainMenu", delayBeforeMainMenu); // Переход в главное меню через 3 секунды
    }

    private void ShowGameOverScreen()
    {
        // Включаем экран Game Over
        gameOverScreen.SetActive(true);
    }

    private void PlayDeathSound()
    {
        // Воспроизводим звук смерти (если назначен)
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
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
        SceneManager.LoadScene(0); // Переход в главное меню
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
