using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Encapsulated StateMachine behaviour for States.
/// </summary>
public interface IStateMachineState
{
    /// <summary>
    /// Recursive update function for state transitions going down the hierarchy.
    /// </summary>
    /// <param name="targetState"></param>
    /// <param name="level"></param>
    /// <returns></returns>
    public List<UnityEvent> UpdateDown(StateBase targetState, int level);

    /// <summary>
    /// Add a state to this StateMachineState.
    /// </summary>
    /// <param name="newState"></param>
    public void AddState(StateBase newState);
}
