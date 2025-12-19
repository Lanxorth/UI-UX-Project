using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stove : MonoBehaviour
{
    [Header("UI")]
    public Canvas stoveCanvas;
    public Slider tempSlider;
    public TMP_Text tempText;

    [Header("Settings")]
    public float minTemp = 50f;
    public float maxTemp = 250f;
    public float activationDistance = 3f;

    [Header("Text Colors")]
    public Color coldColor = Color.blue;
    public Color mediumColor = Color.yellow;
    public Color hotColor = Color.red;

    private Transform player;
    public float CurrentTemperature { get; private set; }

    void Start()
    {
        if (stoveCanvas != null)
            stoveCanvas.gameObject.SetActive(false);

        CurrentTemperature = minTemp;

        tempSlider.minValue = minTemp;
        tempSlider.maxValue = maxTemp;
        tempSlider.value = CurrentTemperature;

        tempSlider.onValueChanged.AddListener(OnSliderChanged);

        player = Camera.main.transform; // ou le Player
        UpdateTextColor();
    }

    void Update()
    {
        CheckProximity();
    }

    void CheckProximity()
    {
        if (player == null) return;

        float distance = Vector3.Distance(player.position, transform.position);

        if (distance <= activationDistance)
        {
            stoveCanvas.gameObject.SetActive(true);
        }
        else
        {
            stoveCanvas.gameObject.SetActive(false);
        }
    }

    void OnSliderChanged(float value)
    {
        CurrentTemperature = value;

        if (tempText != null)
        {
            tempText.text = $"{CurrentTemperature:0} °C";
            UpdateTextColor();
        }
    }

    void UpdateTextColor()
    {
        if (tempText == null) return;

        float t = (CurrentTemperature - minTemp) / (maxTemp - minTemp);

        if (t < 0.5f)
        {
            tempText.color = Color.Lerp(coldColor, mediumColor, t * 2f);
        }
        else
        {
            tempText.color = Color.Lerp(mediumColor, hotColor, (t - 0.5f) * 2f);
        }
    }
}
