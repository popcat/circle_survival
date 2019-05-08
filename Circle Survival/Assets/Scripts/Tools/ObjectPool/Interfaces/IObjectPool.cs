namespace CircleSurvival
{
    public interface IObjectPool<T>
    {
        T TakeFromPool();
        void AddToPool(T obj);
    }
}
