using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class State
{
    private string stateName;

    private List<StateTransition> stateTransitions;

    private UnityEvent entryActions;
    private UnityEvent regActions;
    private UnityEvent exitActions;

    public UnityEvent GetActions() => regActions;
    public UnityEvent GetEntryActions() => entryActions;
    public UnityEvent GetExitActions() => exitActions;

    public List<StateTransition> GetTransitions() => stateTransitions;

    public StateName => stateName;
    
    public void AddTransition(StateTransition newTransition)
    {
        stateTransitions.Add(newTransition);
    }

    public State(string stateName, UnityEvent entryActions = null, UnityEvent regActions = null, UnityEvent exitActions = null)
    {
        this.stateName = stateName;
        this.regActions = regActions;
        this.entryActions = entryActions;
        this.exitActions = exitActions;
        stateTransitions = new List<StateTransition>();
    }
}
