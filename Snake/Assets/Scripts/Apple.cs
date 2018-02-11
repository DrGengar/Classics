using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Vector2Int position;
    private bool isAlive, stopped, paused;
    public Texture2D texture;

	// Use this for initialization
	void Awake ()
    {
        position = NewRandomPosition();
        isAlive = true;
        stopped = false;
    }
	
	// Update is called once per frame
	void Update()
    {
        if(!stopped && !paused)
        {
            if (!isAlive)
            {
                position = NewRandomPosition();

                isAlive = true;
            }
        }
	}

    private Vector2Int NewRandomPosition()
    {
        float x = 10 * Random.Range(0.0f, 3.1f);
        float y = 10 * Random.Range(0.0f, 3.1f);

        return new Vector2Int((int)x, (int)y);
    }

    void OnGUI()
    {
        Graphics.DrawTexture(new Rect(-53 + position.x, -49 + position.y, 1, 1), texture);
    }

    public Vector2Int GetPosition()
    {
        return position;
    }

    public void Eat()
    {
        isAlive = false;
    }

    public void Stop()
    {
        stopped = true;
    }

    public void Resume()
    {
        paused = false;
    }

    public void Pause()
    {
        paused = true;
    }

    public void Reset()
    {
        position = NewRandomPosition();
        isAlive = true;
        stopped = false;
    }
}
