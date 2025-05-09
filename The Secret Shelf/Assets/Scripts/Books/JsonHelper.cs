using UnityEngine;

/// <summary>
/// JsonHelper permite deserializar arrays JSON en Unity usando JsonUtility,
/// que por defecto no soporta arrays o listas en la raíz del JSON.
/// Este helper envuelve el array dentro de un objeto temporal ("items")
/// para que pueda ser leído correctamente por Unity.
/// </summary>
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        // Encapsulamos el JSON en una estructura que permita que Unity lo lea correctamente
        string wrappedJson = "{\"items\":" + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(wrappedJson);
        return wrapper.items;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] items;
    }
}