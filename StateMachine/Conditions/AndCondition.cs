using System.Collections.Generic;

/// <summary>
/// Compound Condition that requires all of its sub-conditions to be triggered.
/// </summary>
public class AndCondition: Condition
{
    private List<Condition> subConditions;

    public AndCondition(params Condition[] subConditions)
    {
        this.subConditions = new List<Condition>();
        this.subConditions.AddRange(subConditions);
    }

    public override bool TestCondition()
    {
        foreach (Condition subCondition in subConditions)
        {
            if (!subCondition.TestCondition())
            {
                return false;
            }
        }
        return true;
    }
}