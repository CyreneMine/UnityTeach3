using UnityEngine;
using UnityEngine.AI;

public class Lesson62 : MonoBehaviour
{
    private CharacterController cc;
    public float speed;
    private Animator animator;
    private NavMeshAgent agent;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                agent.SetDestination(hit.point);

        if (Input.GetMouseButtonUp(0))
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit,1000,1<< LayerMask.NameToLayer("Obstacle")))
                hit.collider.gameObject.SetActive(false);
        if (agent.velocity == Vector3.zero)
            animator.SetInteger("Speed", 0);
        else
            animator.SetInteger("Speed", 1);
    }
}
