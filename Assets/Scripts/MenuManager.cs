using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,
    Medium,
    Hard,
}

public class MenuManager : MonoBehaviour
{
    private MenuManager() { }
    public static MenuManager instance;

    public GameObject gameObjectDifficultyLevels;
    public GameObject gameObjectModeButtons;
    public Difficulty currentDifficulty;

    public static bool isVsBotGame = false;
    public static bool isVsPlayerGame = false;
    public static bool isWallGame = false;

    public int bestScore;
    public int pointToWin;
    public static float botMoveSpeed;

    public string gameId = "4179263";
    public string placementId = "baner";

    private void Awake()
    {
        Load();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    IEnumerator Start()
    {
        ShowSoundIcon();
        Advertisement.Initialize(gameId);


        while (!Advertisement.IsReady(placementId))
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);

    }

    private void ShowSoundIcon()
    {
        if (isSoundsOn)
            soundIcon.GetComponent<Image>().sprite = spritesSounds[0];
        else if (!isSoundsOn)
            soundIcon.GetComponent<Image>().sprite = spritesSounds[1];
    }

    public void Open1PlayerGame()
    {
        Advertisement.Banner.Hide();
        isVsBotGame = true;
        isVsPlayerGame = false;
        isWallGame = false;

        gameObjectModeButtons.SetActive(false);
        gameObjectDifficultyLevels.SetActive(true);
    }

    public void Open2PlayerGame()
    {
        Advertisement.Banner.Hide();
        isVsPlayerGame = true;
        isVsBotGame = false;
        isWallGame = false;

        gameObjectModeButtons.SetActive(false);
        gameObjectDifficultyLevels.SetActive(true);
    }

    public void OpenWallGame()
    {
        Advertisement.Banner.Hide();
        isWallGame = true;
        isVsPlayerGame = false;
        isVsBotGame = false;
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }

    public Sprite[] spritesSounds;
    public GameObject soundIcon;

    public static bool isSoundsOn = true;
    public void SoundsOnOff()
    {
        if (isSoundsOn)
            isSoundsOn = false;
        else
            isSoundsOn = true;


        ShowSoundIcon();
    }

    public void StartGame()
    {
        if (isVsBotGame)
            SceneManager.LoadScene(1);
        else if (isVsPlayerGame)
            SceneManager.LoadScene(2);

        //this.gameObject.SetActive(false);
    }

    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }

    public void Save()
    {
        SaveData data = new SaveData();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);
        BinaryFormatter bFormatter = new BinaryFormatter();
        using (Stream output = File.Create(Application.persistentDataPath + "/savefile.dat"))
        {
            bFormatter.Serialize(output, json);
        }
    }

    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savefile.dat";

        if (File.Exists(path))
        {
            using (Stream input = File.OpenRead(path))
            {
                string json = (string)bFormatter.Deserialize(input);
                SaveData data = JsonUtility.FromJson<SaveData>(json);
                bestScore = data.bestScore;
            }
        }
    }

}