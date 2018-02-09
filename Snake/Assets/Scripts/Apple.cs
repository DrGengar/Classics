using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    private Vector2Int position;
    private bool isAlive, stopped;
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
        if(!stopped)
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
        float x = 10 * Random.Range(0.0f, 1.6f);
        float y = 10 * Random.Range(0.0f, 1.6f);

        return new Vector2Int((int)x, (int)y);
    }

    void OnGUI()
    {
        Graphics.DrawTexture(new Rect(position.x, -position.y, 1, -1), texture);
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
        stopped = false;
    }

    public void Reset()
    {
        position = NewRandomPosition();
        isAlive = true;
        stopped = false;
    }
}
