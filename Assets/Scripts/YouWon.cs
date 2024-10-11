using UnityEngine;
using UnityEngine.SceneManagement;

public class YouWon : MonoBehaviour
{
    #region Game Over
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
}
