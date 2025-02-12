using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DebugLevel
{
    Verbose,
    Info,
    Warning,
    Error
}

public static class DebugLogger
{
    private static DebugSettings debugSettings;

    public static void Initialize(DebugSettings settings)
    {
        debugSettings = settings;
    }

    public static void Log(string tag, string message, DebugLevel level = DebugLevel.Info)
    {
        if (debugSettings == null || debugSettings.IsTagEnabled(tag) && debugSettings.IsDebugLevelEnabled(level))
        {
            Color tagColor = debugSettings.GetTagColor(tag);
            string tagColorHex = "#" + ColorUtility.ToHtmlStringRGB(tagColor); //Convert to Hex string

            switch (level)
            {
                case DebugLevel.Verbose:
                    Debug.Log($"[<color={tagColorHex}>{tag}</color>]<color=#be66ff>[VERBOSE]</color> {message}");
                    break;
                case DebugLevel.Warning:
                    Debug.LogWarning($"[<color={tagColorHex}>{tag}</color>]<color=#ff9300>[WARNING]</color> {message}");
                    break;
                case DebugLevel.Error:
                    Debug.LogError($"[<color={tagColorHex}>{tag}</color>]<color=#ff1400>[ERROR]</color> {message}");
                    break;
                default:
                    Debug.Log($"[<color={tagColorHex}>{tag}</color>]<color=#00ff93>[INFO]</color> {message}");
                    break;
            }
        }
    }

    public static void ToggleTag(string tag, bool state)
    {
        if(debugSettings != null)
        {
            debugSettings.EnableTag(tag, state);
        }
    }
}
