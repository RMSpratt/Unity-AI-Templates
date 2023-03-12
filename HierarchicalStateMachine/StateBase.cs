using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Base class describing States and SubMachineStates in a Hierarchical State Machine.
/// </summary>
public abstract class StateBase
{
    //The State's parent StateMachine or SubMachineState
    protected IStateMachineState parent;

    protected string stateName;

    //The depth in the StateMachine that the state exists.
    protected int level;

    //The state's actions on entry, active, and exit
    protected UnityEvent entryActions;
    protected UnityEvent regActions;
    protected UnityEvent exitActions;

    //Transitions leading to other states in the state machine
    protected List<StateTransition> transitions;

    public int Level { get { return level; } set { level = value; } }
    public string StateName => stateName;
    public IStateMachineState Parent { get { return parent; } set { parent = value; } }
    public UnityEvent GetActions() => regActions;
    public UnityEvent GetEntryActions() => entryActions;
    public UnityEvent GetExitActions() => exitActions;
    public List<StateTransition> Transitions => transitions;

    /// <summary>
    /// Add a transition to the state's list of transitions.
    /// </summary>
    public void AddTransition(StateTransition transitionToAdd)
    {
        transitions.Add(transitionToAdd);
    }

    /// <summary>
    /// Return this state and any of its child states (if applicable).
    /// </summary>
    /// <returns></returns>
    public abstract List<StateBase> GetStates();

    /// <summary>
    /// Base update for end states. Return this state's actions.
    /// </summary>
    /// <returns></returns>
    public virtual UpdateResult Update()
    {
        return new UpdateResult()
        {
            actions = new List<UnityEvent> { GetActions() },
            transition = null,
            level = 0
        };
    }
}
