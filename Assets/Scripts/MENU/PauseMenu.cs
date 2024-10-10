using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioClip deathSound; // ��������� ���� ������
    private AudioSource audioSource;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    // ������� �������� � �������� ��� Game Over
    [SerializeField] private float delayBeforeGameOver = 3f; // 3 ������� �������� ����� ���������� ������ Game Over
    [SerializeField] private float delayBeforeMainMenu = 3f; // 3 ������� �������� ����� ��������� � ������� ����
    [SerializeField] private float delayBeforeDeathSound = 1f; // 1 ������� �������� ����� ������ ������

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        // �������� ��������� AudioSource ��� ��������������� ������
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
        // ������������� ������� ������
        FindObjectOfType<MusicPlayer>().StopMusic();

        // ��������� �������� ����� ������� ������ Game Over
        Invoke("ShowGameOverScreen", delayBeforeGameOver); // �������� ����� Game Over ����� 3 �������

        // ��������� �������� ����� ���������������� ����� ������
        Invoke("PlayDeathSound", delayBeforeDeathSound); // �������� ���� ������ ����� 1 �������

        // ������������� �����
        Time.timeScale = 1;

        // ��������� ������� � ������� ���� ����� ��������
        Invoke("MainMenu", delayBeforeMainMenu); // ������� � ������� ���� ����� 3 �������
    }

    private void ShowGameOverScreen()
    {
        // �������� ����� Game Over
        gameOverScreen.SetActive(true);
    }

    private void PlayDeathSound()
    {
        // ������������� ���� ������ (���� ��������)
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }

    public void Restart()
    {
        if (gameOverScreen.activeInHierarchy)
        {
            gameOverScreen.SetActive(false); // ������ ����� Game Over, ���� �� �������
        }

        Time.timeScale = 1; // ���������, ��� ����� �������������
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1; // ���������� ���������� �������� �������
        SceneManager.LoadScene(0); // ������� � ������� ����
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

        // ������������ ������� ��� �����
        if (status)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
    #endregion
}
