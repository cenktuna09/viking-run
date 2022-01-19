using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField]
    public List<Level> levels;
    public Level activeLevel;
    public Transform levelHolder;
    public bool isLevelRestarted = false;
    public int activeLevelID;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        levelHolder = transform.GetChild(0).transform;
        Application.targetFrameRate = 120;
        LoadLevel(GetLevelID());
    }

    public static void SetLevelID(int levelID)
    {
        PlayerPrefs.SetInt("levelIndex", levelID);
    }
    public static void SetTotalID(int totalID)
    {
        PlayerPrefs.SetInt("totalIndex", totalID);
    }

    public static int GetLevelID()
    {
        int levelID = PlayerPrefs.GetInt("levelIndex", 0);

        if (levelID >= instance.levels.Count)
        {
            levelID = 0;

        }

        return levelID;
    }

    public static int GetLevelTotalID()
    {
        int totalID = PlayerPrefs.GetInt("totalIndex", 1);

        return totalID;
    }

    public static void LoadNextLevel()
    {
        int levelID = GetLevelID() + 1;
        int totalID = GetLevelTotalID() + 1;
        if (levelID >= instance.levels.Count)
        {
            levelID = 0;
            instance.isLevelRestarted = true;
        }

        SetLevelID(levelID);
        SetTotalID(totalID);
        instance.LoadLevel(levelID);
        instance.LogAchieveLevelEvent(levelID.ToString());
        Debug.Log(totalID);

    }

    public void LoadLevel(int index)
    {
        this.RemoveLevel();
        Level currentLevel = Instantiate(levels[index], levelHolder.transform);
        this.activeLevel = currentLevel;
        activeLevelID = index;

        CanvasManager.instance.guide.SetActive(true);

        CanvasManager.instance.levelTxt.SetText((GetLevelID() + 1).ToString());
    }

    public void RestartCurrentLevel()
    {
        //LoadLevel(1);
        LoadLevel(activeLevelID);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RemoveLevel()
    {

        if (GetActiveLevel() != null)
            Destroy(GetActiveLevel().gameObject);
    }
    public Level GetActiveLevel()
    {
        if (levelHolder.childCount > 0)
            return levelHolder.GetChild(0).GetComponent<Level>();
        else
            return null;


    }

    void OnApplicationPause(bool pauseStatus)
    {
        
         
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() =>
                {
                    FB.ActivateApp();
                });
            }
        }
        
    }

    public void LogAchieveLevelEvent(string level)
    {
        /*
        var parameters = new Dictionary<string, object>();
        parameters[AppEventParameterName.Level] = level;
        FB.LogAppEvent(
            AppEventName.AchievedLevel, null,
            parameters
        ); */
    }

    public void LogLevelFailEvent(int level)
    {
        /*
        var parameters = new Dictionary<string, object>();
        parameters["Level"] = level;
        FB.LogAppEvent(
            "LevelFail", null,
            parameters
        );
        */
    }

    public void LogRestartEvent(int level)
    {
        /*
        var parameters = new Dictionary<string, object>();
        parameters["Level"] = level;
        FB.LogAppEvent(
            "Restart", null,
            parameters
        );
        */
    }

    IEnumerator activeButtonAfterSec(float duration)
    {
        yield return new WaitForSeconds(duration);
        RestartCurrentLevel();
    }
}
