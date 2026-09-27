using UnityEngine;
using UnityEditor;

public class AddMeshColliders
{
    [MenuItem("Tools/Treasure Hunt/Add Mesh Colliders To Selected")]
    public static void AddColliders()
    {
        GameObject selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            Debug.LogWarning(
                "Hãy chọn GameObject chứa map trước khi chạy công cụ."
            );

            return;
        }

        MeshFilter[] meshFilters =
            selectedObject.GetComponentsInChildren<MeshFilter>(
                true
            );

        int addedCount = 0;
        int skippedCount = 0;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == null)
            {
                skippedCount++;
                continue;
            }

            MeshCollider meshCollider =
                meshFilter.GetComponent<MeshCollider>();

            if (meshCollider == null)
            {
                meshCollider =
                    Undo.AddComponent<MeshCollider>(
                        meshFilter.gameObject
                    );

                addedCount++;
            }
            else
            {
                skippedCount++;
            }

            meshCollider.sharedMesh = meshFilter.sharedMesh;

            meshCollider.convex = false;
            meshCollider.isTrigger = false;
        }

        Debug.Log(
            $"Đã xử lý map: {selectedObject.name}\n" +
            $"Mesh Collider được thêm: {addedCount}\n" +
            $"Đã có / bỏ qua: {skippedCount}"
        );
    }

    [MenuItem(
        "Tools/Treasure Hunt/Add Mesh Colliders To Selected",
        true
    )]
    public static bool ValidateAddColliders()
    {
        return Selection.activeGameObject != null;
    }
}