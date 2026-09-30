using UnityEngine;

/// <summary>
/// Game system responsible for pausing/resuming the game.
/// Subscribed to by PlayerController (or directly to InputManager) to react to pause requests.
/// </summary>
public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pauseMenu;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void TogglePause()
    {
        IsPaused = !IsPaused;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(IsPaused);
        }

        Time.timeScale = IsPaused ? 0f : 1f;
    }
}
