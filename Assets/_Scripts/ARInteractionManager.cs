using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
public class ARInteractionManager : MonoBehaviour
{
    [SerializeField] private Camera arCamera;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private GameObject arPointer;
    private GameObject item3DModel;
    private GameObject itemSelected;
    private Vector2 initialPositionTouch;
    private bool isInitialPosition;
    private bool isOver3DModel;

    public GameObject Item3DModel
    {
        set
        {
            item3DModel = value;
            item3DModel.transform.position = arPointer.transform.position;
            item3DModel.transform.parent = arPointer.transform;
            isInitialPosition = true;
        }
    }

    private void Start()
    {
        arPointer = transform.GetChild(0).gameObject;
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        GameManager.instance.OnMainMenu += SetItemPosition;
    }

    private void SetItemPosition()
    {
        if (item3DModel == null) return;
        item3DModel.transform.parent = null;
        arPointer.SetActive(false);
        item3DModel = null;
    }

    private void Update()
    {
        SetInitalPosition3DModel();
        transform.position =  Move3DModel();
        Rotate3DModel();
        if (isOver3DModel && item3DModel == null)
        {
            GameManager.instance.ArPosition();
            item3DModel = itemSelected;
            itemSelected = null;
            arPointer.SetActive(true);
            transform.position = item3DModel.transform.position;
            item3DModel.transform.parent = arPointer.transform;
        }
    }

    private void SetInitalPosition3DModel()
    {
        if (!isInitialPosition) return;
        Vector2 middlePointScreen = new Vector2(Screen.width / 2, Screen.height / 2);
        arRaycastManager.Raycast(middlePointScreen, hits,TrackableType.Planes);
        if (hits.Count <= 0) return;
        transform.position = hits[0].pose.position;
        transform.rotation = hits[0].pose.rotation;
        arPointer.SetActive(true);
        isInitialPosition = false;
    }

    private Vector3 Move3DModel()
    {
        if (Input.touchCount <= 0) return transform.position;
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            isOver3DModel = IsTapOver3DModel(touch.position);
        }
        if (touch.phase != TouchPhase.Moved) return transform.position;
        if (!arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes)) return transform.position;
        Pose hitPose = hits[0].pose;
        if (IsTouchInterface(touch) || !isOver3DModel) return transform.position;
        return hitPose.position;
    }

    private bool IsTapOver3DModel(Vector2 touch)
    {
        Ray ray = arCamera.ScreenPointToRay(touch);
        if (Physics.Raycast(ray, out RaycastHit hit3DModel))
        {
            if (hit3DModel.collider.CompareTag("Item"))
            {
                itemSelected = hit3DModel.transform.gameObject;
                return true;
            }
        }

        return false;
    }
    
    private bool IsTouchInterface(Touch touch)
    {
        return EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase == TouchPhase.Ended;
    }

    private void Rotate3DModel()
    {
        if (Input.touchCount == 2)
        {
            Touch oneTouch = Input.GetTouch(0); 
            Touch twoTouch = Input.GetTouch(1);

            if (oneTouch.phase == TouchPhase.Began || twoTouch.phase == TouchPhase.Began)
            {
                initialPositionTouch = twoTouch.position - oneTouch.position;
            }

            if (oneTouch.phase == TouchPhase.Moved || twoTouch.phase == TouchPhase.Moved)
            {
                Vector2 currentPositionTouch = twoTouch.position - oneTouch.position;
                float angle = Vector2.SignedAngle(initialPositionTouch, currentPositionTouch);
                item3DModel.transform.rotation = Quaternion.Euler(0,item3DModel.transform.eulerAngles.y - angle, 0);
                initialPositionTouch = currentPositionTouch;
            }
        }
    }

    public void DeleteItem()
    {
        Destroy(item3DModel);
        arPointer.SetActive(false);
        GameManager.instance.MainMenu();
    }
}