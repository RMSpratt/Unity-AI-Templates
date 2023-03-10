
/// <summary>
/// Condition triggered when the associated UnityEvent fires. 
/// Triggered status can be reset if the containing state isn't active.
/// </summary>
public class EventCondition: Condition, IResetCondition
{
    private bool isTriggered;

    private UnityEvent conditionEvent;

    public EventCondition(UnityEvent conditionEvent)
    {
        this.conditionEvent = conditionEvent;
        this.conditionEvent.AddListener(SetTrigger);
        isTriggered = false;
    }

    public override bool TestCondition()
    {
        return isTriggered;
    }

    public void Reset()
    {
        isTriggered = false;
    }

    public void SetTrigger()
    {
        isTriggered = true;
    }
}
