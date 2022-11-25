using UnityEngine;

public class LevelsImages : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    private ImageWebDownloader _imageDownloader = new();

    private void Start()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            var level = _levels[i];

            if (level.Image1 == null)
            {
                StartCoroutine(_imageDownloader.SetImage(level.Image1Url, level.SetImage1));
            }
            if (level.Image2 == null)
            {
                StartCoroutine(_imageDownloader.SetImage(level.Image2Url, level.SetImage2));
            }
        }
    }
}
