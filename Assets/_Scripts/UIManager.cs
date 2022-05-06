using System.Collections.Generic;
using UnityEngine;
public class UIManager : MonoBehaviour
{
    private float timeScale = 0.3f;
    private float timeFade = 0.2f;
    private Vector3 scaleDefault = new Vector3(0.65177f, 0.65177f, 0.65177f);
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject showItemsCanvas;
    [SerializeField] private GameObject arPositionCanvas;
    [SerializeField] private GameObject scrollViewItems;

    private void Start()
    {
        GameManager.instance.OnMainMenu += ShowMainMenu;
        GameManager.instance.OnItemsMenu += ShowItemsMenu;
        GameManager.instance.OnArPosition += ShowArPosition;
    }

    private void ShowMainMenu()
    {
        AnimateItemsCanvas(mainMenuCanvas,scaleDefault,timeScale,1);
        AnimateItemsCanvas(showItemsCanvas,Vector3.zero,timeScale,0);
        AnimationsUI.instance.DoMoveY(scrollViewItems.transform,200,0.3f);
        AnimateItemsCanvas(arPositionCanvas,Vector3.zero,timeScale,0);
    }

    private void AnimateItemsCanvas(GameObject canvas,Vector3 scaleItems,float durationScale, float fadeValue)
    {
        var countChildrens =canvas.transform.childCount;
        GameObject[] childs = new GameObject[countChildrens];
        childs = GetChildsCanvas(canvas,countChildrens);
        AnimationsUI.instance.ScaleItems(childs,scaleItems,durationScale);
        AnimationsUI.instance.FadeItems(childs,fadeValue,timeFade);
        AnimationsUI.instance.FadeItemsList(GetChildsButtons(childs),fadeValue,timeFade);
    }

    private GameObject[] GetChildsCanvas(GameObject canvasContainerChilds, int sizeArray)
    {
        GameObject[] contentChilds = new GameObject[sizeArray];
        for (int i = 0; i < sizeArray; i++)
        {
            contentChilds[i] = canvasContainerChilds.transform.GetChild(i).gameObject;
        }

        return contentChilds;
    }
    
    private List<GameObject> GetChildsButtons(GameObject[] containerChilds)
    {
        List<GameObject> contentChilds = new List<GameObject>();
        for (int i = 0; i < containerChilds.Length; i++)
        {
            for (int j = 0; j < containerChilds[i].transform.childCount; j++)
            {
                contentChilds.Add(containerChilds[i].transform.GetChild(j).gameObject);
            }
        }

        return contentChilds;
    }

    private void ShowItemsMenu()
    {
        AnimateItemsCanvas(mainMenuCanvas,Vector3.zero,timeScale,0);
        AnimateItemsCanvas(showItemsCanvas,scaleDefault,timeScale,1);
        AnimationsUI.instance.DoScale(scrollViewItems.transform,Vector3.one,timeScale);
        AnimationsUI.instance.DoMoveY(scrollViewItems.transform,900,0.3f);
    }

    private void ShowArPosition()
    {
        AnimateItemsCanvas(mainMenuCanvas,Vector3.zero,timeScale,0);
        AnimateItemsCanvas(showItemsCanvas,Vector3.zero,timeScale,0);
        AnimationsUI.instance.DoMoveY(scrollViewItems.transform,200,0.3f);
        AnimateItemsCanvas(arPositionCanvas,scaleDefault,timeScale,1);
    }
}
