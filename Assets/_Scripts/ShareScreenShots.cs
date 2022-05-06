using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
public class ShareScreenShots : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    private ARPointCloudManager aRPointCloudManager;

    private void Start()
    {
        GameManager.instance.OnScreenShot += TakeScreenShot;
        aRPointCloudManager = FindObjectOfType<ARPointCloudManager>();
    }

    private void TakeScreenShot()
    {
        TurnOnOffARContents();
        StartCoroutine(TakeScreenshotAndShare());
    }

    private void TurnOnOffARContents()
    {
        var points = aRPointCloudManager.trackables;
        foreach (var point in points)
        {
            point.gameObject.SetActive(!point.gameObject.activeSelf);
        }
        mainMenuCanvas.SetActive(!mainMenuCanvas.activeSelf);
        
    }
    
    private IEnumerator TakeScreenshotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        new NativeShare().AddFile( filePath )
            .SetSubject( "Subject goes here" ).SetText( "¡Mira como se ven mis modelos 3D en el mundo real!" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
        TurnOnOffARContents();
        // Share on WhatsApp only, if installed (Android only)
        //if( NativeShare.TargetExists( "com.whatsapp" ) )
        //	new NativeShare().AddFile( filePath ).AddTarget( "com.whatsapp" ).Share();
    }
}
