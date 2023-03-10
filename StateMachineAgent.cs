using UnityEngine;

/// <summary>
/// Sample class for the structure of an Agent with a StateMachine controlling its behaviour.
/// </summary>
public class StateMachineAgent : MonoBehaviour
{
    StateMachine stateMachine;

    /// <summary>
    /// Example of a Function used to create an Agent's StateMachine.
    /// </summary>
    void BuildStateMachine()
    {
        stateMachine = new StateMachine(null, null);

        //1. Create States
        
        //2. Create State Transitions between States

        //3. Add States to StateMachine

        //4. Add AnyStateTransitions to StateMachine
    }
}
