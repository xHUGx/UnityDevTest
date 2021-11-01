using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    public GameObject[] waypoints;
    public GameObject projectilePrefab;
    private Rigidbody bulletRb;
    private Animator anim;
   
    private int current = 0;
    private float WPradius = 1;
    public float speed;
    private int stage = 1;
    private int killed = 0;
    

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        switch (stage)
        {
            case 1:
                Moving(3);
                break;
            case 2: 
                Shooting(2);
                break;
            case 3:
                Moving(5);
                break;
            case 4:
                Shooting(5);
                break;
            case 5:
                Moving(7);
                break;
            case 6:
                Shooting(6);
                break;
            case 7:
                Moving(8);
                break;
            case 8:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
                break;
        }


    }

    private void Moving(int targetPoint)
    {
        anim.SetFloat("Speed", 1f);
        if (Vector3.Distance(waypoints[current].transform.position, transform.position) < WPradius)
        {
            current++;
            if (current == targetPoint)
            {
                stage++;
            }
            else {Moving(targetPoint);}
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[current].transform.position,
                Time.deltaTime * speed);
        }
    }

    public void Shooting(int TargetKils)
    {
        anim.SetFloat("Speed",0.5f);
        if (Input.GetButtonDown("Fire1"))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            
            RaycastHit hitInfo;

            GameObject pooledProjectile = BulletsPooler.SharedInstance.GetPooledObject();
            Rigidbody bulletRb = pooledProjectile.GetComponent<Rigidbody>();
            if (pooledProjectile != null)
            {
                pooledProjectile.SetActive(true);
                Debug.Log(ray);
                if (Physics.Raycast(ray, out hitInfo))
                {
                    Debug.Log(hitInfo.transform.name);
                    if (hitInfo.transform.CompareTag("Enemy"))
                    {
                        hitInfo.collider.GetComponent<EnemyHealth>().TakeDamage(50);
                        if (hitInfo.collider.GetComponent<EnemyHealth>().health == 0)
                        {
                            killed++;
                        }
                        if (killed == TargetKils)
                        {
                            stage++;
                        }
                    }
                    bulletRb.AddForce(ray.direction * 30f , ForceMode.Impulse);
                }
            }
        }

    }
    
    
}
