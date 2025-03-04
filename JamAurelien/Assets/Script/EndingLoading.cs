using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Splines;

public class EndingLoading : MonoBehaviour
{
    private Rigidbody rb;
    private FollowSpline followSpline;
    [SerializeField] private Vector3 jumpDir;
    [SerializeField] private float jumpForce;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        followSpline = GetComponent<FollowSpline>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPos") == true)
        {
            followSpline.CanFollowSpline = false;
            
            Vector3 worldJumpDirection = transform.TransformDirection(jumpDir);
            rb.AddForce(worldJumpDirection * jumpForce, ForceMode.Impulse);
        }

        if (other.CompareTag("End") == true)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
