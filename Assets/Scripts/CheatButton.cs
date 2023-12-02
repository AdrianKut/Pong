using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheatButton : MonoBehaviour
{
    [SerializeField] private Button UnlockAdsButton = null;
    public bool First = false;
    public bool Second = false;
    public bool Reset = false;

    private void OnEnable()
    {
        BindAction();
    }

    private void OnDestroy()
    {
        UnBindAction();
    }

    private void OnDisable()
    {
        UnBindAction();
    }


    private void BindAction()
    {
        UnlockAdsButton.onClick.AddListener( HandleUnlockAdsButtonPressed );

    }

    private void UnBindAction()
    {
        UnlockAdsButton.onClick.RemoveListener( HandleUnlockAdsButtonPressed );
    }

    private void HandleUnlockAdsButtonPressed()
    {
        if( Reset )
        {
            SaveData.Instance.WinVsBot = false;
            SaveData.Instance.Got50Points = false;
            SaveData.Instance.AdsDisabled = false;
            SaveData.Instance.Save();
            
        }

        if( First )
        {
            SaveData.Instance.WinVsBot = true;
            SaveData.Instance.Save();
        }
        else
        {
            SaveData.Instance.WinVsBot = false;
            SaveData.Instance.Save();
        }

        if( Second )
        {
            SaveData.Instance.Got50Points = true;
            SaveData.Instance.Save();
        }
        else
        {
            SaveData.Instance.Got50Points = false;
            SaveData.Instance.Save();
        }

        SceneManager.LoadScene( 0 );
    }


}
