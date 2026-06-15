using System.Collections.Generic;
using UnityEngine;

public class PlayerAwareness : MonoBehaviour
{

    [SerializeField] private float _nearDistance = 5f;
    [SerializeField] private LayerMask _obstacleLayer;
    private bool _isInlight;

    public bool IsAlone()
    {
        foreach (var player in PlayerController.Players)
        {
            if (player == null || player.gameObject == this.gameObject)
                continue;

            float distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance <= _nearDistance)
            {
                Vector2 direction = (player.transform.position - transform.position);
                RaycastHit2D hit= Physics2D.Raycast(transform.position, direction, distance, _obstacleLayer);
                if (hit.collider == null)
                {
                    return false; 
                }
            }
            
        }
        return true;
    }

    public bool IsInLight() => _isInlight;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("LighZone"))
        {
            _isInlight= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LighZone"))
        {
            _isInlight= false;
        }
    }

}