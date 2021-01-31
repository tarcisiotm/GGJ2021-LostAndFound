using TG.Core;
using UnityEngine;

/// <summary>
/// A Pause menu handler
/// </summary>
public class PauseMenu : MonoBehaviour {
    [SerializeField] GameObject pausePanel = default;

    bool isPaused = false;
    bool canUpdate = true;
    bool isLocked = false;

    public delegate void PauseEvent(bool pauseStatus);
    public static event PauseEvent OnPauseEvent;

    private PlayerControls _controls;

    void Start() { }

    private void Awake()
    {
        _controls = new PlayerControls();
    }

    private void OnEnable()
    {
        _controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        _controls.Gameplay.Disable();
    }

    void Update() {
        if (!canUpdate) { return; }

        if (_controls.Gameplay.Pause.triggered) {
            if (isPaused) { UnpauseGame(); } else { PauseGame(); }
        }
    }

    public void PauseGame() {
        isPaused = true;
        OnPauseEvent?.Invoke(isPaused);
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void UnpauseGame() {
        isPaused = false;
        OnPauseEvent?.Invoke(isPaused);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void DisablePause() {
        canUpdate = false;
    }

    public void LoadMainMenu() {
        Time.timeScale = 1;
        canUpdate = false;
        ScenesManager.I.LoadMainMenu();
    }

    public void ReloadScene() {
        if (isLocked) { return; }
        ScenesManager.I.ReloadScene();
        isLocked = true;
    }

    public void QuitGame() {
        Application.Quit();
    }
}