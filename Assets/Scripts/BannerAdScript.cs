using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAdScript : MonoBehaviour
{

    public string gameId = "4179263";
    public string placementId = "baner";

    IEnumerator Start()
    {
        Advertisement.Initialize(gameId);


        while (!Advertisement.IsReady(placementId))
            yield return null;

        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
        Advertisement.Banner.Show(placementId);
        Advertisement.Banner.Hide(true);
    }
}