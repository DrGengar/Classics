using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    //list of segments
    private List<Segment> segments;

    //standart texture of segments
    public Texture2D segmentTexture;

    //(old-)direction that snake moves (moved)
    private Direction dir, oldDir;

    //timer for movement
    private double timer;

    //should we add a segment; is the game stopped?
    private bool addSegment, stopped, paused;

    //how many pixel moves in a second
    public double speed;

    //with how many segment the snake starts
    public int numberSegments;

    public enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        NONE,
        STOP,
    }

	// Use this for initialization
	void Awake ()
    {
        dir = oldDir = Direction.UP;
        timer = 1/speed;
        addSegment = false;
        stopped = false;

        numberSegments = 3;
        segments = new List<Segment>();
        segments.Add(new Segment(0, 2, segmentTexture));
        segments.Add(new Segment(0, 1, segmentTexture));
        segments.Add(new Segment(0, 0, segmentTexture));
    }
	
	// Update is called once per frame
	void Update()
    {
        if (!stopped && !paused)
        {
            timer -= Time.deltaTime;

            if (Input.GetKeyDown("up"))
            {
                dir = Direction.UP;
            }


            if (Input.GetKeyDown("down"))
            {
                dir = Direction.DOWN;
            }

            if (Input.GetKeyDown("left"))
            {
                dir = Direction.LEFT;
            }

            if (Input.GetKeyDown("right"))
            {
                dir = Direction.RIGHT;
            }


            if (timer <= 0)
            {
                switch (dir)
                {
                    case Direction.UP:
                        MoveAll(0, 1);
                        break;

                    case Direction.DOWN:
                        MoveAll(0, -1);
                        break;

                    case Direction.LEFT:
                        MoveAll(-1, 0);
                        break;

                    case Direction.RIGHT:
                        MoveAll(1, 0);
                        break;

                    case Direction.NONE:
                        break;

                    case Direction.STOP:
                        break;

                    default:
                        break;
                }

                timer = 1/speed;
            }
        }
    }

    private void MoveAll(int x, int y)
    {
        Segment head = segments[0];
        Vector2Int oldPositionNextSegment = new Vector2Int(head.X, head.Y);
        head.Move(x, y);

        for(int i = 1; i < segments.Count; i++)
        {
            Segment temp = segments[i];

            Vector2Int tempPosition = new Vector2Int(temp.X, temp.Y);

            temp.MoveTo(oldPositionNextSegment.x, oldPositionNextSegment.y);

            oldPositionNextSegment = tempPosition;
        }

        if(addSegment)
        {
            segments.Add(new Segment(oldPositionNextSegment.x, oldPositionNextSegment.y, segmentTexture));
            addSegment = false;
        }
    }

    void OnGUI()
    {
        foreach (Segment s in segments)
        {
            float xPosition = s.X;
            float yPosition = s.Y;
            Graphics.DrawTexture(new Rect(-53 + xPosition, -49 + yPosition, 1, 1), segmentTexture);
        }
    }

    public Vector2Int GetTailPosition()
    {
        return new Vector2Int(segments[segments.Count -1].X, segments[segments.Count - 1].Y);
    }

    public Vector2Int GetHeadPosition()
    {
        return new Vector2Int(segments[0].X, segments[0].Y);
    }

    public void AddSegment()
    {
        addSegment = true;
    }

    public bool HeadInBody()
    {
        Segment head = segments[0];

        for(int i = 1; i < segments.Count; i++)
        {
            if (segments[i].X == head.X && segments[i].Y == head.Y)
            {
                return true;
            }
        }

        return false;
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
        dir = oldDir = Direction.UP;
        timer = 1/speed;
        addSegment = false;
        stopped = false;

        numberSegments = 3;
        segments = new List<Segment>();
        segments.Add(new Segment(0, 2, segmentTexture));
        segments.Add(new Segment(0, 1, segmentTexture));
        segments.Add(new Segment(0, 0, segmentTexture));
    }
}
