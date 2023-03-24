using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Popups;
using UnityEngine.Networking;

namespace Game.Core.UI
{
    public class PopupEnquer<T> where T : IPopup   // Monobehaviour and generic isn't supported
    {
        private Queue<T> _itemQueue = new Queue<T>();
        private PopupManager _manager;

        public void Setup(PopupManager manager)
        {
            _manager = manager;
            _manager.PopupClosed += OnPopupClosed;
        }

        public void Enqueue(T enquiedItem)              // this one does a enqueue functional
        {
            _itemQueue.Enqueue(enquiedItem);
            if (!_manager.PopupIsPresentOrLoading)
                _manager.StartLoading(_itemQueue.Dequeue());
        }

        private void CheckForQueue()
        {
            if (QueueIsOccupied)
            {
                _manager.StartLoading(_itemQueue.Dequeue());
            }
        }

        private void OnPopupClosed(IPopup item)
        {
            Debug.Log("popupClosed");
            CheckForQueue();
        }

        private bool QueueIsOccupied => _itemQueue.Count > 0;

    }
}
