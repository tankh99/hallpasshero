using UnityEngine;
using TMPro;

public class LocationListUI : MonoBehaviour
{
    public GameObject locationPanel;
    public TextMeshProUGUI locationListText;
    private LocationList locationList;

    private void Start()
    {
        locationList = FindFirstObjectByType<LocationList>();
        locationPanel.SetActive(false);
    }

    public void ToggleLocationPanel()
    {
        if (!locationPanel.activeSelf)
        {
            UpdateLocationDisplay();
        }
        locationPanel.SetActive(!locationPanel.activeSelf);
    }

    private void UpdateLocationDisplay()
    {
        string[] locations = locationList.GetAvailableLocations();
        locationListText.text = "Available Locations:\n" + string.Join("\n", locations);
    }
} 