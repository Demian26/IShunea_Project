using UnityEngine;

public class BallScript : MonoBehaviour
{
    public int damage = 10; // ����, ��������� �����
    public float damageInterval = 1.0f; // �������� ����� ���������� �����
    public float knockbackForce = 5.0f; // ���� ������������

    private float lastDamageTime; // ����� ���������� ���������� �����
    private bool playerInTrap = false; // ����, ��� ����� ��������� � ���� ����
    private Damageable player; // ��������� ������ ��� ��������� �����

    private void Update()
    {
        // ���� ����� � ���� � ������ ���������� ������� � ���������� �����
        if (playerInTrap && player != null && Time.time >= lastDamageTime + damageInterval)
        {
            // ���������� ����������� ������������ �� ������ ����
            Vector2 knockbackDirection = (player.transform.position - transform.position).normalized;
            Vector2 knockback = knockbackDirection * knockbackForce; // ���������� ������������ � �����

            player.Hit(damage, knockback); // ������� ���� ������ � �����������
            lastDamageTime = Time.time; // ��������� ����� ���������� �����

            // ����� ����� �������� �������������� ������� (��������, ������ ������)
            CameraShake(); // ����� ������� ������ ������ (���� ����� ����������)
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

    // ������ ������ ������ (����� ����������� ��-������)
    private void CameraShake()
    {
        // ������ ���������� ������� ������ ������, ���� ����������
        // ��������, ����� ��������������� ������� �������� CameraShake � ���������
        // ��� ������������ Cinemachine ��� ����� ���������
    }
}
