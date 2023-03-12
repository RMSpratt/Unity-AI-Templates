using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// State in the Hierarchical State Machine with its own children states forming a StateMachine.
/// </summary>
public class StateMachineState: StateBase, IStateMachineState
{
    //Current state in this sub state machine
    private StateBase currentState;

    //Starting state for this sub state machine
    private StateBase initialState;

    private List<StateBase> states;

    public StateMachineState(string stateName, UnityEvent entryActions, UnityEvent regActions, UnityEvent exitActions)
    {
        this.stateName = stateName;
        this.entryActions = entryActions;
        this.exitActions = exitActions;
        this.regActions = regActions;
        currentState = null;
        initialState = null;
        level = 0;
        parent = null;
        states = new List<StateBase>();
        transitions = new List<StateTransition>();
    }

    /// <summary>
    /// Adds a child state to the sub-state machine.
    /// </summary>
    /// <param name="stateToAdd"></param>
    public void AddState(StateBase stateToAdd)
    {
        states.Add(stateToAdd);
        stateToAdd.Parent = this;
        stateToAdd.Level = Level + 1;

        //Optional (ensures an initial state is set)
        if (states.Count == 1)
        {
            initialState = states[0];
        }
    }

    /// <summary>
    /// Returns this state and its current state's list of states (if available).
    /// </summary>
    /// <returns></returns>
    public override List<StateBase> GetStates()
    {
        List<StateBase> statesToReturn = new List<StateBase>
        {
            this
        };

        if (currentState != null)
        {
            statesToReturn.AddRange(currentState.GetStates());
        }

        return statesToReturn;
    }

    /// <summary>
    /// Set the initial state of this SubMachine to the state with the passed index.
    /// </summary>
    /// <param name="index"></param>
    public void SetInitialState(int index) {
        if (index < states.Count)
            initialState = states[index];
    }

    /// <summary>
    /// Set the initial state of this SubMachine to the passed state.
    /// If the state doesn't exist within the SubMachine, it is added.
    /// </summary>
    /// <param name="initialState"></param>
    public void SetInitialState(StateBase initialState) {

        foreach (StateBase state in states) {
            if (state.Equals(initialState)) {
                this.initialState = initialState;
                return;
            }
        }

        states.Add(initialState);
        this.initialState = initialState;
    }

    /// <summary>
    /// Returns an UpdateResult for the current SubMachine, checking for transitions in its child states and any descendants.
    /// </summary>
    /// <returns>An HSMUpdateResult with actions and transition information.</returns>
    public override UpdateResult Update()
    {
        UpdateResult resultToReturn;

        //This sub-statemachine doesn't have a current state, so enter the initial state
        if (currentState == null)
        {
            currentState = initialState;
            return new UpdateResult() { actions = new List<UnityEvent>() { currentState.GetEntryActions() } };
        }

        StateTransition trigger = null;

        //For the current state, check if any of its transitions have fired
        foreach (StateTransition stateTransition in currentState.Transitions)
        {
            if (stateTransition.IsTriggered())
            {
                trigger = stateTransition;
                break;
            }
        }

        //A transition has fired, update the new state to return
        if (trigger != null)
        {
            resultToReturn = new UpdateResult();
            resultToReturn.actions = new List<UnityEvent>();
            resultToReturn.transition = trigger;
            resultToReturn.level = trigger.GetLevel(currentState);
        }

        //No transition was found, update recursively downwards
        else
        {
            resultToReturn = currentState.Update();
        }

        //A transition was found in the sub-hierarchy
        if (resultToReturn.transition != null)
        {
            //Same level transition: handle at this state
            if (resultToReturn.level == 0)
            {
                resultToReturn.actions.Add(currentState.GetExitActions());
                resultToReturn.actions.Add(resultToReturn.transition.TransitionActions);
                resultToReturn.actions.Add(resultToReturn.transition.TargetState.GetEntryActions());

                currentState = resultToReturn.transition.TargetState;

                resultToReturn.actions.Add(GetActions());
                resultToReturn.transition = null;
            }

            //Transition to a higher state, it must be passed upwards for further processing
            else if (resultToReturn.level > 0)
            {
                resultToReturn.actions.Add(currentState.GetExitActions());
                currentState = null;
                resultToReturn.level -= 1;
            }

            //Transition to a lower state, it must be passed downwards for further processing
            //Get the target state's parent and have it update up to this state level
            else
            {
                StateBase targetState = resultToReturn.transition.TargetState;
                IStateMachineState parentState = targetState.Parent;

                resultToReturn.actions.Add(resultToReturn.transition.TransitionActions);
                resultToReturn.actions.AddRange(parentState.UpdateDown(targetState, resultToReturn.level * -1));
                resultToReturn.transition = null;
            }
        }

        //There were no transitions in this sub-hierarchy, pass along the current state's regular actions
        else
        {
            resultToReturn.actions.Add(regActions);
        }

        return resultToReturn;
    }

    /// <summary>
    /// Starting from the current state, transition upwards to the target state.
    /// Entry actions for each higher level state are passed along the way.
    /// </summary>
    /// <param name="targetState">The target state of the transition.</param>
    /// <param name="level">The number of levels to recur upward to the target state.</param>
    /// <returns></returns>
    public List<UnityEvent> UpdateDown(StateBase targetState, int level)
    {
        List<UnityEvent> actions = new List<UnityEvent>();

        if (level > 0)
        {
            actions = parent.UpdateDown(this, level-1);
        }

        if (currentState != null)
        {
            actions.Add(currentState.GetExitActions());
        }

        currentState = targetState;
        actions.Add(currentState.GetEntryActions());

        return actions;
    }
}
