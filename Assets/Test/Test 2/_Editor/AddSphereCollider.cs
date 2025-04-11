using UnityEngine;
using UnityEditor;

public class AddSphereCollider : EditorWindow
{
    [MenuItem("Tools/Add SphereCollider")]
    static void RemoveSphereColliders()
    {
        // Lấy tất cả các đối tượng được chọn trong Editor
        GameObject[] selectedObjects = Selection.gameObjects;

        // Duyệt qua tất cả các đối tượng được chọn
        foreach (GameObject obj in selectedObjects)
        {
            // Lấy tất cả các component SphereCollider trong đối tượng này và các đối tượng con
            obj.AddComponent<SphereCollider>();
        }

        Debug.Log("Added SphereCollider");
    }
}
