using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(StateMachine))]
public class ZombieComponent : MonoBehaviour
{
    public NavMeshAgent agent { get; private set; }

    public Animator animr { get; private set; }

    public StateMachine sm { get; private set; }

    public GameObject followTarget;

    public bool _Debug;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animr = GetComponent<Animator>();

        sm = GetComponent<StateMachine>();
        //sm.Initialize("IDLE");

        Initialize(followTarget);
    }


    // Start is called before the first frame update
    void Start()
    {
        if(_Debug)
        {
            Initialize(followTarget);
        }
    }


    public void Initialize(GameObject ft)
    {
        followTarget = ft;

        Zombie_Idle_State idleState = new Zombie_Idle_State(this, sm);
        sm.AddState("IDLE", idleState);

        Zombie_Follow_State followState = new Zombie_Follow_State(this, sm, followTarget);
        sm.AddState("FOLLOW", followState);

        Zombie_Attack_State attackState = new Zombie_Attack_State(this, sm, followTarget);
        sm.AddState("ATTACK", attackState);

        Zombie_Death_State deathState = new Zombie_Death_State(this, sm);
        sm.AddState("DEATH", deathState);


        sm.PrintStates();

        sm.Initialize("FOLLOW");



    }

}
