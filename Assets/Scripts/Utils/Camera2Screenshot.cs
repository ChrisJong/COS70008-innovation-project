using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2Screenshot : MonoBehaviour
{
    public Camera ScreenshotCamera;
    public Camera MainCamera;
    public Texture2D ScreenshotTexture;
    private RenderTexture RenderTexture;
    private TesseractDemoScript Tesseract;

    public IEnumerator processScreen()
    {
        ScreenshotCamera.transform.gameObject.SetActive(true);
        ScreenshotCamera.aspect = MainCamera.aspect;
        ScreenshotCamera.fieldOfView = MainCamera.fieldOfView;
        ScreenshotCamera.orthographic = MainCamera.orthographic;
        ScreenshotCamera.orthographicSize = MainCamera.orthographicSize;
        ScreenshotCamera.backgroundColor = MainCamera.backgroundColor;
        ScreenshotCamera.cullingMask = 6;
        int currentCullingMask = MainCamera.cullingMask;
        MainCamera.cullingMask = 1 << 5;
        MainCamera.cullingMask = ~MainCamera.cullingMask;
        //Screenshot.cullingMask =~ Screenshot.cullingMask;
        yield return new WaitForEndOfFrame();
        ScreenshotCamera.targetTexture = RenderTexture;
        ScreenshotCamera.Render();
        ScreenshotTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ScreenshotTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ScreenshotTexture.Apply();
        Tesseract.Recoginze(ScreenshotTexture);

        // Cleanup
        ScreenshotCamera.targetTexture = null;
        MainCamera.cullingMask = currentCullingMask;
        ScreenshotCamera.transform.gameObject.SetActive(false);
        Debug.Log("Coroutine run");
    }

    public void takeScreenshot()
    {
        Debug.Log("Taking screenshot");
        RenderTexture = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Default);
        StartCoroutine("processScreen");
    }
}