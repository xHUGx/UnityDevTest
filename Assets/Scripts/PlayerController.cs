using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] public GameObject pistolMuzzle;
    private Rigidbody bulletRb;
    private Animator anim;
   
    private int current;
    private float WPradius = 1;
    public float speed;
    private int stage;
    [SerializeField] private TextMeshProUGUI moveText;
    

    private void Start()
    {
        current = 1;
        stage = 1;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
       switch (stage)
        {
            case 0:
                NextStage();
                break;
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

    public void Shooting(int targetKills)
    {
        anim.SetFloat("Speed",0.5f);
        GameObject pooledProjectile = BulletsPooler.SharedInstance.GetPooledObject();
        Rigidbody bulletRb = pooledProjectile.GetComponent<Rigidbody>();
       
        if (Input.GetButtonDown("Fire1") && (EnemyHealth.killed<targetKills))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (pooledProjectile != null)
            {
                pooledProjectile.transform.position = pistolMuzzle.transform.position;
                pooledProjectile.SetActive(true);
                bulletRb.AddForce(ray.direction * 400f , ForceMode.Acceleration);
            }
        }
        if (EnemyHealth.killed == targetKills)
        {
            NextStage();
        }
    }

    void NextStage()
    {
        moveText.gameObject.SetActive(true);
        if (Input.GetButton("Fire1"))
        {
            stage++;
            moveText.gameObject.SetActive(false);
        }
    }
}
