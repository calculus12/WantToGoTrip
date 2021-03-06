using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance; // singletone variable

    public static GameManager instance // singletone property
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private void Awake() // if there is another GameManger, then destroy this
    {
        if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private float score = 0;
    public bool isGameover { get; private set; }

    private void Start()
    {
        FindObjectOfType<RaftHealth>().onDeath += EndGame;
        FindObjectOfType<PlayerHealth>().onDeath += EndGame;
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.SetActiveGameoverUI();
    }
}
