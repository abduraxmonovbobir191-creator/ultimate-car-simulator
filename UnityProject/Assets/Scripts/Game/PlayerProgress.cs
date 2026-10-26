using UnityEngine;

namespace UltimateCarSimulator
{
    /// <summary>Small local progression store; versioned keys make later save upgrades safer.</summary>
    public static class PlayerProgress
    {
        private const string CreditsKey = "ucs_v1_credits";
        private const string MissionsKey = "ucs_v1_missions";
        private const string BestSpeedKey = "ucs_v1_best_speed";

        public static int Credits => PlayerPrefs.GetInt(CreditsKey, 0);
        public static int CompletedMissions => PlayerPrefs.GetInt(MissionsKey, 0);
        public static float BestSpeedKph => PlayerPrefs.GetFloat(BestSpeedKey, 0f);

        public static void AddCredits(int amount)
        {
            if (amount <= 0) return;
            PlayerPrefs.SetInt(CreditsKey, Credits + amount);
            PlayerPrefs.Save();
        }

        public static void RecordMissionCompleted(int rewardCredits)
        {
            PlayerPrefs.SetInt(MissionsKey, CompletedMissions + 1);
            AddCredits(rewardCredits);
            PlayerPrefs.Save();
        }

        public static void RecordSpeed(float speedKph)
        {
            if (speedKph <= BestSpeedKph) return;
            PlayerPrefs.SetFloat(BestSpeedKey, speedKph);
            PlayerPrefs.Save();
        }

        public static void ResetProgress()
        {
            PlayerPrefs.DeleteKey(CreditsKey);
            PlayerPrefs.DeleteKey(MissionsKey);
            PlayerPrefs.DeleteKey(BestSpeedKey);
            PlayerPrefs.Save();
        }
    }
}