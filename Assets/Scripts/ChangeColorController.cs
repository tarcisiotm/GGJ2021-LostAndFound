using System.Collections;
using UnityEngine;

public class ChangeColorController : MonoBehaviour
{
    [SerializeField] private float _targetTime;

    [SerializeField] private Color Color1 = Color.white;
    [SerializeField] private Color Color2 = Color.red;
    [SerializeField] private bool _onEnable = false;

    public float Speed = 1, Offset;

    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    bool _isTakingDamage = false;

    float _currentTime = 0;

    void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    private void OnEnable()
    {
        if (_onEnable)
        {
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color", Color2);
            _renderer.SetPropertyBlock(_propBlock);
            Destroy(this);
        }
    }

    public void TakeDamage()
    {
        _isTakingDamage = true;
        _currentTime = 0;
    }

    void Update()
    {
        if (!_isTakingDamage) return;

        _currentTime += Time.deltaTime;

        _renderer.GetPropertyBlock(_propBlock);

        _propBlock.SetColor("_Color", Color.Lerp(Color2, Color1, _currentTime / _targetTime));
        //if (_currentTime <= _targetTime / 2f) _propBlock.SetColor("_Color", Color.Lerp(Color1, Color2, _currentTime / (_targetTime/2f)));
        //else _propBlock.SetColor("_Color", Color.Lerp(Color2, Color1, _currentTime / _targetTime));

        _renderer.SetPropertyBlock(_propBlock);

        if (_currentTime >= _targetTime) _isTakingDamage = false;
    }
}
