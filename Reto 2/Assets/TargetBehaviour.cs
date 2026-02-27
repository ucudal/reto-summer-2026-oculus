using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    public float visibleTime = 2f;
    public float respawnDelay = 1f;
    public float startDelay = 0f;

    public Vector3 areaMin;
    public Vector3 areaMax;

    private Renderer rend;
    private Collider col;

    void Start()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        Invoke(nameof(StartLoop), startDelay);
    }

    void StartLoop()
    {
        StartCoroutine(TargetLoop());
    }

    System.Collections.IEnumerator TargetLoop()
    {
        while (true)
        {
            // aparecer en posición random
            Vector3 randomPos = new Vector3(
                UnityEngine.Random.Range(areaMin.x, areaMax.x),
                UnityEngine.Random.Range(areaMin.y, areaMax.y),
                UnityEngine.Random.Range(areaMin.z, areaMax.z)
            );

            transform.position = randomPos;

            rend.enabled = true;
            col.enabled = true;

            // tiempo visible
            yield return new WaitForSeconds(visibleTime);

            // desaparecer
            rend.enabled = false;
            col.enabled = false;

            // espera antes de reaparecer
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    public void Hit()
    {
        UnityEngine.Debug.Log("Hit!");

        StopAllCoroutines();

        rend.enabled = false;
        col.enabled = false;

        Invoke(nameof(RestartLoop), respawnDelay);
    }

    void RestartLoop()
    {
        StartCoroutine(TargetLoop());
    }
}