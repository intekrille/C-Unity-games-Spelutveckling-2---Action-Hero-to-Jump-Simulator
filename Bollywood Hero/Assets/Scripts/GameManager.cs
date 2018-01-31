using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    float time;
    Scene scene;

    [SerializeField] GameObject finishScreen;

	void Awake () {
        instance = this;
        Time.timeScale = 1;
        scene = SceneManager.GetActiveScene();
        if (scene.buildIndex > 0)
            LevelStart();
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void LevelStart()
    {
        PlayerPrefs.SetInt("LastLevelPlayed", scene.buildIndex);
    }

    public void LevelCompleted()
    {
        Time.timeScale = 0;
        PlayerPrefs.SetFloat("Level" + scene.buildIndex + "Time", time);
        FinishScreen();
    }

    IEnumerator FinishScreen()
    {
        yield return new WaitForSeconds(3f);
        finishScreen.SetActive(true);
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
}
