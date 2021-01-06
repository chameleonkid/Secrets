using UnityEngine;

public static class UnityExtensions {
    /// <summary>
    /// Tries to get the first matching component attached to a child object, otherwise returns the component attached to the parent.
    /// </summary>
    /// <remarks>
    /// Uses a for loop to check through GetComponentsInChildren, so minimise usage where possible.
    /// Usage of this method is unnecessary if the parent does not have the component attached.
    /// </remarks>
    public static T GetComponentInChildrenFirst<T>(this Component parent) where T : Component {
        var attached = parent.GetComponent<T>();
        var children = parent.GetComponentsInChildren<T>();
        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != attached)
            {
                return children[i];
            }
        }
        return attached;
    }

    public static T GetRandomElement<T>(this T[] array) => array[Random.Range(0, array.Length)];
}
