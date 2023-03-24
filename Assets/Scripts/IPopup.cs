using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopup
{
    void Show();
    void Hide();
     
    event Action<IPopup> OnPopupClose;
    event Action OnPopupLoaded;
}
