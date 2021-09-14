using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Screenshot : MonoBehaviour
{
    public Camera Screenshot;
    public Camera MainCamera;
    public Texture2D ScreenshotTexture;
    public RenderTexture RenderTexture;

    public TesseractDemoScript Tesseract;

    public IEnumerator processScreen()
    {
        Screenshot.aspect = MainCamera.aspect;
        Screenshot.fieldOfView = MainCamera.fieldOfView;
        Screenshot.orthographic = MainCamera.orthographic;
        Screenshot.orthographicSize = MainCamera.orthographicSize;
        Screenshot.backgroundColor = MainCamera.backgroundColor;
        Screenshot.cullingMask = 6;
        MainCamera.cullingMask = 1 << 5;
        MainCamera.cullingMask = ~MainCamera.cullingMask;
        //Screenshot.cullingMask =~ Screenshot.cullingMask;
        yield return new WaitForEndOfFrame();
        Screenshot.targetTexture = RenderTexture;
        Screenshot.Render();
        ScreenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ScreenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ScreenshotTexture.Apply();
        Tesseract.Recoginze(ScreenshotTexture);
        
        // Cleanup
        Screenshot.targetTexture = null;
        Debug.Log("Coroutine run");
    }

    public void saveAsPng()
    {
        byte[] bytes = ScreenshotTexture.EncodeToPNG();
        string path = "jar:file://" + Application.dataPath + "!/assets/screenshot.png";
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log(bytes.Length / 1024 + " file size of screenshot");
    }

    public void takeScreenshot()
    {
        Debug.Log("Taking screenshot");
        RenderTexture = new RenderTexture(Screen.width, Screen.height,0,RenderTextureFormat.Default, RenderTextureReadWrite.Default);
        StartCoroutine("processScreen");
    }
}
