using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    // 카메라 움직임 스크립트

    public GameObject player; // 카메라가 따라갈 플레이어 오브젝트
    public float leftEnd = -12f; // 카메라의 왼쪽 끝
    public float rightEnd = 12f; // 카메라의 오른쪽 끝
    public float forwardEnd = 10f;
    public float backwardEnd = -20f; // 카메라의 뒤쪽 끝;
    private float initialCameraZ;

    public float minFOV = 24f; // 최대 줌
    public float maxFOV = 55f; // 최소 줌
    public float damping = 5f;

    private float distance;

    private Camera mainCamera;

    public float scrollSensitivity = 10f; // 스크롤 속도
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
        float playerX = player.transform.position.x; // 플레이어의 x축 위치
        float playerZ = player.transform.position.z; // 플레어의 z축 위치
        if (playerX >= leftEnd && playerX <= rightEnd) // 카메라의 x축 위치는 leftEnd과 rightEnd사이어야 함
        {
            Vector3 temp = transform.position;
            temp.x = playerX;
            transform.position = temp; // 카메라가 플레이의 x축 방향을 따라감
        }
        if (playerZ >= backwardEnd && playerZ <= forwardEnd) // 카메라의 z축 위치는 backwardEnd과 forwardEnd사이어야 함
        {
            Vector3 temp = transform.position;
            temp.z = playerZ + initialCameraZ;
            transform.position = temp; // 카메라가 플레이의 z축 방향을 따라감
        }


        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        distance -= mouseWheelInput * scrollSensitivity;
        distance = Mathf.Clamp(distance, minFOV, maxFOV);
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, distance, Time.deltaTime * damping);
    }
}
