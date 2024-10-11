using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour
{
    // ���������� ��� ��������, �������� �� ����� ����
    private bool hasKey = false;

    // �������� ����� ��� ��������
    public string sceneName = "YouWon";

    // ����� ��� ������, ����� ����� ��������� ����
    public void CollectKey()
    {
        hasKey = true;
    }

    // Level move zoned enter, ���� ��������� ��� �����
    // ������� �� ������ �����
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� ����� �������� � ������� � � ���� ���� ����
        if (other.tag == "Player" && hasKey)
        {
            // ������� �� ��������� �����
            print("Switching Scene to " + sceneName);
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        else if (other.tag == "Player" && !hasKey)
        {
            // ������� ���������, ���� ����� �� �������� ����
            print("You need a key to enter the next level.");
        }
    }
}
