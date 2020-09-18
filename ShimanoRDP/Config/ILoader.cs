namespace ShimanoRDP.Config
{
    public interface ILoader<out T>
    {
        T Load();
    }
}