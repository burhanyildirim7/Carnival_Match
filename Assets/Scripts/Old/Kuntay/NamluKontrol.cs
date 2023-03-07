using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamluKontrol : MonoBehaviour
{
    [SerializeField] GameObject _projectile, _fireFX;

    private GameObject _tempMermi, _tempfireFX;

    public void FireProjectile(GameObject hedef)
    {
        _tempMermi = Instantiate(_projectile, transform);
        _tempMermi.transform.parent = null;
        _tempMermi.transform.GetComponent<Projectile>()._hedef = hedef;
        _tempfireFX = Instantiate(_fireFX, transform);
    }


}
