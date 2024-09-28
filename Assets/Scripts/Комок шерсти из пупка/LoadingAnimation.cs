using UnityEngine;

public class LoadingAnimation : MonoBehaviour
{
    [SerializeField] private GameObject _animation;
    public void Enable()
    {
        _animation.gameObject.SetActive(true);
    }
    
    public void Disable()
    {
        _animation.gameObject.SetActive(false);
    }
}