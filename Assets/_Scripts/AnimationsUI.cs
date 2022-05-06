using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class AnimationsUI : MonoBehaviour
{
    public static AnimationsUI instance;
    private void Awake()
    {
        if (instance == null) instance = this;
    }
    
    public void ScaleItems(GameObject[] items, Vector3 scale, float duration)
    {
        foreach (var item in items)
        {
            item.transform.DOScale(scale,duration);
        }
    }

    public void FadeItems(GameObject[] items, float fadeValue, float duration)
    {
        foreach (var item in items)
        {
            item.GetComponent<Image>().DOFade(fadeValue, duration);
        }
    }
    public void FadeItemsList(List<GameObject> items, float fadeValue, float duration)
    {
        foreach (var item in items)
        {
            item.GetComponent<Image>().DOFade(fadeValue, duration);
        }
    }

    public void DoMoveY(Transform target ,float positionY, float duration)
    {
        target.DOMoveY(positionY, duration);
    }

    public void DoScale(Transform target, Vector3 scale, float duration)
    {
        target.DOScale(scale, duration);
    }
}
