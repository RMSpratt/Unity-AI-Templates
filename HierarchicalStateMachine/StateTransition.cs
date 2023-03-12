using UnityEngine.Events;

/// <summary>
/// Representation of transitions between states in the hierarchical state machine.
/// </summary>
public class StateTransition
{
    //The destination state.
    protected StateBase targetState;

    //Actions to execute for the transition.
    protected UnityEvent transitionActions;

    //The Condition used to determine when to trigger the state.
    protected Condition condition;

    public Condition Condition => condition;
    public StateBase TargetState => targetState;
    public UnityEvent TransitionActions => transitionActions;

    public StateTransition(StateBase targetState, UnityEvent actions, Condition condition)
    {
        this.targetState = targetState;
        this.transitionActions = actions;
        this.condition = condition;
    }

    /// <summary>
    /// Get the level difference between the passed start state and the transition's target state.
    /// </summary>
    /// <param name="startState"></param>
    /// <returns></returns>
    public int GetLevel(StateBase startState)
    {
        return startState.Level - targetState.Level;
    }

    /// <summary>
    /// Check this state's transition and see if its condition has been met.
    /// </summary>
    /// <returns></returns>
    public bool IsTriggered()
    {
        return condition.TestCondition();
    }
}
