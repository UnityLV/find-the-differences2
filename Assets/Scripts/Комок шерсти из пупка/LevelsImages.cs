using UnityEngine;

public class LevelsImages : MonoBehaviour
{
    [SerializeField] private Levels _levels;
    private ImageWebDownloader _imageDownloader = new();

    private void Start()
    {
        //for (int i = 0; i < _levels.Length; i++)
        //{
        //    var level = _levels[i];

        //    if (level.PreviewUrl != string.Empty)
        //    {
        //        StartCoroutine(_imageDownloader.SetImage(level.PreviewUrl, level.SetImage1));
        //    }            
        //}
    }
}
