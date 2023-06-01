using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보관될 객체

public class Poolable : MonoBehaviour
{
    // 몇 초 지나고 반납
    [SerializeField] float releaseTime;

    private ObjectPool pool;
    public ObjectPool Pool { get { return pool; } set { pool = value; } }

    private void OnEnable()
    {
        // 시작하자마자 몇 초 뒤에 반납 진행
        StartCoroutine(ReleaseTimer());
    }

    IEnumerator ReleaseTimer()
    {
        yield return new WaitForSeconds(releaseTime);
        pool.Release(this);
    }
}
