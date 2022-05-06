using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    public event Action OnMainMenu;
    public event Action OnItemsMenu;
    public event Action OnArPosition;
    public event Action OnScreenShot;
    
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void MainMenu()
    {
        OnMainMenu?.Invoke();
    }

    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
    }

    public void ArPosition()
    {
        OnArPosition?.Invoke();
    }

    public void ScreenShot()
    {
        OnScreenShot?.Invoke();
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
