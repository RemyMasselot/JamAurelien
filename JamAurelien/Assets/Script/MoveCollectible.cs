using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCollectible : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private GameObject prefab;
    public bool ColletibleSaw = false;

    public void Movement(Vector3 targetPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        //Debug.Log("donut is coming");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
