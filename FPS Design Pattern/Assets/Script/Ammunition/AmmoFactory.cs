using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class AmmoFactory : MonoBehaviour
{
    [SerializeField] GameObject[] _ammo;
    public GameObject CreateRandomAmmo(Vector3 position)
    {
        int randomIndex = Random.Range(0, _ammo.Length);
        GameObject ammoModel = null; 

        switch (randomIndex)
        {
            case 0:
                ammoModel = _ammo[0];
                break;
            case 1:
                ammoModel = _ammo[1];
                break;
            case 2:
                ammoModel = _ammo[2];
                break;
            default:
                return null;
        }
        if (ammoModel != null)
        {
            GameObject ammoObject = Instantiate(ammoModel, position, Quaternion.identity);
            return ammoObject;
        }
        return null;
    }
}
