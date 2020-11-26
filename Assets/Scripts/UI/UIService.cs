using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public static class UIService
{
    private static GameObject staticUiLayer;
    private static GameObject dynamicUiLayer;
    private static GameObject overlayUiLayer;
    private static Dictionary<AssetReference, GameObject> loadedAssets = new Dictionary<AssetReference, GameObject>();
    private static Dictionary<AssetReference, AWidget> inactiveWidgetPool = new Dictionary<AssetReference, AWidget>();
    private static Dictionary<AssetReference, AWidget> activeWidgets = new Dictionary<AssetReference, AWidget>();

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

    public static async Task<T> ShowWidget<T>(AssetReference assetReference, IWidgetProperties properties)
        where T : AWidget
    {
        if (activeWidgets.ContainsKey(assetReference))
        {
            AWidget returnWidget = activeWidgets[assetReference];
            returnWidget.ApplyProperties(properties);
            return (T) returnWidget;
        }
        else if (inactiveWidgetPool.ContainsKey(assetReference))
        {
            AWidget widget = inactiveWidgetPool[assetReference];
            widget.ApplyProperties(properties);
            widget.Show();
            activeWidgets.Add(assetReference, widget);
            inactiveWidgetPool.Remove(assetReference);
            return (T) widget;
        }
        else
        {
            GameObject result = await assetReference.LoadAssetAsync<GameObject>().Task;
            AWidget assetWidget = result.GetComponent<AWidget>();
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
                return null;
            }

            AWidget newWidget = GameObject.Instantiate(assetWidget, parentLayer.transform);
            newWidget.Open();
            newWidget.ApplyProperties(properties);
            activeWidgets.Add(assetReference, newWidget);
            return (T) newWidget;
        }
    }

    public static void HideWidget(AssetReference assetReference)
    {
        if (activeWidgets.ContainsKey(assetReference))
        {
            AWidget activeWidget = activeWidgets[assetReference];
            if (activeWidget != null)
            {
                activeWidget.Hide();
                activeWidgets.Remove(assetReference);
                inactiveWidgetPool.Add(assetReference, activeWidget);
            }
        }
    }

    public static void HideWidget(AssetReference[] assetReferences)
    {
        for (var i = 0; i < assetReferences.Length; i++)
        {
            AssetReference assetReference = assetReferences[i];
            HideWidget(assetReference);
        }
    }
}