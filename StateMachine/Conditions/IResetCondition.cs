/// <summary>
/// Interface for marking Conditions as having resettable data, i.e. event-based conditions.
/// </summary>
public interface IResetCondition
{
    public void Reset();
}