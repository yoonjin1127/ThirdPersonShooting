using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IHitable
{
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Hit(RaycastHit hit, int damage)
    {
        // 공격받으면 이 게임오브젝트 삭제
        // Destroy(gameObject);

        if (rb != null)
        {
            rb?.AddForceAtPosition(-10 * hit.normal, hit.point, ForceMode.Impulse);
        }
    }
}
