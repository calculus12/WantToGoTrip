using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // ī�޶� ������ ��ũ��Ʈ

    public GameObject player; // ī�޶� ���� �÷��̾� ������Ʈ
    public float leftEnd = -12f; // ī�޶��� ���� ��
    public float rightEnd = 12f; // ī�޶��� ������ ��
    public float forwardEnd = 10f;
    public float backwardEnd = -20f; // ī�޶��� ���� ��;
    private float initialCameraZ;

    public float minFOV = 24f; // �ִ� ��
    public float maxFOV = 55f; // �ּ� ��
    public float damping = 5f;

    private float distance;

    private Camera mainCamera;

    public float scrollSensitivity = 10f; // ��ũ�� �ӵ�
    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.fieldOfView = 24f;
        distance = mainCamera.fieldOfView;
        initialCameraZ = mainCamera.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x; // �÷��̾��� x�� ��ġ
        float playerZ = player.transform.position.z; // �÷����� z�� ��ġ
        if (playerX >= leftEnd && playerX <= rightEnd) // ī�޶��� x�� ��ġ�� leftEnd�� rightEnd���̾�� ��
        {
            Vector3 temp = transform.position;
            temp.x = playerX;
            transform.position = temp; // ī�޶� �÷����� x�� ������ ����
        }
        if (playerZ >= backwardEnd && playerZ <= forwardEnd) // ī�޶��� z�� ��ġ�� backwardEnd�� forwardEnd���̾�� ��
        {
            Vector3 temp = transform.position;
            temp.z = playerZ + initialCameraZ;
            transform.position = temp; // ī�޶� �÷����� z�� ������ ����
        }


        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= mouseWheelInput * scrollSensitivity;
        distance = Mathf.Clamp(distance, minFOV, maxFOV);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, distance, Time.deltaTime * damping);
    }
}
