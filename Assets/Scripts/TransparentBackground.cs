using UnityEngine;
using System;
using System.Runtime.InteropServices;
public class TransparentBackground : MonoBehaviour
{
    const int GWL_EXSTYLE = -20;
    const int WS_EX_LAYERED = 0x80000;
    const int WS_EX_TRANSPARENT = 0x20;
    const int LWA_COLORKEY = 0x1;
    const int LWA_ALPHA = 0x2;

    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll", SetLastError = true)]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll", SetLastError = true)]
    static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    void Start()
    {
#if UNITY_STANDALONE_WIN && !UNITY_EDITOR
        var hwnd = GetActiveWindow();

        // Enable layered style
        int extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
        SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_LAYERED);

        // Make background transparent (Set color key to black, fully transparent)
        SetLayeredWindowAttributes(hwnd, 0x000000, 0, LWA_COLORKEY);

        // Optional: Also make the window click-through (good for overlays)
        // SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT);
#endif
    }


}
