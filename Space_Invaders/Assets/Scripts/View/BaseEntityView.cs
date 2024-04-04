using DG.Tweening;
using UnityEngine;

namespace View
{
    public abstract class BaseEntityView : MonoBehaviour
    {
        [Header("Tween")]
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private float _shakeStength;
        [SerializeField] private float _shakeDuration;
        [SerializeField] private Color _damageColor;
        [SerializeField] private float _colorDuration;
        
        public void InvokeDamageTween()
        {
            var seq = DOTween.Sequence();
            seq.Restart();
            seq.Append(transform.DOShakePosition(_shakeDuration, _shakeStength));
            seq.Join(_sprite.DOColor(_damageColor, _colorDuration).SetLoops(2, LoopType.Restart));
            seq.Append(_sprite.DOColor(Color.white, 0));
        }
    }
}