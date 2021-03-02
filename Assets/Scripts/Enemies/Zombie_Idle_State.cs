using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Idle_State : ZombieState
{

    public Zombie_Idle_State(ZombieComponent zom, StateMachine sm) : base(zom, sm)
    {
    }

    public override void Start()
    {
        base.Start();

        owningZombie.agent.isStopped = true;
        owningZombie.agent.ResetPath();

        owningZombie.animr.SetFloat("MovementSpeed", 0.0f);
    }



}
