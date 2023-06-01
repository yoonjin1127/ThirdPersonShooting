using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [SerializeField] Gun gun;

    // List<Gun> gunList = new List<Gun>();

    public void Fire()
    {
        Debug.Log("Fire");
        gun.Fire();
    }

    /* public void GetWeapon(Gun gun)
    {
        gunList.Add(gun)
    } */
}
