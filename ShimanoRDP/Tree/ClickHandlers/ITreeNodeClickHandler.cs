namespace ShimanoRDP.Tree.ClickHandlers
{
    public interface ITreeNodeClickHandler<in T>
    {
        void Execute(T clickedNode);
    }
}