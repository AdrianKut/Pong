using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdvertManager : MonoBehaviour
{
    string gameId = "4179263";
    string intersititalAd = "Android_Interstitial";

    private void OnEnable()
    {
        Advertisement.Initialize(gameId);
        Advertisement.Load(intersititalAd);
    }

    public void ShowInterstitialAd()
    {
        if( SaveData.Instance.AdsDisabled )
        {
            return;
        }

        Advertisement.Show(intersititalAd);
    }
}
