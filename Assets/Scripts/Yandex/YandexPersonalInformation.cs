using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Networking;

public class YandexPersonalInformation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private RawImage _playerImage;
    

    [DllImport("__Internal")]
    private static extern void SetPlayerPersonalInformation();

    public void SetInformationButton()
    {
        SetPlayerPersonalInformation();
    }

    public void SetName(string name)
    {
        _nameText.text = name;
    }

    public void SetPhoto(string photoUrl)
    {
        StartCoroutine(DownloadImage(photoUrl));
    }

    private IEnumerator DownloadImage(string photoUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(photoUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)        
            Debug.Log(request.error);        
        else        
            _playerImage.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
        
    }

}
