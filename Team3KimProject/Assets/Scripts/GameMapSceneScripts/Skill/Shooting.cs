using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject projectile;
    public float speed = 130f;
    public Vector3 direction;
    public Vector3 myPosition;
    public GameObject target;
    public delegate void ShootingProjectile();
    public static event ShootingProjectile SlowMotion;



    private void OnEnable()
    {
        AttackAHandler.OnShoot += CreateProjectile;
    }

    private void OnDisable()
    {
        AttackAHandler.OnShoot -= CreateProjectile;
    }

    private void CreateProjectile(GameObject shootingUnit,GameObject target,int selectedAttack)
    {
        if (shootingUnit.GetComponent<Unit>().projectiles != null)
        {
            this.projectile = shootingUnit.GetComponent<Unit>().projectiles[selectedAttack];
            
        }
        this.target = target;
        myPosition = shootingUnit.transform.position;   
        direction = target.transform.position - myPosition;
        GameObject myProjectile = Instantiate(projectile, myPosition+(shootingUnit.transform.forward*1.3f), Quaternion.identity);
        
        //Debug.Log("I am shooting");
        myProjectile.transform.rotation = Quaternion.LookRotation(direction);
        myProjectile.GetComponent<Rigidbody>().velocity = transform.TransformDirection(direction.normalized * speed);//.AddForce(direction * speed);
        shootingUnit.transform.rotation = shootingUnit.GetComponent<AttackAHandler>().currentRotation;
        SlowMotion();

    }
    
}
