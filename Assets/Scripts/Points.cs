using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Points : MonoBehaviour
{
    public TextMeshProUGUI textPointsToWin;

    public void Start()
    {
        MenuManager.instance.pointToWin = 5;
    }

    public void ValuePointsToWinChanged()
    {
        MenuManager.instance.pointToWin = int.Parse(textPointsToWin.text);
    }

}
