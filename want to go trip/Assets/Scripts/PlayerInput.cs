
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // �÷��̾��� �Է��� �޴� ��ũ��Ʈ
    public float horizontalMoving { get; private set; } // ������� (x�����)
    public float verticalMoving { get; private set; } // �������� (z�� ����)
    public bool space { get; private set; } // space ��ư �Է� Ȯ�� (������)

    // Update is called once per frame
    void Update()
    {
        horizontalMoving = Input.GetAxis("Horizontal");
        verticalMoving = Input.GetAxis("Vertical");
        space = Input.GetKeyDown(KeyCode.Space);
    }
}
