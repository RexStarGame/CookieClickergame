using UnityEngine;
using System;

public class BackgroundCalculator : MonoBehaviour
{
    private DateTime lastPlayTime; // Sidste tidspunkt, hvor spillet blev lukket

    private void Start()
    {
        // Vent på, at Game.Instance er initialiseret
        if (Game.Instance == null)
        {
            Debug.LogError("Game.Instance er null! Sørg for, at Game.cs er i scenen og er en singleton.");
            return;
        }

        // Indlæs sidste tidspunkt fra PlayerPrefs
        string lastPlayTimeString = PlayerPrefs.GetString("LastPlayTime", "");
        if (!string.IsNullOrEmpty(lastPlayTimeString))
        {
            lastPlayTime = DateTime.Parse(lastPlayTimeString);
        }
        else
        {
            lastPlayTime = DateTime.Now; // Hvis ingen tid er gemt, brug nuværende tid
        }

        // Beregn hvor meget tid der er gået
        TimeSpan timePassed = DateTime.Now - lastPlayTime;

        // Beregn hvor meget valuta der skal akkumuleres
        float moneyPerSecond = Game.Instance.scoreIncreasedPerSecond;
        float moneyEarned = (float)timePassed.TotalSeconds * moneyPerSecond;

        // Tilføj den akkumulerede valuta til Game.cs
        Game.Instance.currentScore += moneyEarned;

        Debug.Log($"Tid gået: {timePassed.TotalSeconds} sekunder");
        Debug.Log($"Penge akkumuleret: {moneyEarned}");
    }

    private void OnApplicationQuit()
    {
        // Gem nuværende tidspunkt, når spillet lukkes
        PlayerPrefs.SetString("LastPlayTime", DateTime.Now.ToString());
        PlayerPrefs.Save();
    }
} 