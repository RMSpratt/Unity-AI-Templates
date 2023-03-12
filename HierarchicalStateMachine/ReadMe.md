# AI Algorithm Hierarchical StateMachine

## Description
This directory offers an implementation of a hierarchical state machine for implementing character behaviour in Unity.
Theoretically, an infinite number of levels is supported through recursive methods.

StateMachines for character behaviour are well-suited for characters who need to repeat some behaviour 
for an extended period of time, with accommodation for sudden changes in behaviour.

### Credits
This implementation was originally based on pseudocode provided by Ian Millington in "AI for Games".
The implementation presented there was not language-specific, but likely geared towards C++.

The code was entirely written by me.


## Entities
A description of the major entities used for creating the Hierarchical StateMachine are below.

### States
States represent the behaviour for agents to execute while active. 
An agent will exist in *one* state at any given time.

There are three types of states in this implementation. One abstract, and two concrete.

#### StateBase
This abstract class type defines properties shared by both concrete state types.

Shared properties defined here include:

- **StateName**: String to provide a name for the state.

- **Parent**: The parent State to this State, or null.

- **Level**: Integer representing the level this state exists in within the hierarchy.

- **EntryActions**: A UnityEvent holding actions to execute when entering the state.
- **RegActions**: A UnityEvent holding actions to execute while remaining in the state.
- **ExitActions**: A UnityEvent holding actions to execute when exiting the state.

- **Transitions**: A list of transitions to other states within a state machine.

Accessors are provided for the state's name, transitions, and actions.

#### EndState
This concrete state defines a state with no child states.
This state only contains inherited members from StateBase, and an overriden GetStates method.

#### StateMachineState
This concrete state  defines a state with child states, forming its own state machine 
within the hierarchy.

New properties defined here include:

- **CurrentState**: The current active state for this StateMachineState.

- **InitialState**: The starting state for this StateMachineState.

- **States**: A list of child states of this state defined at this level of the hierarchy.

### StateTransitions
StateTransitions form the links or edges connecting states. 
StateTransitions are defined with a condition for the StateTransition to be taken.

A **StateTransition** class is defined by the following properties:

- **TargetState**: The State to transition to when the StateTransition takes place.
- **TransitionActions**: A UnityEvent holding the action(s) to execute when the transition executes.
- **Condition**: A Condition class object used to determine when the transition will be triggered.

The key method defined for StateTransition class objects is "IsTriggered" which tests the 
triggered status of its associated condition.

### Condition
Conditions are used by StateTransitions and determine when an agent should change its behaviour.

Condition classes derive from the abstract base class Condition and only require the "TestCondition"
function to be executed.

Many variations of the Condition base class are provided here with different criteria for triggering.
These variations were designed to provide as much flexibility as possible for creating conditions.

#### DelegateConditions
DelegateCondition allows for an external method to be called to perform a check.
This provides a simpler way to perform a check than building a condition with multiple values.

Ex. An Agent may transition to a new state when it is capable of attacking the player.
It may be easier to invoke a "CanAttackPlayer()" method to evaluate this condition.

### CompoundConditions
CompoundConditions simply allow for chaining conditions using boolean logic operators, AND, OR, NOT.
Ex. Trigger a StateTransition if Condition A OR Condition B is true.
Ex. Trigger a StateTransition if Condition C is NOT true.
Ex. Triger a StateTransition if Condition A AND Condition C are true.

#### ComparerConditions
ComparerConditions allow for an Agent's current properties to be compared against a static value.

Ex. An Agent may transition to a new state when its health reaches or falls below a threshold value, i.e. 0.
A Condition based on this check could use the IntComparerCondition with a ComparerOperator of "LTE".

#### ResetConditions
ResetConditions are just one implementation for conditions that rely on an external event, i.e. a UnityEvent.

Ex. An Agent may wait to transition to a new state until an Animation completes.
The animation can be configured to fire a UnityEvent, "AnimationFinished" which is tracked by a ResetCondition.

To avoid missing event firings and responding to stale events, the Reset method is provided 
to reset the triggered status of the Condition.

### HSMUpdateResult
HSMUpdateResult is a class used only within the Hierarchical State Machine, designed to gather
actions and optionally a transition to be processed somewhere within the hierarchy.

### HierarchicalStateMachine
A driver class for running the contained Hierarchical State Machine.
Its sole method GetActions() returns a list of UnityEvents for an Agent to carry out.


## Process (How it Works)

Every time, the driver class *HierarchicalStateMachine*'s GetActions() method is called,
the baseState as the root to the statemachine is traversed recursively through the hierarchy
to get the actions to execute.

For each level, the current state is queried on its transitions for any active transitions.
If no transitions are active at the current level queried, it will query its current child state
for any transitions or actions to execute.

If a transition is triggered somewhere in the hierarchy, an *UpdateResult* will be passed throughout
to gather actions to execute as the transition is processed.

If at the end of recurring through all current states, no transition has been activated, for each level, 
the regular actions will be returned to execute.

### Transitions
Transitions are considered from the top of the hierarchy downards, prioritizing higher-level transitions
over lower-level ones.

This also allows for nested state machines (within the hierarchy) to retain their active state
if a higher-level transitions to a new state.

For gameplay purposes, this can allow for isolated behaviour in a single state machine.

Ex. Consider a guard patrolling an encampment. Spotting an enemy may send them into an "Alert Camp"
sub-state machine where they must run to get a torch and light a signal fire.

If the Agent retrieves the torch but then needs to react to an enemy attacking them,
they should be able to re-enter their "Alert Camp" state machine and continue to the fire to light,
(since they already have a torch).


## Benefits over Flat State Machines
Hierarchical State Machines are well-suited when agents have sets of behaviour that should be considered
independently from one another.

Using a hierarchy allows for transitions in the state machine to not affect nested behaviour
in sub-state machines.

Hierarchical state machines also can reduce the number of explicit transitions needed.
If there is one state that should be reached by any other state, (an alarm state),
it can be placed adjacent to a nested hierarchy representing the agent's other regular behaviour.

An example of this is in the "Images/" folder where it was used in a project I worked on.


## Implementation Notes

### About the StateMachine Class
The StateMachine class is the "Driver" for the architecture.

This class isn't a separate entity in the original pseudocode proposed by Ian Millington.
It is designed as a liaison between an Agent (MonoBehaviour) and the StateMachine itself.

### Conditions
The same conditions for this StateMachine are used for the Flat State Machine.
The code for conditions is contained there.

### Structure Changes

The original pseudocode implementation for this algorithm relies on Multiple Inheritence
to define the StateMachineState class, by inheriting from BaseState and a StateMachine classes.

Since C# does not support Multiple Inheritance, I modified the structure using an Interface
and a new abstract StateBase class.

The purpose of the abstract StateeBase class is to avoid explicit type checking when calling
update on a EndState or StateMachineState within the hierarchy.
