using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    [SerializeField] GameObject continueButton, newgameConfirmation, levelSelect, settings;

    void Awake()
    {
        if (PlayerPrefs.GetInt("LastLevelPlayed") == 0)
            continueButton.SetActive(false);
    }

    public void Continue()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("LastLevelPlayed"));
    }

    public void NewGame()
    {
        if (PlayerPrefs.GetInt("HavePlayedBefore") == 1)
            newgameConfirmation.SetActive(true);
        else
            SceneManager.LoadScene(1);
    }

    public void NewGameConfirm()
    {
        PlayerPrefs.SetInt("HavePlayedBefore", 0);
    }

    public void NewGameCancel()
    {
        newgameConfirmation.SetActive(false);
    }

    public void LevelSelect()
    {
        levelSelect.SetActive(!levelSelect.activeSelf);
    }

    public void Settings()
    {
        settings.SetActive(!settings.activeSelf);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
