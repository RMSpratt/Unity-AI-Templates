///Comparison operators used by Comparer Conditions.
public enum CompareOperator
{
    EQ,
    LT,
    GT,
    LTE,
    GTE,
    NEQ
}

/// <summary>
/// Base class for Conditions that evaluate some static value against a changeable dynamic value using an equality comparison operator.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class ComparerCondition<T>: Condition
{
    protected T _staticValue;

    public delegate T valueDelegate();

    protected valueDelegate _testValueDelegate;

    protected CompareOperator _compareOperator;

    public ComparerCondition(T staticValue, valueDelegate testValueDelegate, CompareOperator compareOperator)
    {
        _staticValue = staticValue;
        _testValueDelegate = testValueDelegate;
        _compareOperator = compareOperator;
    }
}