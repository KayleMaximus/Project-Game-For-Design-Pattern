using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _range = 500f, _damage = 30f;
    [SerializeField] Camera _FPcamera;
    [SerializeField] ParticleSystem _muzzleFlash;
    [SerializeField] GameObject _hitEffect;
    [SerializeField] Ammo _ammoSlot;
    [SerializeField] AmmoType _ammoType;
    [SerializeField] float _Cooldown = 0.5f;
    [SerializeField] bool _canShoot = true;
    //[SerializeField] TextMeshProUGUI _ammoText; 

    private void OnEnable()
    {
        _canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButtonDown(0) || _canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        //_ammoText.SetText(_ammoSlot.GetCurrentAmmo(_ammoType).ToString());
    }

    IEnumerator Shoot()
    {
        _canShoot = false;
        /*if (_ammoSlot.GetCurrentAmmo(_ammoType) > 0)
        {
            _ammoSlot.ReduceCurrentAmmo(_ammoType);
        }*/
            PlayMuzzleFlash();
            ProcessRaycast();
        yield return new WaitForSeconds(_Cooldown);
        _canShoot = true;

    }

    private void PlayMuzzleFlash()
    {
        _muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        //Cách 1: referrence tới 1 camera
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;
        if (Physics.Raycast(rayOrigin, out hitInfo, _range))
        {
            Debug.Log("I hit: " + hitInfo.transform.name);
            CreatHitImpact(hitInfo);
            EnemyHealth target = hitInfo.transform.GetComponent<EnemyHealth>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
        }

        //Cách 2: referrence tới nhiều camera
        /*RaycastHit hitInfo;
        Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hitInfo, _range);
        Debug.Log("I hit: " + hitInfo.transform.name);*/
    }

    private void CreatHitImpact(RaycastHit hitInfo)
    {
        GameObject impact = Instantiate(_hitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
        Destroy(impact, .1f);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }

}
