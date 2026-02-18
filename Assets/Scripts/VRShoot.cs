using UnityEngine;

public class VRShoot : MonoBehaviour
{
    public Transform muzzle;
    public float range = 100f;
    public LayerMask hitLayers;

    void Update()
    {
        // Disparo con mouse (para probar sin VR)
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (muzzle == null)
        {
            Debug.LogWarning("No hay muzzle asignado");
            return;
        }

        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        Debug.DrawRay(muzzle.position, muzzle.forward * range, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, range, hitLayers))
        {
            Debug.Log("Impacto en: " + hit.collider.name);
        }
        else
        {
            Debug.Log("Disparo sin impacto");
        }
    }
}
