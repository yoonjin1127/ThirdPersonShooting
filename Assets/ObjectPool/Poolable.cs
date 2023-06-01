using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ��ü

public class Poolable : MonoBehaviour
{
    // �� �� ������ �ݳ�
    [SerializeField] float releaseTime;

    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }

    private void OnEnable()
    {
        // �������ڸ��� �� �� �ڿ� �ݳ� ����
        StartCoroutine(ReleaseTimer());
    }

    IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }
}
