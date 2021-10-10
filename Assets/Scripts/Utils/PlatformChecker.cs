using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformChecker : MonoBehaviour
{
    public static bool IsAndroid()
    {
        return Application.platform == RuntimePlatform.Android;
    }

    public static bool IsIOS()
    {
        return Application.platform == RuntimePlatform.IPhonePlayer;
    }

    public static bool IsWindows()
    {
        return Application.platform == RuntimePlatform.WindowsPlayer;
    }

    public static bool isFlash()
    {
        return Application.platform == RuntimePlatform.FlashPlayer;
    }

    public static bool IsWindowsEditor()
    {
        return Application.platform == RuntimePlatform.WindowsEditor;
    }
}
