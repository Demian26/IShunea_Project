using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    // Переменная для проверки, подобрал ли игрок ключ
    private bool hasKey = false;

    // Название сцены для перехода
    public string sceneName = "YouWon";

    // Метод для вызова, когда игрок подбирает ключ
    public void CollectKey()
    {
        hasKey = true;
    }

    // Level move zoned enter, если коллайдер это игрок
    // Переход на другую сцену
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если игрок попадает в триггер и у него есть ключ
        if (other.tag == "Player" && hasKey)
        {
            // Переход на указанную сцену
            print("Switching Scene to " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else if (other.tag == "Player" && !hasKey)
        {
            // Выводим сообщение, если игрок не подобрал ключ
            print("You need a key to enter the next level.");
        }
    }
}
