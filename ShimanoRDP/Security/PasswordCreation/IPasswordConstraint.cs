using System.Security;


namespace ShimanoRDP.Security.PasswordCreation
{
    public interface IPasswordConstraint
    {
        string ConstraintHint { get; }

        bool Validate(SecureString password);
    }
}