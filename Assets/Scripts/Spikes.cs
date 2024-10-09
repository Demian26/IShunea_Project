using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int damage = 10; // Урон, наносимый шипами
    public float damageInterval = 1.0f; // Интервал между нанесением урона
    public float knockbackForce = 5.0f; // Сила отбрасывания

    private float lastDamageTime; // Время последнего нанесённого урона
    private bool playerInTrap = false; // Флаг, что игрок находится в зоне шипов
    private Damageable player; // Компонент игрока для нанесения урона

    private void Update()
    {
        // Если игрок в зоне шипов и прошло достаточно времени с последнего урона
        if (playerInTrap && player != null && Time.time >= lastDamageTime + damageInterval)
        {
            Vector2 knockback = CalculateKnockback(); // Рассчитываем отбрасывание
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
            playerInTrap = true; // Флаг, что игрок в зоне шипов
            lastDamageTime = Time.time - damageInterval; // Наносим урон сразу
        }
    }

    // Игрок выходит из зоны триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Damageable>() == player)
        {
            playerInTrap = false; // Игрок вышел из зоны шипов
            player = null;
        }
    }

    // Функция для расчёта направления отбрасывания
    private Vector2 CalculateKnockback()
    {
        // Рассчитываем направление от игрока к шипам
        Vector2 direction = (player.transform.position - transform.position).normalized;

        // Отбрасываем игрока в противоположном направлении от шипов
        Vector2 knockback = direction * knockbackForce;

        return knockback;
    }
}
