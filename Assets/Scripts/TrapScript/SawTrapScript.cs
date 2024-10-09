using UnityEngine;

public class SawTrapScript : MonoBehaviour
{
    public int damage = 10; // Урон, наносимый пилой
    public float damageInterval = 1.0f; // Интервал между нанесением урона
    public float knockbackForce = 5.0f; // Сила отбрасывания
    public float minKnockbackDistance = 0.1f; // Минимальная дистанция для отбрасывания

    private float lastDamageTime; // Время последнего нанесённого урона
    private bool playerInTrap = false; // Флаг, что игрок находится в зоне пилы
    private Damageable player; // Компонент игрока для нанесения урона

    private void Update()
    {
        // Если игрок в зоне и прошло достаточно времени с последнего урона
        if (playerInTrap && player != null && Time.time >= lastDamageTime + damageInterval)
        {
            Vector2 knockback = CalculateKnockback(player.transform.position); // Рассчитываем отбрасывание
            player.Hit(damage, knockback); // Наносим урон игроку
            lastDamageTime = Time.time; // Обновляем время последнего урона
        }
    }

    // Игрок заходит в зону триггера
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Damageable>();

        if (player != null && player.IsAlive)
        {
            playerInTrap = true; // Флаг, что игрок в зоне пилы
            lastDamageTime = Time.time - damageInterval; // Наносим урон сразу
        }
    }

    // Игрок выходит из зоны триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Damageable>() == player)
        {
            playerInTrap = false; // Игрок вышел из зоны пилы
            player = null;
        }
    }

    // Функция для расчёта направления отбрасывания
    private Vector2 CalculateKnockback(Vector3 playerPosition)
    {
        // Рассчитываем направление от игрока к центру пилы
        Vector2 direction = (playerPosition - transform.position).normalized;

        // Проверяем, если игрок слишком близко к центру
        if (Vector2.Distance(playerPosition, transform.position) < minKnockbackDistance)
        {
            // Если игрок в центре, отбрасываем в случайном направлении
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        // Отбрасываем игрока в противоположном направлении от пилы
        Vector2 knockback = direction * knockbackForce;

        return knockback;
    }
}
