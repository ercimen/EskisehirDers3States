using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    [SerializeField] private Transform[] patrolPointList;

    [SerializeField] private Transform target;

    private NavMeshAgent _agent;

    private StateMachine _stateMachine;

    private Transform _transform;

    private Transform _playerTransform;

    private bool _isTargetPlayer;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += CheckGameStatus;
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= CheckGameStatus;
    }

    private void CheckGameStatus(GameState newState)
    {
        if (newState==GameState.Play)
        {
            _stateMachine.ChangeState(_stateMachine._patrolState);
        }
    }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _stateMachine = GetComponent<StateMachine>();
        _transform = transform;
        _stateMachine.ChangeState(_stateMachine._idleState);
    }

    private void Start()
    {
        _playerTransform = GameManager.Instance.PlayerTransform;
    }

    public void Chase()
    {
        target = _playerTransform;
        _agent.SetDestination(target.position);
    }

    public void Patrol()
    {

        if (target ==null)
        {
            int randomPoint = Random.Range(0, patrolPointList.Length);

            target = patrolPointList[randomPoint];

        }

        _agent.SetDestination(target.position);

        float distance = Vector3.Distance(_transform.position, target.position);

        if (distance<1.3f)
        {
            target = null;
        }
    }


    private void Update()
    {

        if (_playerTransform ==null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(_transform.position, _playerTransform.position);

        Debug.Log(distanceToPlayer);

        if (distanceToPlayer<=10f)
        {
            _isTargetPlayer = true;
            _stateMachine.ChangeState(_stateMachine._chaseState);
        }

        if (distanceToPlayer>=15f && _isTargetPlayer)
        {
            target = null;
            _isTargetPlayer = false;
            _stateMachine.ChangeState(_stateMachine._patrolState);

            Debug.Log("Change state : Patrol");
        }
    }



}
