using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    public Idle(Animator anim, GameObject nbc) : base(anim, nbc)
    {
        lvl = PHASE.IDLE;
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        
    }

    public override void Exite()
    {
        base.Exite();
    }
}
