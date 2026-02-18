using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

/// <summary>
/// XRGrabInteractable que fuerza el snap a la mano al hacer Force Grab con Ray Interactor.
/// Reemplaza al XRGrabInteractable normal en el objeto (este script YA ES uno).
///
/// Cómo funciona: en LateUpdate (después de que XRI termine todo su procesamiento),
/// fuerza la posición del objeto para que su Attach Transform coincida con la mano.
/// </summary>
public class ForceGrabToHand : XRGrabInteractable
{
    [Header("Force Snap Settings")]
    [Tooltip("Fuerza snap a la mano al hacer force grab con Ray Interactor.")]
    [SerializeField] private bool forceSnapToHand = true;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (forceSnapToHand)
            useDynamicAttach = false;

        base.OnSelectEntered(args);
    }

    /// <summary>
    /// LateUpdate corre DESPUÉS de todo el pipeline de XRI (ProcessInteractor → ProcessInteractable).
    /// El XRRayInteractor posiciona el objeto al final del rayo en su ProcessInteractor,
    /// y no hay forma confiable de interceptarlo antes. Así que lo dejamos hacer lo suyo
    /// y luego nosotros reposicionamos el objeto a la mano.
    /// </summary>
    private void LateUpdate()
    {
        if (!forceSnapToHand || !isSelected)
            return;

        foreach (var interactor in interactorsSelecting)
        {
            if (interactor is XRRayInteractor)
            {
                SnapToHand(interactor.transform);
                break;
            }
        }
    }

    /// <summary>
    /// Posiciona el objeto de modo que su Attach Transform quede exactamente
    /// en la posición de la mano del interactor.
    /// </summary>
    private void SnapToHand(Transform hand)
    {
        if (attachTransform != null && attachTransform != transform)
        {
            // Calcular offset local del attach respecto al objeto
            // (funciona aunque el attach sea un hijo anidado)
            Vector3 localAttachPos = transform.InverseTransformPoint(attachTransform.position);
            Quaternion localAttachRot = Quaternion.Inverse(transform.rotation) * attachTransform.rotation;

            // Rotar el objeto para que su attach coincida con la rotación de la mano
            transform.rotation = hand.rotation * Quaternion.Inverse(localAttachRot);

            // Mover el objeto para que su attach coincida con la posición de la mano
            transform.position = hand.position - transform.rotation * localAttachPos;
        }
        else
        {
            transform.position = hand.position;
            transform.rotation = hand.rotation;
        }
    }
}