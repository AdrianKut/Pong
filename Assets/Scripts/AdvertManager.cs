using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro.EditorUtilities;

[RequireComponent(typeof(Button))]
public class AdvertManager : MonoBehaviour
{
    string gameId = "4179263";
    string intersititalAd = "Android_Interstitial";
    void Start()
    {
        Advertisement.Initialize(gameId);
    }

    public void ShowInterstitialAd()
    {
        Advertisement.Show(intersititalAd);
    }
}
