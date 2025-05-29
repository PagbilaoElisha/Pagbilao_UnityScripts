using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [Header("Orb Counts Per Type")]
    public int fireCount;
    public int earthCount;
    public int airCount;

    [Header("UI Elements")]
    public TextMeshProUGUI fireText;
    public TextMeshProUGUI earthText;
    public TextMeshProUGUI airText;
    public GameObject orbPanel;
    public GameObject winPanel;
    public GameObject losePanel;

    private int totalTotems;
    private int destroyedTotems;

    void Start()
    {
        if (AudioManager.Instance.musicSource.isPlaying) AudioManager.Instance.StopMusic();
        Time.timeScale = 1f;
        totalTotems = GameObject.FindGameObjectsWithTag("Totem").Length;
        destroyedTotems = 0;
        UpdateUI();
        if (SceneManager.GetActiveScene().buildIndex != 1)
            AudioManager.Instance.PlaySFX(AudioManager.Instance.intro);
    }

    public bool HasOrb(OrbType type)
    {
        return type switch
        {
            OrbType.Fire => fireCount > 0,
            OrbType.Earth => earthCount > 0,
            OrbType.Air => airCount > 0,
            _ => false,
        };
    }

    public void ConsumeOrb(OrbType type)
    {
        switch (type)
        {
            case OrbType.Fire: fireCount--; break;
            case OrbType.Earth: earthCount--; break;
            case OrbType.Air: airCount--; break;
        }

        UpdateUI();

        if (!HasAnyOrbLeft())
            Invoke(nameof(ShowLosePanel), 2f); // Allow time for physics to settle
    }

    private bool HasAnyOrbLeft() => fireCount > 0 || earthCount > 0 || airCount > 0;

    private void UpdateUI()
    {
        if (fireText) fireText.text = fireCount.ToString();
        if (earthText) earthText.text = earthCount.ToString();
        if (airText) airText.text = airCount.ToString();
    }

    public void RegisterTotemDestroyed()
    {
        destroyedTotems++;
        if (destroyedTotems >= totalTotems)
            WinLevel();
    }

    private void WinLevel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.win);
        Time.timeScale = 0f;
        orbPanel?.SetActive(false);
        winPanel?.SetActive(true);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        SaveSystem.MarkLevelCompleted(currentIndex);
    }

    private void ShowLosePanel()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.loss);
        Time.timeScale = 0f;
        orbPanel?.SetActive(false);
        losePanel?.SetActive(true);
    }

    public void NextLevel(int level)
    {
        SceneManager.LoadScene("Level" + level);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}