using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyLevel1 : MonoBehaviour
{
    public int sceneBuildIndex; // ������ ����� ��� ��������

    // ���������� ��� ����� � �������
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���������, ��� ������, ������� ����� � �������, ����� ��� "Player"
        if (other.CompareTag("Player"))
        {
            print("Switching Scene to " + sceneBuildIndex);
            LoadNextLevel(); // ������� �� ��������� �������
        }
    }

    // ����� ��� �������� ��������� �����
    private void LoadNextLevel()
    {
        // ����� �������� �������������� ������ �����, ��������, ���������� ��������� ���� ��� ��������
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }
}
