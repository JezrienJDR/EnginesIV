using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Follow_State : ZombieState
{

    private GameObject followTarget;
    private const float StopDistance = 1.0f;

    public Zombie_Follow_State(ZombieComponent zom, StateMachine sm, GameObject ft) : base(zom, sm)
    {
        followTarget = ft;
        UpdateInterval = 2.0f;
    }
    public override void Start()
    {
        base.Start();
        owningZombie.agent.SetDestination(followTarget.transform.position);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();

        owningZombie.agent.SetDestination(followTarget.transform.position);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        owningZombie.animr.SetFloat("MovementSpeed", owningZombie.agent.velocity.normalized.z);

        if(Vector3.Distance(owningZombie.transform.position, followTarget.transform.position) < StopDistance)
        {
            machine.PrintStates();
            //Debug.Log(machine.states);
            Debug.Log("Attacking!");
            machine.ChangeState("ATTACK");
        }
    }
}
