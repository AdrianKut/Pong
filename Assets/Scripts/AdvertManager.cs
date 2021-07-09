using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Button))]
public class AdvertManager : MonoBehaviour
{
    string gameId = "4179263";

    void Start()
    {
        Advertisement.Initialize(gameId);
        Advertisement.Show();
    }
}
