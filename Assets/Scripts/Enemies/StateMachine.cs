using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public State currentState { get; private set; }

    public Dictionary<string, State> states;

    private bool running;


    public void PrintStates()
    {
        List<string> keyList = new List<string>(states.Keys);

        foreach (string s in keyList)
        {
            Debug.Log(s);
        }
    }

    private void Awake()
    {
        if (states == null)
        {
            states = new Dictionary<string, State>();
        }

        //Debug.Log("Awake");

        if(states== null)
        {
            Debug.Log("States NULL on AWAKE!!!!");
        }
    }

    public void Initialize(string startState)
    {
        if (states == null)
        {
            Awake();
        }

        if (states.ContainsKey(startState))
        {
            ChangeState(startState);
        }
        else if(states.ContainsKey("IDLE"))
        {
            ChangeState("IDLE");
        }
    }

    public void AddState(string nm, State newState)
    {
        //Debug.Log("ADDING STATE " + nm);

        if(states == null)
        {
            //Awake();
            //Debug.Log("States Dictionary NULL");
            states = new Dictionary<string, State>();
        }

        if (states.ContainsKey(nm))
        {
            return;
        }
        else
        {
            states.Add(nm, newState);
        }
    }

    public void RemoveState(string nm)
    {
        if(states.ContainsKey(nm))
        {
            states.Remove(nm);
        }
    }

    public void ChangeState(string nm)
    {
        if (running)
        {
            StopRunningState();
        }
        
        if (!states.ContainsKey(nm))
        {
            Debug.Log("STATE NOT FOUND");
            return;
        }
        
        currentState = states[nm];
        currentState.Start();

        if (currentState.UpdateInterval > 0)
        {
            InvokeRepeating(nameof(IntervalUpdate), 0.0f, currentState.UpdateInterval);
        }

        running = true;

    }

    public void StopRunningState()
    {
        running = false;

        currentState.Exit();

        CancelInvoke(nameof(IntervalUpdate));
    }

    private void Update()
    {
        if (running) { currentState.Update(); }
    }

    private void FixedUpdate()
    {
        if (running) { currentState.FixedUpdate(); }
    }

    private void IntervalUpdate()
    {
        if (running)
        {
            currentState.IntervalUpdate();
        }
    }
}


public class State
{
    protected StateMachine machine;

    public float UpdateInterval;

    protected State(StateMachine sm)
    {
        machine = sm;
    }

    public virtual void Start()
    {

    }

    public virtual void IntervalUpdate()
    {

    }

    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }

    public virtual void Exit()
    {

    }
}


public class ZombieState : State
{

    protected ZombieComponent owningZombie;
    public ZombieState(ZombieComponent zom, StateMachine sm): base(sm)
    {
        owningZombie = zom;
    }
}

enum ZomState
{
    IDLE,
    FOLLOW,
    ATTACK,
    DEATH
}