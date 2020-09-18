namespace ShimanoRDP.Tree
{
    public class AlwaysConfirmYes : IConfirm<object>
    {
        public bool Confirm(object target)
        {
            return true;
        }
    }
}