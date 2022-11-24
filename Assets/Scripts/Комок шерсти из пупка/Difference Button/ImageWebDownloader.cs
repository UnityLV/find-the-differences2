using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ImageWebDownloader
{
    public IEnumerator SetImage(string url, Action<Sprite> spriteSetter)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
            Debug.Log(request.error);
        else
            spriteSetter(ToSpriteConverter(((DownloadHandlerTexture)request.downloadHandler).texture));
    }

    private Sprite ToSpriteConverter(Texture2D texture2D)
    {
        return Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f),100f);

    }

}


