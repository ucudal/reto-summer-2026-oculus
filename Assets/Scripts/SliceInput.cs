using UnityEngine;

public class SliceInput : MonoBehaviour
{
    private Vector3 mouseStart;
    private Vector3 mouseEnd;
    private bool isDragging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStart = Input.mousePosition;
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            mouseEnd = Input.mousePosition;
            isDragging = false;

            PerformSlice(mouseStart, mouseEnd);
        }
    }

    void PerformSlice(Vector3 start, Vector3 end)
    {
        if (Camera.main == null)
            return;

        Vector3 dragDirection = (end - start);

        if (dragDirection.magnitude < 10f)
            return; // evita micro cortes accidentales

        dragDirection.Normalize();

        Vector3 worldDragDir =
            Camera.main.transform.right * dragDirection.x +
            Camera.main.transform.up * dragDirection.y;

        Vector3 sliceNormal =
            Vector3.Cross(worldDragDir, Camera.main.transform.forward).normalized;

        Ray ray = Camera.main.ScreenPointToRay(start);

        RaycastHit[] hits = Physics.RaycastAll(ray, 100f);

        foreach (RaycastHit hit in hits)
        {
            Slice slice = hit.collider.GetComponent<Slice>();

            if (slice != null)
            {
                Vector3 sliceOrigin = slice.transform.position;
                slice.ComputeSlice(sliceNormal, sliceOrigin);
                break; // cortar solo uno
            }
        }
    }
}
