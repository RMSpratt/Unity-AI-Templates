
/// <summary>
/// Integer override for base ValueCondition.
/// </summary>
public class IntComparerCondition : ComparerCondition<int>
{
    public IntComparerCondition(int staticValue, valueDelegate testValueDelegate, CompareOperator compareOperator): base(staticValue, testValueDelegate, compareOperator)
    {
    }

    public override bool TestCondition()
    {
        //This notation is ridiculous, but cool
        return _compareOperator switch
        {
            CompareOperator.EQ => _testValueDelegate() == _staticValue,
            CompareOperator.LT => _testValueDelegate() < _staticValue,
            CompareOperator.GT => _testValueDelegate() > _staticValue,
            CompareOperator.LTE => _testValueDelegate() <= _staticValue,
            CompareOperator.GTE => _testValueDelegate() >= _staticValue,
            CompareOperator.NEQ => _testValueDelegate() != _staticValue,
        _ => false,
        };
    }
}