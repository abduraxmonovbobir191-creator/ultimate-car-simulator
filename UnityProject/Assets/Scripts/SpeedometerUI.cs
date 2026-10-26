using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Optional speed display. Assign a UI Text component and the car controller.
/// Use a legacy Unity UI Text object for this starter version.
/// </summary>
public class SpeedometerUI : MonoBehaviour
{
    [SerializeField] private CarController car;
    [SerializeField] private Text speedText;

    private void Update()
    {
        if (car == null || speedText == null) return;
        speedText.text = Mathf.RoundToInt(car.SpeedKph) + " km/h";
    }
}
