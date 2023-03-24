using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Popups;
using UnityEngine.UI;

namespace Game.Core.UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private ScreenController _screenController;
        [SerializeField] private PopupManager _popupManager;
        private PopupEnquer<IPopup> _popupController;

        [SerializeField] private Image _funnyImage;

        private void Start()
        {
            _popupController = new PopupEnquer<IPopup>();          // if you want to show popup over popup, you should create more enquiers
            _popupController.Setup(_popupManager);
        }

        public void EnqueSimplePopup()
        {
            IPopup popup = new Popup(new PopupLoadInformation("Hello its first popup",
                "You can put your description here",
                "https://img.freepik.com/free-vector/hand-painted-watercolor-pastel-sky-background_23-2148902771.jpg"));
            _popupController.Enqueue(popup);

        }

        public void EnqueButtonPopup()
        {
            var objToSpawn = new GameObject("ButtonShowAnim");
            var animComponent=  objToSpawn.AddComponent<ButtonShowAnimation>();
            animComponent.Setup(_funnyImage);
            IPopup popup = new PopupWithButtons(animComponent);
            _popupController.Enqueue(popup);

        }

        public void EnqueThreePopups()
        {
            for (int i = 1; i < 4; i++)
            {
                IPopup popup = new Popup(new PopupLoadInformation("Hello its " + i + " popup",
                    "You can put your description here",
                    "https://img.freepik.com/free-vector/hand-painted-watercolor-pastel-sky-background_23-2148902771.jpg"));
                _popupController.Enqueue(popup);
            }

        }
    }
}
