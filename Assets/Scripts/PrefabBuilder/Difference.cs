using LibraryForGames;
using System;
using UnityEngine;

public class Difference
{
    private Vector2Int[] _directions = { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left ,
    
    new Vector2Int(1,1), new Vector2Int(-1,1), new Vector2Int(1,-1), new Vector2Int(-1,-1),};

    private bool[,] _pixels;

    private readonly int _width;
    private readonly int _height;
    private readonly float _scaleCoeficient = 30f;
    private int _xSum;
    private int _ySum;
    private int _counter;

    private int _minX = int.MaxValue;
    private int _maxX;
    private int _minY = int.MaxValue;
    private int _maxY;

    public bool[,] GetPixels() => _pixels;

    public Difference(bool[,] pixels)
    {
        _pixels = pixels;
        _width = pixels.GetLength(0);
        _height = pixels.GetLength(1);
    }

    public void Seek(int x, int y)
    {
        _pixels[x, y] = false;
        _counter++;
        _xSum += x;
        _ySum += y;

        foreach (var direction in _directions)
        {
            int newX = x + direction.x;
            int newY = y + direction.y;

            if (newX >= 0 && newX < _width &&
                newY >= 0 && newY < _height)
            {
                if (_pixels[newX, newY])
                {
                    UpdateValues(newX, newY);
                    Seek(newX, newY);
                }
            }
        }
    }

    private void UpdateValues(int x, int y)
    {
        if (x > _maxX)
            _maxX = x;
        if (y > _maxY)
            _maxY = y;
        if (x < _minX)
            _minX = x;
        if (y < _minY)
            _minY = y;
    }

    public void Print()
    {
        float x = (float)Math.Round(_xSum / (double)_counter / _width, 2);
        float y = (float)Math.Round(_ySum / (double)_counter / _height, 2);

        //Debug.Log($"{_counter}");

        float scaleX = (float)(_maxX - _minX) / _width;
        float scaleY = (float)(_maxY - _minY) / _height;
        Vector2 scale = new Vector2(scaleX, scaleY);

        Tools.LogColor($" Position {x}, {y}", Color.green);

        Debug.Log("Scale" + scale);
    }

    public DifferenceButtonConfig GetConfig()
    {
        float x = (float)Math.Round(_xSum / (double)_counter / _width, 2);
        float y = (float)Math.Round(_ySum / (double)_counter / _height, 2);

        //Debug.Log($"{_counter}");

        float scaleX = (_maxX - _minX) / _scaleCoeficient;
        float scaleY =  (_maxY - _minY) / _scaleCoeficient;

        Vector2 scale = new Vector2(scaleX, scaleY);

        return new DifferenceButtonConfig(new Vector2(x, y), scale);
    }
}