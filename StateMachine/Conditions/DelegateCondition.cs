
/// <summary>
/// Condition that calls a delegate function to evaluate as true or false. 
/// Not recommended but can work in a pinch.
/// </summary>
public class DelegateCondition : Condition
{
    public delegate bool conditionDelegate();

    private conditionDelegate conditionFunc;

    public DelegateCondition(conditionDelegate conditionFunc)
    {
        this.conditionFunc = conditionFunc;
    }

    public override bool TestCondition()
    {
        return conditionFunc();
    }
}