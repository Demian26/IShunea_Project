using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{
    // Этот метод вызывается при входе в триггер
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Проверяем, что в триггер вошёл игрок
        if (other.CompareTag("Player"))
        {
            // Уведомляем скрипт уровня о том, что ключ был подобран
            FindObjectOfType<LevelMove_Ref>().CollectKey();

            // Удаляем ключ с уровня после подбора
            Destroy(gameObject);

            // Переход на сцену YouWon
            SceneManager.LoadScene("YouWon");
        }
    }
}
