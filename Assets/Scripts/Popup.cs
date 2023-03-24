using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Popups
{
    public class Popup : MonoBehaviour, IPopup
    {
        [SerializeField] private Animator _anim;
        [Header("Visuals")]
        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _description;

        private PopupLoadInformation _popupInfo;
        public PopupLoadInformation PopupInfo => _popupInfo;
        public event Action OnPopupLoaded;
        public event Action<IPopup> OnPopupClose;

        //   public readonly record LoadInformation  // unfortunately records isn't available for Unity 2020.3 which actually could be obsolete in any minute, so bad for me

        public Popup(PopupLoadInformation popupLoadInformation)  // This constructor actually isn't allowed in Unity, instead its better to create a separate aggregation class, but i will leave it as it is for now
        {
            _popupInfo = popupLoadInformation;
        }

        public void Draw(Sprite background, string header, string description)
        {
            _background.sprite = background;
            _header.text = header;
            _description.text = description;
        }

        public void Hide()
        {
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


    public struct PopupLoadInformation
    {
        public readonly string PopupTitle;
        public readonly string PopupDescription;
        public readonly string PopupBackgroundURL;

        public PopupLoadInformation(string popupTitle, string popupDescription, string popupBackgroundURL)
        {
            PopupTitle = popupTitle;
            PopupDescription = popupDescription;
            PopupBackgroundURL = popupBackgroundURL;
        }
    }
}
