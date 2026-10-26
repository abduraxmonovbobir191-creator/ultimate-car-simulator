using UnityEngine;
using UnityEngine.UI;

namespace UltimateCarSimulator
{
    public sealed class DrivingHUD : MonoBehaviour
    {
        [SerializeField] private VehicleController vehicle;
        [SerializeField] private Text speedText;
        [SerializeField] private Text gearText;
        [SerializeField] private Image throttleFill;
        [SerializeField] private GameObject pausePanel;
        private bool paused;

        private void Update()
        {
            if (vehicle != null && speedText != null)
                speedText.text = Mathf.RoundToInt(vehicle.SpeedKph).ToString("000") + " km/h";
            if (vehicle != null && gearText != null)
            {
                float speed = vehicle.SignedSpeedKph;
                gearText.text = Mathf.Abs(speed) < 1f ? "N" : speed < 0f ? "R" : "D";
            }
            if (vehicle != null && throttleFill != null)
                throttleFill.fillAmount = Mathf.Abs(vehicle.NormalizedThrottle);
            if (Input.GetKeyDown(KeyCode.Escape)) TogglePause();
        }

        public void TogglePause()
        {
            paused = !paused;
            Time.timeScale = paused ? 0f : 1f;
            if (pausePanel != null) pausePanel.SetActive(paused);
            Cursor.visible = paused;
            Cursor.lockState = paused ? CursorLockMode.None : CursorLockMode.Locked;
        }

        public void Resume()
        {
            if (paused) TogglePause();
        }

        private void OnDestroy() => Time.timeScale = 1f;
    }
}