using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// End State in the StateMachine with no children.
/// </summary>
public class EndState: StateBase
{
    public EndState(string stateName, UnityEvent entryActions, UnityEvent regActions, UnityEvent exitActions)
    {
        level = 0;
        parent = null;
        transitions = new List<StateTransition>();
        this.stateName = stateName;
        this.entryActions = entryActions;
        this.exitActions = exitActions;
        this.regActions = regActions;
    }

    public override List<StateBase> GetStates()
    {
        return new List<StateBase> { this };
    }
}
