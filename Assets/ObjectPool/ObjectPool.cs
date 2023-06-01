using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ������ ����

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
        // �뿩
        if (objectPool.Count > 0)
        {
            Poolable poolable = objectPool.Pop();
            poolable.gameObject.SetActive(true);
            poolable.transform.parent = null;
            return poolable;
        }
        else
        {
            // ���� �� ���� ��� �����ؼ� ���
            Poolable poolable = Instantiate(poolablePrefab);
            poolable.Pool = this;
            return poolable;
        }
    }

    public void Release(Poolable poolable)
    {
        // �ݳ�
        // �ݳ����� �ִ����� ���� �ʴ� ��쿡�� �ݳ�
        if (objectPool.Count < maxSize)
        {
            poolable.gameObject.SetActive(false);
            poolable.transform.SetParent(transform);
            objectPool.Push(poolable);
        }
        else
        {
            // �ݳ����� �ִ����� �Ѵ� ��쿡�� ����
            Destroy(poolable.gameObject);
        }
    }
}
