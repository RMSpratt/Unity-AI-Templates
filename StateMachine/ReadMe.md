# AI Algorithm StateMachine

## Description
This directory offers an implementation of a flat state machine for implementing character behaviour in Unity.
Being a flat StateMachine, this implementation only allows for one level of states and transitions.

StateMachines for character behaviour are well-suited for characters who need to repeat some behaviour 
for an extended period of time, with accommodation for sudden changes in behaviour.

This implementation was originally based on pseudocode provided by Ian Millington in "AI for Games".
The implementation presented there was not language-specific, but likely geared towards C++.


## Entities
A description of the major entities used for creating the StateMachine are below.

### States
States represent the behaviour for agents to execute while active. 
An agent will exist in *one* state at any given time.

A **State** class is defined by the following properties:

- **StateName**: String to provide a name for the state.

- **EntryActions**: A UnityEvent holding actions to execute when entering the state.
- **RegActions**: A UnityEvent holding actions to execute while remaining in the state.
- **ExitActions**: A UnityEvent holding actions to execute when exiting the state.

- **StateTransitions**: A list of transitions to other states within a state machine.

Accessors are provided for the state's name, transitions, and actions.

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

To avoid missing event firings and responding to stale events, the Reset method is provided to reset the triggered
status of the Condition.


## Implementation Notes

### About the StateMachine Class
The StateMachine class is the "Driver" for the architecture.

The StateMachine's list of states is kept to track states and to establish "AnyStateTransitions".

### About Conditions
The base "Condition" class could technically be an interface instead of an abstract class.
This was done following the original pseudocode, and I didn't see a need to change it.

##### Resetting Conditions
The implementation of resetting a condition is a design problem with different approaches.
In a recent project, I had states call the method for any of its Conditions that implement the IResetCondition
interface when the state becomes active.

Resetting only becomes an issue if multiple states in a state machine respond to the *same* event, or if
a state has more than one transition with one transition being event-based.
