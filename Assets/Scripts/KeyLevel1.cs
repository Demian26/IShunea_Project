using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyLevel1 : MonoBehaviour
{
    public int sceneBuildIndex; // Индекс сцены для перехода

    // Вызывается при входе в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что объект, который вошёл в триггер, имеет тег "Player"
        if (other.CompareTag("Player"))
        {
            print("Switching Scene to " + sceneBuildIndex);
            LoadNextLevel(); // Переход на следующий уровень
        }
    }

    // Метод для загрузки следующей сцены
    private void LoadNextLevel()
    {
        // Можно добавить дополнительную логику здесь, например, сохранение состояния игры или анимации
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
