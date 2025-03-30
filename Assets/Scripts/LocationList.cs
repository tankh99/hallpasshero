using UnityEngine;
using System.Collections.Generic;

public class LocationList : MonoBehaviour
{
    private HashSet<string> availableLocations = new HashSet<string>();

    // Default locations that are always available
    private readonly string[] baseLocations = new string[]
    {
        "Bathroom",
        "Nurse's Office",
        "Main Office"
    };

    // Special locations that might be available depending on the day/scenario
    private readonly Dictionary<int, string[]> daySpecificLocations = new Dictionary<int, string[]>
    {
        { 1, new string[] { "Library", "Cafeteria" } },
        { 2, new string[] { "Gym", "Computer Lab" } },
        { 3, new string[] { "Art Room", "Music Room" } }
        // Add more days as needed
    };

    public void SetupLocationsForDay(int dayNumber)
    {
        availableLocations.Clear();
        
        // Add base locations that are always available
        foreach (string location in baseLocations)
        {
            availableLocations.Add(location);
        }

        // Add day-specific locations if they exist
        if (daySpecificLocations.ContainsKey(dayNumber))
        {
            foreach (string location in daySpecificLocations[dayNumber])
            {
                availableLocations.Add(location);
            }
        }
    }

    public bool IsLocationAvailable(string location)
    {
        return availableLocations.Contains(location);
    }

    public string[] GetAvailableLocations()
    {
        return new List<string>(availableLocations).ToArray();
    }
}