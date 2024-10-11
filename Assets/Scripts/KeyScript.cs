using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyScript : MonoBehaviour
{
    // ���� ����� ���������� ��� ����� � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� � ������� ����� �����
        if (other.CompareTag("Player"))
        {
            // ���������� ������ ������ � ���, ��� ���� ��� ��������
            FindObjectOfType<LevelMove_Ref>().CollectKey();

            // ������� ���� � ������ ����� �������
            Destroy(gameObject);

            // ������� �� ����� YouWon
            SceneManager.LoadScene("YouWon");
        }
    }
}
