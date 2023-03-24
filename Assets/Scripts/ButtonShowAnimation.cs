using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonShowAnimation : ButtonAction
{
    private Image _funnyImage;

    public void Setup(Image imageToShow)
    {
        _funnyImage = imageToShow;

    }

    public override void MainAction()
    {
        base.MainAction();
        StartCoroutine(FunnyAnimation());
    }

    private IEnumerator FunnyAnimation()
    {

        while (true)
        {
            float deltaTime = 0;
            float animationTime = 1;
            while (deltaTime < animationTime)
            {
                _funnyImage.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, deltaTime / animationTime);
                deltaTime += Time.deltaTime;
                yield return null;
            }
            deltaTime = 0f;
            while (deltaTime < animationTime)
            {
                _funnyImage.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, deltaTime / animationTime);
                deltaTime += Time.deltaTime;
                yield return null;
            }

            yield return null;
        }
    }
}
