using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 보관소 역할

public class ObjectPool : MonoBehaviour
{
    [SerializeField] Poolable poolablePrefab;

    [SerializeField] int poolSize;
    [SerializeField] int maxSize;

    private Stack<Poolable> objectPool = new Stack<Poolable>();

    private void Awake()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        for (int i = 0; i <poolSize; i++)
        {
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            poolable.Pool = this;
            objectPool.Push(poolable);
        }
    }

    public Poolable Get()
    {
        // 대여
        if (objectPool.Count > 0)
        {
            Poolable poolable = objectPool.Pop();
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            // 남은 게 없는 경우 생성해서 사용
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(Poolable poolable)
    {
        // 반납
        // 반납량이 최대사이즈를 넘지 않는 경우에는 반납
        if (objectPool.Count < maxSize)
        {
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
        else
        {
            // 반납량이 최대사이즈를 넘는 경우에는 삭제
            Destroy(poolable.gameObject);
        }
    }
}
