using UnityEngine;
using UnityEngine.InputSystem;

public class VRGun : MonoBehaviour
{
    public Transform muzzle;

    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(muzzle.position, muzzle.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Le pegaste a " + hit.collider.name);
        }
    }
}
