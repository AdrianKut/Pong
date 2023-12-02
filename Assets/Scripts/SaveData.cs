using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveData : Singleton<SaveData>
{
    public bool WinVsBot;
    public bool Got50Points;
    public bool AdsDisabled;

    private void OnEnable()
    {
        Load();
        DontDestroyOnLoad( gameObject );
    }

    [System.Serializable]
    class SaveManager
    {
        public bool WinVsBot;
        public bool Got50Points;
        public bool AdsEnabled;
    }

    public void Save()
    {
        SaveManager data = new SaveManager();
        data.WinVsBot = WinVsBot;
        data.Got50Points = Got50Points;
        data.AdsEnabled = AdsDisabled;

        string json = JsonUtility.ToJson( data );
        BinaryFormatter bFormatter = new BinaryFormatter();

        using( Stream output = File.Create( Application.persistentDataPath + "/saveAdsfile.dat" ) )
        {
            bFormatter.Serialize( output, json );
        }
    }

    public void Load()
    {
        BinaryFormatter bFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/saveAdsfile.dat";

        if( File.Exists( path ) )
        {
            using( Stream input = File.OpenRead( path ) )
            {
                string json = (string)bFormatter.Deserialize( input );
                SaveManager data = JsonUtility.FromJson<SaveManager>( json );
                WinVsBot = data.WinVsBot;
                Got50Points = data.Got50Points;
                AdsDisabled = data.AdsEnabled;
            }
        }

    }
}