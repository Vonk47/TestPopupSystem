using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    public virtual void Dispose()
    {
        StopAllCoroutines();
        Destroy(gameObject);
    }
        
    public virtual void MainAction() // we can do whatever we want here, just programm it in the future
    {
        Debug.Log("you pressed custom button");
    }
}
