using System.Collections.Generic;

/// <summary>
/// Compound Condition that requires one or more of its sub-conditions to be triggered.
/// </summary>
public class OrCondition: Condition
{
    public List<Condition> subConditions;

    public override bool TestCondition()
    {
        foreach (Condition subCondition in subConditions)
        {
            if (subCondition.TestCondition())
            {
                return true;
            }
        }

        return false;
    }
}