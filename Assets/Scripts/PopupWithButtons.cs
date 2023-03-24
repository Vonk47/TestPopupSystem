using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Popups
{
    public class PopupWithButtons : MonoBehaviour, IPopup
    {
        [SerializeField] private Animator _anim;
        [Header("Visuals")]
        [SerializeField] private Image _background;
        [SerializeField] private Button _testButton;
        public event Action<IPopup> OnPopupClose;
        public event Action OnPopupLoaded;
        public ButtonAction DesiredTypeOfButton;

        public PopupWithButtons(ButtonAction desiredTypeOfButton)
        {
            DesiredTypeOfButton = desiredTypeOfButton;
        }

        public void SetupButton(ButtonAction action)
        {
            DesiredTypeOfButton = action;
            _testButton.onClick.AddListener(action.MainAction);
        }

        public void Hide()
        {
            DesiredTypeOfButton.Dispose();
            _anim.SetTrigger("Hide");
            StartCoroutine(CheckIfAnimationEnded());
        }

        public IEnumerator CheckIfAnimationEnded()
        {
            // There should be a real check onAnimation end, or  implementation using unity animations event
            yield return new WaitForSecondsRealtime(1f);
            OnPopupClose?.Invoke(this);
            Destroy(this.gameObject);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            _anim.SetTrigger("Show");
            OnPopupLoaded?.Invoke();
        }


    }
}
