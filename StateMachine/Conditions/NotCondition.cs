/// <summary>
/// Condition that requires its sub-condition's trigger to evaluate to false.
/// </summary>
public class NotCondition: Condition
{
    public Condition subCondition;

    public NotCondition(Condition subCondition)
    {
        this.subCondition = subCondition;
    }
    
    public override bool TestCondition()
    {
        return !subCondition.TestCondition();
    }
}