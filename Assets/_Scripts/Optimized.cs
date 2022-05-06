using UnityEngine;
public class Optimized : MonoBehaviour
{
    void Start()
    {
        Application.targetFrameRate = 60;
        Screen.SetResolution(Screen.width,Screen.height,true);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }
}
