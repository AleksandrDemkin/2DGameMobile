﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Tween
{
    public class PopupView : MonoBehaviour
    {
        [SerializeField] private Button _buttonClosePopup;
        [SerializeField] private float _duration = 1.0f;
        [SerializeField] private float _strength = 20.0f;
        [SerializeField] private int _vibrato = 3;

        private void Start()
        {
            _buttonClosePopup.onClick.AddListener(HidePopup);
        }

        private void OnDestroy()
        {
            _buttonClosePopup.onClick.RemoveAllListeners();
        }

        public void ShowPopup()
        {
            gameObject.SetActive(true);

            AnimationShow();
        }

        public void HidePopup()
        {
            AnimationHide();
        }
        
        private void AnimationShow()
        {
            var sequence = DOTween.Sequence();

            sequence.Insert(0.0f, transform.DOScale(Vector3.one, _duration));
            sequence.Insert(0.0f, transform.DOShakePosition(_duration, _strength));
            sequence.Insert(0.0f, transform.DOShakeRotation(_duration, _strength,_vibrato));
            sequence.OnComplete(() =>
            {
                sequence = null;
            });
        }

        private void AnimationHide()
        {
            var sequence = DOTween.Sequence();

            sequence.Insert(0.0f, transform.DOScale(Vector3.zero, _duration));
            sequence.OnComplete(() =>
            {
                sequence = null;
                gameObject.SetActive(false);
            });
        }
    }
}