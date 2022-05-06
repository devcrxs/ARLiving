using UnityEngine;
using UnityEngine.UI;
public class ItemButtonManager : MonoBehaviour
{
    private string itemName;
    private Sprite itemImage;
    private GameObject item3DModel;
    private ARInteractionManager arInteractionManager;
    public string ItemName { set => itemName = value; }
    public Sprite ItemImage { set => itemImage = value; }
    public GameObject Item3DModel { set => item3DModel = value; }

    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        var maskBorder = transform.GetChild(1).transform;
        maskBorder.GetChild(0).GetComponent<Image>().sprite = itemImage;

        var button = GetComponent<Button>();
        button.onClick.AddListener(GameManager.instance.ArPosition);
        button.onClick.AddListener(Create3DModel);

        arInteractionManager = FindObjectOfType<ARInteractionManager>();
    }

    private void Create3DModel()
    {
        var positionInstantiate = Vector3.zero;
        positionInstantiate.z = 4;
        arInteractionManager.Item3DModel =  Instantiate(item3DModel, positionInstantiate,item3DModel.transform.rotation);
    }
}
