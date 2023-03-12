using System.Collections;
using System.Collections.Generic;

//Used to process transitions within the StateMachine, pooling actions for each state exit and entry
public class UpdateResult
{
    public List<UnityEvent> actions;
    public StateTransition transition;
    public int level;
}
