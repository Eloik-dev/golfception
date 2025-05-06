using UnityEditor;
using UnityEngine;

/**
 * Inspiré de https://www.youtube.com/watch?v=UMaJYYMLHsI
 */
[ExecuteInEditMode]
public class LockToGrid : MonoBehaviour
{
    [SerializeField] float tileSize = 1;
    [SerializeField] Vector3 tileOffset = Vector3.zero;

    [SerializeField] bool activateX = true;
    [SerializeField] bool activateY = true;
    [SerializeField] bool activateZ = true;


    // Update is called once per frame
    void Update()
    {
        if (EditorApplication.isPlaying) return;

        Vector3 currentPosition = transform.position;

        float snappedX = activateX ? (Mathf.Round(currentPosition.x / tileSize) * tileSize + tileOffset.x) : currentPosition.x;
        float snappedY = activateY ? (Mathf.Round(currentPosition.y / tileSize) * tileSize + tileOffset.y) : currentPosition.y;
        float snappedZ = activateZ ? (Mathf.Round(currentPosition.z / tileSize) * tileSize + tileOffset.z) : currentPosition.z;

        Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
        transform.position = snappedPosition;
    }
}
