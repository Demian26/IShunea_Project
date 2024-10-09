using UnityEngine;

public class SawTrapScript : MonoBehaviour
{
    public int damage = 10; // ����, ��������� �����
    public float damageInterval = 1.0f; // �������� ����� ���������� �����
    public float knockbackForce = 5.0f; // ���� ������������
    public float minKnockbackDistance = 0.1f; // ����������� ��������� ��� ������������

    private float lastDamageTime; // ����� ���������� ���������� �����
    private bool playerInTrap = false; // ����, ��� ����� ��������� � ���� ����
    private Damageable player; // ��������� ������ ��� ��������� �����

    private void Update()
    {
        // ���� ����� � ���� � ������ ���������� ������� � ���������� �����
        if (playerInTrap && player != null && Time.time >= lastDamageTime + damageInterval)
        {
            Vector2 knockback = CalculateKnockback(player.transform.position); // ������������ ������������
            player.Hit(damage, knockback); // ������� ���� ������
            lastDamageTime = Time.time; // ��������� ����� ���������� �����
        }
    }

    // ����� ������� � ���� ��������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Damageable>();

        if (player != null && player.IsAlive)
        {
            playerInTrap = true; // ����, ��� ����� � ���� ����
            lastDamageTime = Time.time - damageInterval; // ������� ���� �����
        }
    }

    // ����� ������� �� ���� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Damageable>() == player)
        {
            playerInTrap = false; // ����� ����� �� ���� ����
            player = null;
        }
    }

    // ������� ��� ������� ����������� ������������
    private Vector2 CalculateKnockback(Vector3 playerPosition)
    {
        // ������������ ����������� �� ������ � ������ ����
        Vector2 direction = (playerPosition - transform.position).normalized;

        // ���������, ���� ����� ������� ������ � ������
        if (Vector2.Distance(playerPosition, transform.position) < minKnockbackDistance)
        {
            // ���� ����� � ������, ����������� � ��������� �����������
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        // ����������� ������ � ��������������� ����������� �� ����
        Vector2 knockback = direction * knockbackForce;

        return knockback;
    }
}
