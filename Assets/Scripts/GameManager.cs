using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int playersYBounds = 40;
    public GameObject playerRight;
    public GameObject playerLeft;
    public GameObject Ball;
    public GameObject GameOverButton;
    public GameObject[] DifficultyMode;

    public float playerMoveSpeed = 0.15f;
    public float botMoveSpeed;

    public int leftScore = 0;
    public int rightScore = 0;

    public TextMeshProUGUI textLeftScore;
    public TextMeshProUGUI textRightScore;

    public TextMeshProUGUI debug;
    public TextMeshProUGUI middleText;

    public int pointsToWin;
    public bool isPaused = true;
    public bool isGameOver = false;

    public Animator animator;
    public Animation shakingAnimation;

    public Action<int> OnScore;
    private void Awake()
    {
        OnScore += SendAchivementProgress;
        Application.targetFrameRate = 144;
        isPaused = true;
    }

    void Start()
    {
        pointsToWin = MenuManager.instance.pointToWin;
        botMoveSpeed = MenuManager.botMoveSpeed;

        if (!MenuManager.isWallGame)
            ResetPositionBallAndPlayers();

        if (MenuManager.isVsPlayerGame)
        {
            switch (MenuManager.instance.currentDifficulty)
            {
                case Difficulty.Easy:
                    DifficultyMode[0].SetActive(true);
                    break;
                case Difficulty.Medium:
                    DifficultyMode[1].SetActive(true);
                    break;
                case Difficulty.Hard:
                    DifficultyMode[2].SetActive(true);
                    break;
                case Difficulty.Extreme:
                    DifficultyMode[3].SetActive(true);
                    break;
            }
        }
    }

    public void LeftScore()
    {
        leftScore++;
        StartCoroutine(Scored("Left"));
        textLeftScore.text = "" + leftScore;
    }

    public void RightScore()
    {
        rightScore++;
        StartCoroutine(Scored("Right"));
        textRightScore.text = "" + rightScore;

    }

    private void ResetPositionBallAndPlayers()
    {
        Ball.GetComponent<RectTransform>().localPosition = Vector3.zero;
        playerLeft.GetComponent<RectTransform>().localPosition = new Vector3(-600, 0, 0);
        playerRight.GetComponent<RectTransform>().localPosition = new Vector3(600, 0, 0);
    }

    private IEnumerator Scored(string who)
    {
        if (MenuManager.isSoundsOn)
        {
            Handheld.Vibrate();
            animator.SetTrigger("shake");
        }

        var ballRigid2D = Ball.GetComponent<Rigidbody2D>();
        ballRigid2D.constraints = RigidbodyConstraints2D.FreezeAll;

        yield return new WaitForSeconds(0.3f);
        if (who == "Left")
        {
            Ball.GetComponent<Ball>().LaunchAfterLeftScore();
        }
        else
        {
            Ball.GetComponent<Ball>().LaunchAfterRightScore();
        }

        ballRigid2D.constraints = RigidbodyConstraints2D.None;
        ballRigid2D.constraints = RigidbodyConstraints2D.FreezeRotation;

        ResetPositionBallAndPlayers();
        isPaused = true;


        if (leftScore == pointsToWin)
            GameOver("1");
        else if (rightScore == pointsToWin)
            GameOver("2");
    }

    private int backToMenuCounter = 0;
    void Update()
    {

        if (isPaused)
        {
            ResetPositionBallAndPlayers();
            Time.timeScale = 0;
        }

        if (isGameOver)
        {
            isPaused = true;
            Ball.SetActive(false);
        }

        if (Input.anyKey && !(Input.GetKey(KeyCode.Escape)) && !isGameOver)
        {
            middleText.text = "";
            backToMenuCounter = 0;
            isPaused = false;
            Time.timeScale = 1;
        }


        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyUp(KeyCode.Escape) && !isGameOver)
            {
                backToMenuCounter++;
                if (backToMenuCounter == 2)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Time.timeScale = 0;
                    middleText.text = "     PAUSE";
                }
            }
        }

        MovePlayers();
    }

    private void MovePlayers()
    {
        if (MenuManager.isWallGame)
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x >= 200)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        playerRight.transform.position = new Vector3(playerRight.transform.position.x,
                           Mathf.Clamp(playerRight.transform.position.y + touch.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds), playerRight.transform.position.z);
                    }
                }
            }

        }
        if (MenuManager.isVsBotGame)
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x >= 200)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        playerRight.transform.position = new Vector3(playerRight.transform.position.x,
                           Mathf.Clamp(playerRight.transform.position.y + touch.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds), playerRight.transform.position.z);
                    }
                }
            }

            playerLeft.transform.position = new Vector3(playerLeft.transform.position.x,
                Mathf.Clamp(Ball.transform.position.y * botMoveSpeed, -playersYBounds, playersYBounds),
                playerLeft.transform.position.z);
        }
        else if (MenuManager.isVsPlayerGame)
        {
            if (Input.touchCount == 1)
            {
                var touch = Input.GetTouch(0);
                if (touch.position.x >= 1300)
                {

                    if (touch.phase == TouchPhase.Moved)
                    {
                        playerRight.transform.position = new Vector3(playerRight.transform.position.x,
                           Mathf.Clamp(playerRight.transform.position.y + touch.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                            playerRight.transform.position.z);
                    }
                }

                if (touch.position.x <= 1220)
                {
                    if (touch.phase == TouchPhase.Moved)
                    {
                        playerLeft.transform.position = new Vector3(playerLeft.transform.position.x,
                            Mathf.Clamp(playerLeft.transform.position.y + touch.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                             playerLeft.transform.position.z);
                    }
                }
            }
            else if (Input.touchCount > 0)
            {
                var touch_1 = Input.GetTouch(0);
                var touch_2 = Input.GetTouch(1);


                if (touch_1.position.x >= 1300)
                {

                    if (touch_1.phase == TouchPhase.Moved)
                    {
                        playerRight.transform.position = new Vector3(playerRight.transform.position.x,
                           Mathf.Clamp(playerRight.transform.position.y + touch_1.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                            playerRight.transform.position.z);
                    }
                }
                if (touch_1.position.x <= 1220)
                {
                    if (touch_1.phase == TouchPhase.Moved)
                    {
                        playerLeft.transform.position = new Vector3(playerLeft.transform.position.x,
                                 Mathf.Clamp(playerLeft.transform.position.y + touch_1.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                                  playerLeft.transform.position.z);
                    }
                }


                if (touch_2.position.x >= 1300)
                {

                    if (touch_2.phase == TouchPhase.Moved)
                    {
                        playerRight.transform.position = new Vector3(playerRight.transform.position.x,
                            Mathf.Clamp(playerRight.transform.position.y + touch_2.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                             playerRight.transform.position.z);
                    }
                }
                if (touch_2.position.x <= 1220)
                {
                    if (touch_2.phase == TouchPhase.Moved)
                    {
                        playerLeft.transform.position = new Vector3(playerLeft.transform.position.x,
                               Mathf.Clamp(playerLeft.transform.position.y + touch_2.deltaPosition.y * playerMoveSpeed, -playersYBounds, playersYBounds),
                                playerLeft.transform.position.z);
                    }
                }
            }
        }
    }


    public void IncreaseScore(int amount)
    {
        rightScore++;
        OnScore(rightScore);
        textRightScore.text = "" + rightScore;
    }

    private void SendAchivementProgress(int score)
    {
        if (MenuManager.instance.isConnectedToGooglePlayServies)
        {
            switch (score)
            {
                case 10:
                    Social.ReportProgress(GPGSIds.achievement_10_score, 100.0f, null);
                    break;

                case 25:
                    Social.ReportProgress(GPGSIds.achievement_25_score, 100.0f, null);
                    break;

                case 50:
                    Social.ReportProgress(GPGSIds.achievement_50_score, 100.0f, null);
                    break;

                case 100:
                    Social.ReportProgress(GPGSIds.achievement_100_score, 100.0f, null);
                    break;

                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Not signed in .. unable to report score");
        }
    }

    public void GameOverWallGame()
    {
        isGameOver = true;
        GameOverButton.SetActive(true);
        if (MenuManager.isSoundsOn)
        {
            Handheld.Vibrate();
            animator.SetTrigger("shake");
        }
        textRightScore.text = "GAME OVER";

        SendAchivementProgress(rightScore);

        if (MenuManager.instance.isConnectedToGooglePlayServies)
        {
            Social.ReportScore(rightScore, GPGSIds.leaderboard_top_score, (success) =>
            {
                if (!success)
                {
                    Debug.LogError("Unable to post highscore!");
                }
            });
        }
        else
        {
            Debug.Log("Not signed in .. unable to report score");
        }

        if (rightScore > MenuManager.instance.bestScore)
        {        
            middleText.text = "NEW HIGHSCORE!\nYour Score: " + rightScore + "\nBest Score: " + MenuManager.instance.bestScore;
            MenuManager.instance.bestScore = rightScore;
            MenuManager.instance.Save();
        }
        else
        {
            middleText.text = "Your Score: " + rightScore + "\nBest Score: " + MenuManager.instance.bestScore;
        }

    }

    public void GameOver(string whoWin)
    {
        if (MenuManager.isVsPlayerGame)
            for (int i = 0; i < DifficultyMode.Length; i++)
                DifficultyMode[i].SetActive(false);

        isGameOver = true;
        GameOverButton.SetActive(true);
        if (MenuManager.isSoundsOn)
        {
            Handheld.Vibrate();
            animator.SetTrigger("shake");
        }

        middleText.text = "PLAYER " + whoWin + " WINS";
    }

    public void BackToMenu()
    {
        isPaused = false;
        isGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
