using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficulty : MonoBehaviour
{
    public void SetEasyMode()
    {
        MenuManager.instance.currentDifficulty = Difficulty.Easy;
        MenuManager.botMoveSpeed = 0.6f;
    }

    public void SetMediumMode()
    {
        MenuManager.instance.currentDifficulty = Difficulty.Medium;
        MenuManager.botMoveSpeed = 0.8f;
    }

    public void SetHardMode()
    {
        MenuManager.instance.currentDifficulty = Difficulty.Hard;
        MenuManager.botMoveSpeed = 1f;
    }

    public void SetHardExtreme()
    {
        MenuManager.instance.currentDifficulty = Difficulty.Extreme;
        MenuManager.botMoveSpeed = 1.4f;
    }
}
