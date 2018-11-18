using System.Collections.Generic;
using UnityEngine;

public static class UIService
{
    private static GameObject staticUiLayer;
    private static GameObject dynamicUiLayer;
    private static GameObject overlayUiLayer;
    private static Dictionary<string, GameObject> loadedAssets = new Dictionary<string, GameObject>();
    private static Dictionary<string, AWidget> inactiveWidgetPool = new Dictionary<string, AWidget>();
    private static Dictionary<string, AWidget> activeWidgets = new Dictionary<string, AWidget>();

    public static void RegisterStaticLayer(GameObject layer)
    {
        if (staticUiLayer == null)
        {
            staticUiLayer = layer;
        }
    }

    public static void RegisterDynamicLayer(GameObject layer)
    {
        if (dynamicUiLayer == null)
        {
            dynamicUiLayer = layer;
        }
    }

    public static void RegisterOverlayLayer(GameObject layer)
    {
        if (overlayUiLayer == null)
        {
            overlayUiLayer = layer;
        }
    }

    public static void ShowWidget(string widgetName, IWidgetProperties properties)
    {
        if (activeWidgets.ContainsKey(widgetName))
        {
            activeWidgets[widgetName].ApplyProperties(properties);
        }
        else if (inactiveWidgetPool.ContainsKey(widgetName))
        {
            AWidget widget = inactiveWidgetPool[widgetName];
            widget.ApplyProperties(properties);
            widget.Show();
            activeWidgets.Add(widgetName, widget);
            inactiveWidgetPool.Remove(widgetName);
        }
        else
        {
            GameObject asset = GetAsset(widgetName);
            AWidget assetWidget = asset.GetComponent<AWidget>();
            GameObject parentLayer;
            if (assetWidget.GetComponentType() == UiComponentType.Static)
            {
                parentLayer = staticUiLayer;
            }
            else if (assetWidget.GetComponentType() == UiComponentType.Dynamic)
            {
                parentLayer = dynamicUiLayer;
            }
            else if (assetWidget.GetComponentType() == UiComponentType.Overlay)
            {
                parentLayer = overlayUiLayer;
            }
            else
            {
                Debug.LogError("Layer has not been defined for Widget: " + assetWidget.name);
                return;
            }

            AWidget newWidget = GameObject.Instantiate(asset, parentLayer.transform).GetComponent<AWidget>();
            newWidget.Open();
            newWidget.ApplyProperties(properties);
            activeWidgets.Add(widgetName, newWidget);
        }
    }

    public static void HideWidget(string widgetName)
    {
        if (activeWidgets.ContainsKey(widgetName))
        {
            AWidget activeWidget = activeWidgets[widgetName];
            activeWidget.Hide();
            activeWidgets.Remove(widgetName);
            inactiveWidgetPool.Add(widgetName, activeWidget);
        }
    }

    public static void HideWidget(string[] widgetNames)
    {
        for (var i = 0; i < widgetNames.Length; i++)
        {
            string widgetName = widgetNames[i];
            HideWidget(widgetName);
        }
    }

    public static GameObject GetAsset(string assetPath)
    {
        if (loadedAssets.ContainsKey(assetPath))
        {
            return loadedAssets[assetPath];
        }
        else
        {
            GameObject newAsset = Resources.Load<GameObject>(assetPath);
            loadedAssets.Add(assetPath, newAsset);
            return newAsset;
        }
    }
}