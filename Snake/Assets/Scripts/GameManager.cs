using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Snake snake;
    Apple apple;
    bool gameOver, paused;
    int score;
    int lifes;
    public Text scoreText, lifesText, gameOverText, pausedText;
    float timer;
    private AudioSource sourceOneShot;
    public AudioClip borderCrash, snakeCrash, pickUp, bgMusic;
    private float volume = 1.0f;

    // Use this for initialization
    void Start()
    {
        timer = Time.deltaTime;

        snake = GetComponentInChildren<Snake>();
        apple = GetComponentInChildren<Apple>();
        gameOver = false;
        score = 0;
        lifes = 2;

        scoreText.text = "Score: " + score;
        lifesText.text = "Lifes: " + lifes;

        sourceOneShot = GetComponent<AudioSource>();

        gameOverText.enabled = false;
        pausedText.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown("space") && !gameOver)
        {
            if(paused)
            {
                paused = false;
                pausedText.enabled = false;
                snake.Resume();
                apple.Resume();
            }
            else
            {
                paused = true;
                pausedText.enabled = true;
                snake.Pause();
                apple.Pause();
            }
        }

        if(!gameOver && !paused)
        {
            timer += Time.deltaTime;
            if(timer >= 1.0f)
            {
                score++;
                timer = 0;
            }


            if (snake.GetHeadPosition() == apple.GetPosition())
            {
                apple.Eat();
                sourceOneShot.PlayOneShot(pickUp, volume);
                score += 500;
                snake.AddSegment();
            }

            if (snake.HeadInBody())
            {
                lifes--;
                sourceOneShot.PlayOneShot(snakeCrash, volume);
                snake.Reset();
                apple.Reset();
            }

            if (InWall(snake.GetHeadPosition()))
            {
                lifes--;
                sourceOneShot.PlayOneShot(borderCrash, volume);
                snake.Reset();
                apple.Reset();
            }

            if (lifes <= 0)
            {
                gameOver = true;
                snake.Stop();
                apple.Stop();
            }
        }

        if(gameOver)
        {
            paused = false;
            gameOverText.enabled = true;
        }

        scoreText.text = "Score: " + score;
        lifesText.text = "Lifes: " + lifes;
    }

    private bool InWall(Vector2Int position)
    {
        if(position.x < 0 || position.y < 0 || position.x > 31 || position.y > 31)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
