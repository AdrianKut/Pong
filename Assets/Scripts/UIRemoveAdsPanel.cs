using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRemoveAdsPanel : MonoBehaviour
{
    [SerializeField] private Button CloseButton = null;
    [SerializeField] private Button RemoveAdsButton = null;
    [SerializeField] private TextMeshProUGUI TextHeader = null;
    [SerializeField] private TextMeshProUGUI[] TextDescription = null;
    private SaveData SaveManager = null;

    private void OnEnable()
    {
        SaveManager = SaveData.Instance;
        SaveManager.Load();
        Initialize();
        BindAction();
    }

    private void Initialize()
    {
        RemoveAdsButton.interactable = CanUseRemoveAds();
        UpdateTexts();
    }

    private void UpdateTexts()
    {
        if( SaveManager.AdsDisabled )
        {
            TextHeader.text = "THANKS FOR PLAYING :]\n YOU DON'T HAVE ANY INTERSTITIAL ADS";
            TextHeader.alignment = TextAlignmentOptions.Center;
            TextDescription[0].text = string.Empty;
            TextDescription[1].text = string.Empty;
            RemoveAdsButton.gameObject.SetActive( false );
        }
        else
        {
            UpdateTextDescription( 0, SaveManager.WinVsBot );
            UpdateTextDescription( 1, SaveManager.Got50Points );
        }
    }

    private void UpdateTextDescription( int index, bool condition )
    {
        TextDescription[index].text = TextDescription[index].text
            .Replace( "{0}", condition ? "green" : "red" )
            .Replace( "{1}", condition ? "TRUE" : "FALSE" );
    }

    private void OnDestroy()
    {
        UnBindAction();
    }

    private void OnDisable()
    {
        UnBindAction();
        SaveManager = null;
    }

    private void BindAction()
    {
        CloseButton.onClick.AddListener( HandleCloseButtonPressed );
        RemoveAdsButton.onClick.AddListener( HandleRemoveAdsPressed );
    }

    private void UnBindAction()
    {
        CloseButton.onClick.RemoveListener( HandleCloseButtonPressed );
        RemoveAdsButton.onClick.RemoveListener( HandleRemoveAdsPressed );
    }

    private void HandleCloseButtonPressed()
    {
        gameObject.SetActive( false );
    }

    private void HandleRemoveAdsPressed()
    {
        SaveManager.AdsDisabled = true;
        SaveManager.Save();

        gameObject.SetActive( false );
    }

    private bool CanUseRemoveAds()
    {
        if( SaveManager.Got50Points && SaveManager.WinVsBot )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
