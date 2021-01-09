using UnityEngine.AI;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public Transform[] points;
    private NavMeshAgent nav;
    private int destPoint;

    private float distanceStopFromPlayer = 2f;

    public GameObject player;


    public EnemyBrain enemyBrain;

    // Start is called before the first frame update
    void Start()
    {
        enemyBrain = GetComponent<EnemyBrain>();
        nav = GetComponent<NavMeshAgent>();
        nav.destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs((gameObject.transform.root.position - player.transform.position).magnitude) < distanceStopFromPlayer)
        {
            nav.destination = gameObject.transform.root.position;
            if (enemyBrain.CanAttack())
            {
                enemyBrain.Attack();
            }
            Debug.Log("I am happy here");
            Debug.Log(Mathf.Abs((gameObject.transform.root.position - player.transform.position).magnitude));
        }
        else
        {
            if (Mathf.Abs((nav.destination - player.transform.position).magnitude) > distanceStopFromPlayer)
            {
                nav.destination = player.transform.position;
            }
        }
    }
}
