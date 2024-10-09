using UnityEngine;

public class Elevator : MonoBehaviour
{
    public float speed = 2.0f;
    public float topPoint = 3.0f; // �����
    public float bottomPoint = 0.0f; // ����
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = new Vector3(transform.position.x, topPoint, transform.position.z); // ��������� ���������
    }

    private void FixedUpdate()
    {
        // ������ �������� � ����
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.fixedDeltaTime);

        // ���� �������� ����, ������ �����������
        if (transform.position.y >= topPoint)
        {
            targetPosition = new Vector3(transform.position.x, bottomPoint, transform.position.z);
        }
        else if (transform.position.y <= bottomPoint)
        {
            targetPosition = new Vector3(transform.position.x, topPoint, transform.position.z);
        }
    }
}
