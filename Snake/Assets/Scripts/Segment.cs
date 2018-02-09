using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment
{
    int x, y;
    Texture2D texture;

    public Segment(int _x, int _y, Texture2D _texture)
    {
        x = _x;
        y = _y;
        texture = _texture;
    }

    public int X
    {
        get { return x; }
        private set { x = value; }
    }

    public int Y
    {
        get { return y; }
        private set { y = value; }
    }

    public void Move(int offsetX, int offsetY)
    {
        x += offsetX;
        y += offsetY;
    }

    public void MoveTo(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}
