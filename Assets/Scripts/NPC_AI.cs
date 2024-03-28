using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_AI : MonoBehaviour
{
    private NavMeshAgent _agent;
    [SerializeField] public Transform _player;
    public LayerMask ground, player;
    public Vector3 destinationPoint;
    private bool destPointSet;
    public float walkPointRange;
    private bool alreadyAttacked;
    public GameObject _bullet;
    public float timeBetweenAttacks;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // Player g�r�� alan�mda m�?
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        // Player sald�r� alan�nda m�?
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        // Patrol - Chase - Attack

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    void Patrolling()
    {
        if (!destPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            _agent.SetDestination(destinationPoint);
        }
        Vector3 disyenceToDestPoint = transform.position - destinationPoint;
        if (disyenceToDestPoint.magnitude < 1.0f)
        {
            // Player'a �ok yakla�t�k.
            destPointSet = false;
        }
    }
    void SearchWalkPoint()
    {
        //eksi ve art� y�nde gidebilir.
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        // Yeni destionation.
        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            //E�er gitmeye �al��t���m nokta ground'da ise;
            destPointSet = true;
        }
    }


    void ChasePlayer()
    {
        _agent.SetDestination(_player.position);
    }
    void AttackPlayer()
    {
        _agent.SetDestination(transform.position);
        transform.LookAt(_player);
        if (!alreadyAttacked)
        {
            //sald�r� an�nda bullet f�rlat�lacak
            // Quaternion.identity => �retilecek nesnenin prefab�n�n rotasyonunu referans al�r. Bizim �rne�imizde bu bullet.
            // rb ile instantiate edilmi� gameobject'in rigidbody'si tutuluyor.
            Rigidbody rb = Instantiate(_bullet, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);//ileri
            rb.AddForce(transform.up * 7f, ForceMode.Impulse);//yukar�
            StartCoroutine(ChangeTag(rb.gameObject));
            StartCoroutine(DestroyBullet(rb.gameObject));
            alreadyAttacked = true;

            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    IEnumerator ChangeTag(GameObject gameObject)
    {
        yield return new WaitForSeconds(2);
        gameObject.tag = "Untagged";
    }

    IEnumerator DestroyBullet(GameObject gameObject)
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    void ResetAttack()
    {
        //s�rekli olarak ate� edilmesini �nlemek i�in.
        alreadyAttacked = false;
    }


    //Basit �rnek
    /*
    [SerializeField] private GameObject destinationPoint;
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _agent.SetDestination(destinationPoint.transform.position);
    }
    */


}
