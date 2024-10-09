using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int damage = 10; // Урон, наносимый пилой
    public float damageInterval = 1.0f; // Интервал между нанесением урона
    public float knockbackForce = 5.0f; // Сила отталкивания

    private float lastDamageTime; // Время последнего нанесённого урона
    private bool playerInTrap = false; // Флаг, что игрок находится в зоне пилы
    private Damageable player; // Компонент игрока для нанесения урона

    private void Update()
    {
        // Если игрок в зоне и прошло достаточно времени с последнего урона
        if (playerInTrap && player != null && Time.time >= lastDamageTime + damageInterval)
        {
            // Рассчитать направление отталкивания от центра шара
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
            Vector2 knockback = knockbackDirection * knockbackForce; // Рассчитать отталкивание с силой

            player.Hit(damage, knockback); // Наносим урон игроку и отталкиваем
            lastDamageTime = Time.time; // Обновляем время последнего урона

            // Здесь можно добавить дополнительные эффекты (например, тряску камеры)
            CameraShake(); // Вызов эффекта тряски камеры (если будет реализован)
        }
    }

    // Игрок заходит в зону триггера
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Damageable>();

        if (player != null && player.IsAlive)
        {
            playerInTrap = true; // Флаг, что игрок в зоне шара
            lastDamageTime = Time.time - damageInterval; // Наносим урон сразу
        }
    }

    // Игрок выходит из зоны триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Damageable>() == player)
        {
            playerInTrap = false; // Игрок вышел из зоны шара
            player = null;
        }
    }

    // Эффект тряски камеры (можно реализовать по-своему)
    private void CameraShake()
    {
        // Добавь реализацию эффекта тряски камеры, если необходимо
        // Например, можно воспользоваться готовым скриптом CameraShake в интернете
        // или использовать Cinemachine для лёгкой настройки
    }
}
