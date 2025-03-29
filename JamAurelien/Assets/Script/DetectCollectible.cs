using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollectible : MonoBehaviour
{
    public float detectionRadius = 2f;
    public LayerMask collectableLayer;

    void Update()
    {
        DetectCollectables();
    }

    void DetectCollectables()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, collectableLayer);

        foreach (Collider hit in hits)
        {
            MoveCollectible ui = hit.gameObject.GetComponent<MoveCollectible>();
            if (ui != null)
            {
                ui.Movement(transform.position);
            }
            //Debug.Log("Collectable détecté : " + hit.name);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
