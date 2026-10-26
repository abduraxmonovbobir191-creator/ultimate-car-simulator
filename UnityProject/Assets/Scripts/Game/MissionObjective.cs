using UnityEngine;
using UnityEngine.Events;

namespace UltimateCarSimulator
{
    public sealed class MissionObjective : MonoBehaviour
    {
        [SerializeField] private string objectiveTitle = "Park the car";
        [SerializeField, Min(0)] private int creditReward = 100;
        [SerializeField] private UnityEvent onObjectiveStarted;
        [SerializeField] private UnityEvent onObjectiveCompleted;
        private bool started;
        private bool completed;

        public string ObjectiveTitle => objectiveTitle;
        public bool IsCompleted => completed;

        private void Start()
        {
            if (started) return;
            started = true;
            onObjectiveStarted?.Invoke();
        }

        public void Complete()
        {
            if (completed) return;
            completed = true;
            PlayerProgress.RecordMissionCompleted(creditReward);
            onObjectiveCompleted?.Invoke();
            if (GameManager.Instance != null) GameManager.Instance.CompleteMission();
        }
    }
}