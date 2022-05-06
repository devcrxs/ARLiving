using System.Collections.Generic;
using UnityEngine; 
public class DataManager : MonoBehaviour
{
    [SerializeField] private List<Item> items = new List<Item>();
    [SerializeField] private GameObject buttonContainer;
    [SerializeField] private ItemButtonManager itemButtonManager;

    private void Start()
    {
        CreateButtons();
    }

    private void CreateButtons()
    {
        foreach (var item in items)
        {
            ItemButtonManager itemButton;
            itemButton = Instantiate(itemButtonManager, buttonContainer.transform);
            itemButton.ItemName = item.itemName;
            itemButton.ItemImage = item.itemImage;
            itemButton.Item3DModel = item.item3DModel;
        }
    }
}
