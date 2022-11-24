using UnityEngine;

public class DifferenceButtonPromtView : MonoBehaviour
{
    [SerializeField] private GameObject _promtTemplate;
    [SerializeField] private RectTransform _effectParrent;

    [SerializeField] private DifferenceButtonPromt _differenceButtonPromt;

    private float _effectLifeTime = 5;

    private void OnEnable()
    {
        _differenceButtonPromt.ShowedPromt += OnShowedPromt;
    }

    private void OnDisable()
    {
        _differenceButtonPromt.ShowedPromt -= OnShowedPromt;
    }

    private void OnShowedPromt(DifferenceButton button)
    {
        ShowPromtOn(button);
    }

    private void ShowPromtOn(DifferenceButton button)
    {
        Vector2 position = button.transform.position;
        Vector2 Scale = button.transform.localScale;

        var effect = Instantiate(_promtTemplate, position, Quaternion.identity, _effectParrent);        

        effect.transform.localScale = Scale;

        Destroy(effect.gameObject, _effectLifeTime);

    }
}
