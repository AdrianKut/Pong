using UnityEngine;
using UnityEngine.UI;

public class RemoveAds : MonoBehaviour
{
    [SerializeField] private GameObject AdsPrefab;
    [SerializeField] private Button ButtonAds;
    
    private void OnEnable()
    {
        ButtonAds.onClick.AddListener( HandleButtonAdsClicked );
    }

    private void OnDisable()
    {
        ButtonAds.onClick.RemoveListener( HandleButtonAdsClicked );
    }

    private void HandleButtonAdsClicked()
    {
        AdsPrefab.gameObject.SetActive( true );
    }
}
