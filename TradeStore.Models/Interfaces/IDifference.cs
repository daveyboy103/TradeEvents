namespace TradeStore.Models.Interfaces
{
    public interface IDifference<T>
    {
        T GetDiff(T diffObject);
    }
}