using System;
using PhiloSoftware.Core.Infrastructure.Security;

namespace PhiloSoftware.Core.Infrastructure.Definitions
{
    public interface IHaveUsernameAndPassword
    {
        string Username { get; }
        IHashedValue Password { get; }
        DateTimeOffset LastLoginDate { get; }

        /// <summary>
        /// Authenticate the password against the stored password
        /// </summary>
        bool Authenticate(string passwordAttempt);

        /// <summary>
        /// Change the users password
        /// </summary>
        bool ChangePassword(string passwordAttempt, string newPassword, string newPasswordConfirm);
    }
}
