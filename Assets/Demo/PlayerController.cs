using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform weaponSpace;
    private Weapon _currentWeapon; 
    [SerializeField] private Weapon[] weapons;
    
    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private TextMeshProUGUI moveText;
    private Rigidbody bulletRb;
    private Animator anim;
   
    private int currentWP;
    private float WPradius = 1;
    public float speed;
    private int stage;
    
    private void Start()
    {
        currentWP = 1;
        stage = 0;
        anim = GetComponentInChildren<Animator>();

        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        _currentWeapon = Instantiate(weapons[0], weaponSpace);
        _currentWeapon.transform.localPosition = Vector3.zero;
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
        if (Vector3.Distance(waypoints[currentWP].transform.position, transform.position) < WPradius)
        {
            currentWP++;
            if (currentWP == targetPoint)
            {
                stage++;
            }
            else {Moving(targetPoint);}
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWP].transform.position,
                Time.deltaTime * speed);
        }
    }

    public void Shooting(int targetKills)
    {
        anim.SetFloat("Speed",0.5f);
        
        if (Input.GetButtonDown("Fire1") && (EnemyHealth.killed<targetKills))
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            _currentWeapon.Shoot(ray.direction);
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
