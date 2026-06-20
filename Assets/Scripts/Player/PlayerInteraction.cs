using UnityEngine;

public class PlayerInteraction:MonoBehaviour
{
    [SerializeField] private Transform _interactPoint;
    [SerializeField] private float _interactRadius = 1f;
    [SerializeField] private LayerMask _interactableLayer;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            _interactPoint.position,
            _interactRadius,
            _interactableLayer
        );

        if (hit == null)
            return;

        if (hit.TryGetComponent<IInteractable>(out var interactable))
        {
            interactable.Interact();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_interactPoint == null)
            return;

        Gizmos.DrawWireSphere(
            _interactPoint.position,
            _interactRadius
        );
    }
}