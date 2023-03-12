using System.Collections.Generic;
using UnityEngine.Events;

/// <summary>
/// Driver class for creating Hierarchical State Machines.
/// </summary>
public class HierarchicalStateMachine
{
    //Entry State to the Machine
    private StateBase baseState;
    
    public StateBase BaseState { get { return baseState; } set { baseState = value; } }

    public HierarchicalStateMachine()
    {
        baseState = null;
    }

    /// <summary>
    /// Perform an update cycle in the StateMachine checking all active transitions along the way.
    /// </summary>
    /// <returns>List of UnityEvent actions to invoke.</returns>
    public List<UnityEvent> GetActions()
    {
        if (baseState != null)
        {
            return baseState.Update().actions;
        }

        return new List<UnityEvent>();
    }
}
