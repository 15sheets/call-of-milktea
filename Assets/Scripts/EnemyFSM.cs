using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// interface for fsm states
/// </summary>
public interface IState
{
    public void Enter()
    {
        // code that runs upon entering the state
    }

    public void Update()
    {
        // code that runs in update. include transitions to new states
    }

    public void FixedUpdate()
    {
        // code that runs in fixedupdate. physics stuff.
    }

    public void Exit()
    {
        // code that runs on exiting the state
    }
}


// FSM for enemy movement
[Serializable]
public class EnemyFSM
{
    public IState currState { get; private set; }

    // references to state objects
    public IdleState idleState;
    public ChaseState chaseState;
    public NavState navState;
    public AttackState attackState;

    // constructor (pass in necessary params)
    public EnemyFSM(EnemyBehavior e) //(PlayerMovement player)
    {
        /*
        this.groundState = new GroundState(player);
        this.wallState = new WallState(player);
        this.jumpState = new JumpState(player);
        this.fallState = new FallState(player);
        */
        this.idleState = new IdleState(e);
        this.chaseState = new ChaseState(e);
        this.navState = new NavState(e);
        this.attackState = new AttackState(e);
    }

    // set starting state
    public void Reset(IState state)
    {
        currState = state;
        state.Enter();
    }

    // take state transition
    public void TransitionTo(IState nextState)
    {
        currState.Exit();
        currState = nextState;
        nextState.Enter();
    }

    // code from the current state which should run in update
    public void Update()
    {
        if (currState != null)
        {
            currState.Update();
        }
    }

    // code from the current state which should run in fixedupdate
    public void FixedUpdate()
    {
        if (currState != null)
        {
            currState.FixedUpdate();
        }
    }
}

//
// The following are various states for enemy behavior
//

public class IdleState : IState
{
    //private PlayerMovement player;
    //private Vector2 movementInput;
    
    private EnemyBehavior enemy;

    // constructor
    public IdleState(EnemyBehavior e) 
    {
        enemy = e;
    }

    public void Enter()
    {
        //.Log("entered idle state");
    }

    public void Update()
    {
        bool inRange = (StatMan.sm.getPlayerPosition() - enemy.transform.position).magnitude < enemy.maxChaseRange;

        if (inRange && !enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.chaseState);
        } else if (inRange)
        {
            enemy.fsm.TransitionTo(enemy.fsm.navState);
        }
    }

    public void FixedUpdate()
    {
        enemy.goalVelocity = Vector3.zero;
    }

    public void Exit()
    {
        //.Log("exited idle state");
    }
}

public class ChaseState : IState
{   
    private EnemyBehavior enemy;

    // constructor
    public ChaseState(EnemyBehavior e) 
    {
        enemy = e;
    }

    public void Enter()
    {
        //.Log("entered chase state");
    }

    public void Update()
    {
        bool inAttackRange = (StatMan.sm.getPlayerPosition() - enemy.transform.position).magnitude < enemy.maxAttackRange;

        if (inAttackRange && !enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.attackState);
        } else if (enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.navState);
        }
    }

    public void FixedUpdate()
    {
        enemy.goalVelocity = enemy.maxSpeed * (StatMan.sm.getPlayerPosition() - enemy.transform.position).normalized;
    }

    public void Exit()
    {
        //Debug.Log("exited chase state");
    }
}

public class NavState : IState
{
    //private PlayerMovement player;
    //private Vector2 movementInput;

    private EnemyBehavior enemy;

    // constructor
    public NavState(EnemyBehavior e)
    {
        enemy = e;
    }

    public void Enter()
    {
        //Debug.Log("entered nav state");
    }

    public void Update()
    {
        bool inAttackRange = (StatMan.sm.getPlayerPosition() - enemy.transform.position).magnitude < enemy.maxAttackRange;

        if (inAttackRange && !enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.attackState);
        }
        else if (!enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.chaseState);
        }
    }

    public void FixedUpdate()
    {
        enemy.goalVelocity = enemy.maxSpeed * enemy.navDirection;
    }

    public void Exit()
    {
        //Debug.Log("exited nav state");
    }
}

public class AttackState : IState
{
    //private PlayerMovement player;
    //private Vector2 movementInput;

    private EnemyBehavior enemy;

    // constructor
    public AttackState(EnemyBehavior e)
    {
        enemy = e;
    }

    public void Enter()
    {
        //Debug.Log("entered attack state");
        enemy.attackStart = true;
    }

    public void Update()
    {
        if (enemy.attackDone && enemy.shouldNav)
        {
            enemy.fsm.TransitionTo(enemy.fsm.navState);
        }
        else if (enemy.attackDone)
        {
            enemy.fsm.TransitionTo(enemy.fsm.chaseState);
        }
    }

    public void FixedUpdate()
    {
        // another file will be managing enemy movement in attack state
    }

    public void Exit()
    {
        enemy.attackDone = false;
        //Debug.Log("exited attack state");
    }
}