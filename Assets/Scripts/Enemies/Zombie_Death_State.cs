using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Death_State : ZombieState
{
    public Zombie_Death_State(ZombieComponent zom, StateMachine sm) : base(zom, sm)
    {
    }

    public override void Start()
    {
        base.Start();

        owningZombie.agent.isStopped = true;
        owningZombie.agent.ResetPath();

        owningZombie.animr.SetFloat("MovementSpeed", 0.0f);
        owningZombie.animr.SetBool("isDead", true);
    }

    public override void Exit()
    {
        base.Exit();
        owningZombie.agent.isStopped = false;   
        owningZombie.animr.SetBool("isDead", false);
    }

}
