using System.Collections;
using UnityEngine;

public class CurrencyRewarderView : MonoBehaviour
{
    [SerializeField] private RectTransform _cavas;
    [SerializeField] private СurrencyRewarder _сurrencyRewarder;
    [SerializeField] private GameObject _rewadAnimationTemplate;


    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    private float _animationLifeTime = 3;

    private void OnEnable()
    {
        _сurrencyRewarder.RewardSended += OnRewardSended;
    }

    private void OnDisable()
    {
        _сurrencyRewarder.RewardSended -= OnRewardSended;

    }

    private void OnRewardSended(int amount)
    {
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(RewardAnimation(amount));
        }
    }

    private IEnumerator RewardAnimation(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float randomDelay = Random.Range(_minDelay, _maxDelay);
            yield return new WaitForSeconds(randomDelay);
            SpawnAnimation();
        }
    }

    private void SpawnAnimation()
    {
        var template = Instantiate(_rewadAnimationTemplate,_cavas.transform);
        Destroy(template.gameObject, _animationLifeTime);
    }

}

