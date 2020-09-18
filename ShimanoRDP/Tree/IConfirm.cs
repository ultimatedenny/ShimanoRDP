namespace ShimanoRDP.Tree
{
    public interface IConfirm<in TConfirmationTarget>
    {
        bool Confirm(TConfirmationTarget confirmationTarget);
    }
}