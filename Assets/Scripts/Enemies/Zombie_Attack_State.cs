using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Attack_State : ZombieState
{

    private GameObject followTarget;
    private float attackRange = 2.0f;

    public Zombie_Attack_State(ZombieComponent zom, StateMachine sm, GameObject ft) : base(zom, sm)
    {
        followTarget = ft;
        UpdateInterval = 2.0f;

    }
    public override void Start()
    {
        base.Start();
        owningZombie.agent.isStopped = true;
        owningZombie.agent.ResetPath();
        owningZombie.animr.SetFloat("MovementSpeed", 0.0f);
        owningZombie.animr.SetBool("isAttacking", true);
    }

    public override void IntervalUpdate()
    {
        base.IntervalUpdate();

        //TODO: add damage to target?????
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        owningZombie.transform.LookAt(followTarget.transform.position, Vector3.up);

        float distanceBetween = Vector3.Distance(owningZombie.transform.position, followTarget.transform.position);

        if(distanceBetween > attackRange)
        {
            machine.ChangeState("FOLLOW");
        }
        
        //TODO: die if health is <= 0;
    }

    public override void Exit()
    {
        base.Exit();
        owningZombie.animr.SetBool("isAttacking", false);

    }
}
