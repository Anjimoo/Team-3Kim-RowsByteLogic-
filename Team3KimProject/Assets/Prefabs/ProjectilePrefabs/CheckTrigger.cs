using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    [SerializeField]
    GameObject poof;
    [SerializeField]
    GameObject Shooting;

    public delegate void ShootingProjectile();
    public static event ShootingProjectile SlowOnExplosionMotion;

    private void Start()
    {
        Shooting = GameObject.Find("Shoot");
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject == Shooting.GetComponent<Shooting>().target)
        {
            
            poof = Instantiate(poof, other.transform.position, Quaternion.identity);
            SlowOnExplosionMotion();
            Destroy(poof, 2f);
            Destroy(gameObject);
            GameObject.Find("TurnsHandler").GetComponent<TurnsHandler>().unitsInQueue.Peek().GetComponent<AttackAHandler>().InvokeAttack();
        }
    }


}

