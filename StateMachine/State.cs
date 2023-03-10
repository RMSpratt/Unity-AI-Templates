using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class State
{
    public readonly int stateIdx;
    public readonly string stateName;

    private List<StateTransition> stateTransitions;

    private UnityEvent entryActions;
    private UnityEvent regActions;
    private UnityEvent exitActions;

    public UnityEvent GetActions() => regActions;
    public UnityEvent GetEntryActions() => entryActions;
    public UnityEvent GetExitActions() => exitActions;

    public List<StateTransition> GetTransitions() => stateTransitions;

    public void AddTransition(StateTransition newTransition)
    {
        stateTransitions.Add(newTransition);
    }
    
    public State(int stateIdx, string stateName, UnityEvent entryActions = null, UnityEvent regActions = null, UnityEvent exitActions = null)
    {
        this.stateIdx = stateIdx;
        this.stateName = stateName;
        this.regActions = regActions;
        this.entryActions = entryActions;
        this.exitActions = exitActions;
        stateTransitions = new List<StateTransition>();
    }
}
