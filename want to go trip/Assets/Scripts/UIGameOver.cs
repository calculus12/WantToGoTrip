using UnityEngine;

public class UIGameOver : MonoBehaviour
{
    public void OnClickRestart()
    {
        UIManager.instance.GameRestart();
    }

    public void OnClickExit() 
    {
        UIManager.instance.GoStartMenu();
    }
}
