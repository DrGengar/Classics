using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Snake snake;
    Apple apple;
    bool gameOver;
    int score;
    int lifes;
    TextMesh[] texts;
    TextMesh scoreText, lifesText, gameOverText;

	// Use this for initialization
	void Start()
    {
        texts = GetComponentsInChildren<TextMesh>();

        scoreText = texts[0];
        lifesText = texts[1];
        gameOverText = texts[2];

        gameOverText.gameObject.SetActive(false);

        snake = GetComponentInChildren<Snake>();
        apple = GetComponentInChildren<Apple>();
        gameOver = false;
        score = 0;
        lifes = 2;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(!gameOver)
        {
            score += (int)(100 * Time.deltaTime);

            if (snake.GetTailPosition() == apple.GetPosition())
            {
                apple.Eat();
                score += 500;
                snake.AddSegment();
            }

            if (snake.HeadInBody() || InWall(snake.GetHeadPosition()))
            {
                lifes--;
                snake.Reset();
                apple.Reset();
            }

            if(lifes <= 0)
            {
                gameOverText.gameObject.SetActive(true);
                gameOverText.text = "Game Over!\n Endscore: " + score;
                gameOver = true;
                snake.Stop();
                apple.Stop();
            }
        }

        scoreText.text = "Score: " + score;
        lifesText.text = "Lifes: " + lifes;
	}

    private bool InWall(Vector2Int position)
    {
        if(position.x < 0 || position.y < 0 || position.x > 32 || position.y > 32)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
