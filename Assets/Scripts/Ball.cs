using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 100;
    public Vector3 startPos;

    private GameManager gameManager;
    private AudioSource audioSource;
    public AudioClip audioBounce;
    public AudioClip audioScored;

    public float x;
    public float y;

    private Rigidbody2D rb;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {     
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        startPos = transform.position;
        Launch();

        if (MenuManager.isSoundsOn)
            audioSource.mute = false;
        else if (!MenuManager.isSoundsOn)
            audioSource.mute = true;

    }

    public void AddForce(Vector2 force)
    {
        rb.AddForce(force);
    }


    public void Reset()
    {
        speed = 100;
        rb.velocity = Vector3.zero;
        transform.position = startPos;
        Launch();
    }

    public void Launch()
    {
        x = Random.value < 0.5f ? -1.0f : 1.0f;
        y = Random.value < 0.5f ? 0.5f : 0.5f;

        Vector2 direction = new Vector2(x, y);
        rb.velocity = new Vector2(x * speed, y * speed);
    }

    public void LaunchAfterLeftScore()
    {
        x = -1;
        y = Random.value < 0.5f ? -0.5f : 0.5f;

        Vector2 direction = new Vector2(x, y);
        rb.velocity = new Vector2(x * speed, y * speed);
    }

    public void LaunchAfterRightScore()
    {
        x = 1;
        y = Random.value < 0.5f ? -0.5f : 0.5f;

        Vector2 direction = new Vector2(x, y);
        rb.velocity = new Vector2(x * speed, y * speed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlayerLeft")
        {
            audioSource.PlayOneShot(audioBounce);
        }

        if (collision.gameObject.tag == "PlayerRight")
        {
            audioSource.PlayOneShot(audioBounce);
        }

        if (collision.gameObject.tag == "BorderRight")
        {
            audioSource.PlayOneShot(audioScored);
            gameManager.LeftScore();
            Reset();
        }

        if (collision.gameObject.tag == "BorderLeft")
        {
            audioSource.PlayOneShot(audioScored);
            gameManager.RightScore();
            Reset();
        }

        if (collision.gameObject.tag == "Wall")
        {
            audioSource.PlayOneShot(audioBounce);
            GameObject.Find("GameManager").GetComponent<GameManager>().IncreaseScore(1);
        }
        
        if (collision.gameObject.tag == "PlayerWall")
        {
            audioSource.PlayOneShot(audioScored);
            Invoke("GameOverWallGame", 0.2f);
        }

        if (collision.gameObject.tag == "Obstacle")
        {
            audioSource.PlayOneShot(audioBounce);
        }
    }

    private void GameOverWallGame()
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOverWallGame();
    }
}
