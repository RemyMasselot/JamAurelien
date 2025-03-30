using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class EndingLoading : MonoBehaviour
{
    private Rigidbody rb;
    private FollowSpline followSpline;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform jumpTarget;
    [SerializeField] private RawImage fade;


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
            
            Vector3 worldJumpDirection = (jumpTarget.position - transform.position).normalized;
            rb.AddForce(worldJumpDirection * jumpForce, ForceMode.Impulse);

            followSpline.UpdateAnim("Jump");
        }

        if (other.CompareTag("End") == true)
        {
            fade.DOFade(1, 0.5f).OnComplete(() =>
            {
                SceneManager.LoadScene("Level1");
            });
        }
    }
}
