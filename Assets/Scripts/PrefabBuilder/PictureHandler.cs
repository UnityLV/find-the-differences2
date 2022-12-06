using LibraryForGames;
using System;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PictureHandler : MonoBehaviour
{
    [field: SerializeField] public string FirstUrl { get; private set; }
    [field: SerializeField] public string SecondUrl { get; private set; }
    [SerializeField] private int _scaleFactor = 4;

    private Sprite _first;
    private Sprite _second;
    private ImageWebDownloader _downloader = new();

    private void Start()
    {
        GetDifferences();
    }

    public IEnumerator SetImages() 
    {
        yield return _downloader.SetImage(FirstUrl, (S) => _first = S);
        yield return _downloader.SetImage(SecondUrl, (S) => _second = S);
    }

    public IEnumerable<Difference> GetDifferences()
    {
        if (SizeIsEquel() == false)
        {
            throw new Exception($"Pictures has different size {_first.texture.width} x {_first.texture.height}" +
                $" and {_second.texture.width} x {_second.texture.height}");
        }

        int width = _first.texture.width;
        int height = _first.texture.height;
        bool[,] pixels = GetArray(width, height);

        width /= _scaleFactor;
        height /= _scaleFactor;
        List<Difference> differences = CalculateDifferences(width, height, ref pixels);

        foreach (var difference in differences)
        {
            yield return difference;
            difference.Print();
        }

    }

    private static List<Difference> CalculateDifferences(int width, int height, ref bool[,] pixels)
    {
        List<Difference> differences = new List<Difference>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (pixels[x, y])
                {
                    Difference difference = new Difference(pixels);
                    differences.Add(difference);
                    difference.Seek(x, y);
                    pixels = difference.GetPixels();
                }
            }
        }

        return differences;
    }

    private bool[,] GetArray(int width, int height)
    {
        bool[,] pixels = new bool[width / _scaleFactor, height / _scaleFactor];

        for (int x = 0; x  < width - _scaleFactor; x += _scaleFactor)
            for (int y = 0; y < height - _scaleFactor; y += _scaleFactor)
                if (PixelsIsDiffrent(x, y))
                    pixels[x / _scaleFactor, y / _scaleFactor] = true;
        return pixels;
    }

    private bool PixelsIsDiffrent(int x, int y) => _first.texture.GetPixel(x, y) != _second.texture.GetPixel(x, y);

    private bool SizeIsEquel() => _first.texture.width == _second.texture.width && _first.texture.height == _second.texture.height;
}
