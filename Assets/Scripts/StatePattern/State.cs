using UnityEngine;

public class State 
{
    public enum STATE { ENTER, UPDATE, EXIT }
    public enum PHASE { IDLE, PATROL }

    public STATE condition;
    protected Animator anim;
    protected State nextState;
    protected GameObject nbc;
    protected PHASE lvl;

    public State(Animator anim, GameObject nbc)
    {
        this.anim = anim;
        this.nbc = nbc;
        condition = STATE.ENTER;
    }

    public virtual void Enter() { condition = STATE.UPDATE; }
    public virtual void Update() { condition = STATE.UPDATE; }
    public virtual void Exite() { condition = STATE.EXIT; }

    public State Process()
    {
        if (condition == STATE.ENTER) Enter();
        if (condition == STATE.UPDATE) Update();
        if (condition == STATE.EXIT)
        {
            Exite();
            return nextState;
        }

        return this;
    }
}
