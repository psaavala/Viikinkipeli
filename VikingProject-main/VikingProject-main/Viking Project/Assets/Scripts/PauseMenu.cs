using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsPanel;

    void Update()
    {
        // Press Escape or P to toggle pause
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Unpause game
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Pause game
        GameIsPaused = true;
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pauseMenuUI.SetActive(false);
    }

    public void BackFromSettings()
    {
        settingsPanel.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.sceneLoaded += OnSceneLoaded; // tilaa eventtiin
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Etsi uusi pelaaja ja kytke kamera siihen
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CinemachineVirtualCamera cam = FindObjectOfType<CinemachineVirtualCamera>();

        if (cam != null && player != null)
        {
            cam.Follow = player.transform;
            cam.LookAt = player.transform;
        }

        // poista eventti, ettei se kerry joka restartilla
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
