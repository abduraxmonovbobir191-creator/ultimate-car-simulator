using UnityEngine;
using UnityEngine.SceneManagement;

namespace UltimateCarSimulator
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject missionCompletePanel;
        [SerializeField] private string menuSceneName = "MainMenu";
        public bool IsPaused { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            Time.timeScale = 1f;
            if (pauseMenu != null) pauseMenu.SetActive(false);
            if (missionCompletePanel != null) missionCompletePanel.SetActive(false);
        }

        public void TogglePause()
        {
            IsPaused = !IsPaused;
            Time.timeScale = IsPaused ? 0f : 1f;
            if (pauseMenu != null) pauseMenu.SetActive(IsPaused);
            Cursor.visible = IsPaused;
            Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void Resume()
        {
            if (IsPaused) TogglePause();
        }

        public void CompleteMission()
        {
            if (missionCompletePanel != null) missionCompletePanel.SetActive(true);
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        public void RestartCurrentScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            if (!string.IsNullOrWhiteSpace(menuSceneName)) SceneManager.LoadScene(menuSceneName);
        }

        private void OnDestroy()
        {
            if (Instance == this) Instance = null;
            Time.timeScale = 1f;
        }
    }
}