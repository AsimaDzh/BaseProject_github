using UnityEngine;

public enum GameState
{
    MainMenu = 0,
    Playing = 1,
    Paused = 2
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}

