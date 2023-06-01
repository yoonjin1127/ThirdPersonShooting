using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class Gun : MonoBehaviour
{
    [SerializeField] ParticleSystem hitEffect;
    [SerializeField] ParticleSystem muzzleEffect;
    [SerializeField] TrailRenderer bulletTrail;
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxDistance;
    [SerializeField] int damage;
    
    public virtual void Fire()
    {
        Debug.Log("Fire");
        muzzleEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, maxDistance))
        {
            // Destroy(hit.transform.gameObject);

            // 인터페이스는 GetComponent에도 동작시킬 수 있다
            IHitable hitable = hit.transform.GetComponent<IHitable>();
            // 파티클 시스템
            ParticleSystem effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            effect.transform.parent = hit.transform;
            Destroy(effect.gameObject, 3f);

            TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail, muzzleEffect.transform.position, hit.point));
            Destroy(trail.gameObject, 3f);

            hitable?.Hit(hit, damage);
            /* if (hitable != null)
                 hitable.Hit(hit, damage); */
        }
        else
        {
            TrailRenderer trail = Instantiate(bulletTrail, muzzleEffect.transform.position, Quaternion.identity);
            StartCoroutine(TrailRoutine(trail, muzzleEffect.transform.position, Camera.main.transform.forward * maxDistance));
            Destroy(trail.gameObject, 3f);
        }
    }
    IEnumerator TrailRoutine(TrailRenderer trail, Vector3 startPoint, Vector3 endPoint)
    {
        Debug.Log("Trail");
        float totalTime = Vector2.Distance(startPoint, endPoint) / bulletSpeed;

        float rate = 0;

        while (rate < 1)
        {
            trail.transform.position = Vector3.Lerp(startPoint, endPoint, rate);
            rate += Time.deltaTime / totalTime;

            yield return null;
        }
    }
}
