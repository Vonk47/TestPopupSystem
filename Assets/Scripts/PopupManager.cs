using Game.Popups;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    [SerializeField] Transform _popupViewRoot;
    [SerializeField] Popup _popupPrefab;
    [SerializeField] PopupWithButtons _buttonPopupPrefab;
    private bool _popupIsPresentOrLoading;
    public bool PopupIsPresentOrLoading => _popupIsPresentOrLoading;
    public event Action<IPopup> PopupClosed;

    public void StartLoading(IPopup popupItem) 
    {
        StartCoroutine(LoadPopup(popupItem));
    }

    private IEnumerator LoadPopup(IPopup popupItem)     // our main coroutine for loading popup
    {
        if (_popupIsPresentOrLoading)
            yield break;


        switch (popupItem)
        {
            case Popup localPopup:
                _popupIsPresentOrLoading = true;
                var instantiated = Instantiate(_popupPrefab, _popupViewRoot);
                instantiated.gameObject.SetActive(false);

                UnityWebRequest www = UnityWebRequestTexture.GetTexture(localPopup.PopupInfo.PopupBackgroundURL);
                yield return www.SendWebRequest();
                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError(www.error);
                    Destroy(instantiated.gameObject);
                    _popupIsPresentOrLoading = false;
                    yield break;
                }
                else
                {
                    Texture2D texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2());
                    instantiated.Draw(sprite, localPopup.PopupInfo.PopupTitle, localPopup.PopupInfo.PopupDescription);
                    instantiated.OnPopupClose += OnPopupClosed;
                    yield return new WaitForSecondsRealtime(1); //lets emulate we loading some heavyweight;
                    instantiated.Show();
                }
                break;
            case PopupWithButtons buttonPopup:
                _popupIsPresentOrLoading = true;
                var instantiatedButton = Instantiate(_buttonPopupPrefab, _popupViewRoot);
                instantiatedButton.gameObject.SetActive(false);
                // we don't loading anything in this popup, so we  skip any www further
                instantiatedButton.SetupButton(buttonPopup.DesiredTypeOfButton);
                instantiatedButton.Show();
                break;
            


        }

        _popupIsPresentOrLoading = false;
    }

      

  
    private void OnPopupClosed(IPopup popup)
    {
        popup.OnPopupClose -= OnPopupClosed;
        _popupIsPresentOrLoading = false;
        PopupClosed?.Invoke(popup);
    }

  
}
