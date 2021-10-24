
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 플레이어의 입력을 받는 스크립트
    public float horizontalMoving { get; private set; } // 수평방향 (x축방향)
    public float verticalMoving { get; private set; } // 수직방향 (z축 방향)
    public bool space { get; private set; } // space 버튼 입력 확인 (도끼질)

    // Update is called once per frame
    void Update()
    {
        horizontalMoving = Input.GetAxis("Horizontal");
        verticalMoving = Input.GetAxis("Vertical");
        space = Input.GetKeyDown(KeyCode.Space);
    }
}
