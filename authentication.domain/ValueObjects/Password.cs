using System.Security.Cryptography;
using System.Text;

namespace authentication.domain.ValueObjects
{
    public class Password
    {
        public string? HashedPassword { get; private set; } = string.Empty;
        public string? PreviousHashedPassword { get; private set; } = string.Empty;
        public byte[] Salt { get; private set; }
        public bool IsFirstTime { get; set; } = true;
        public DateTime Updated { get; private set; } = DateTime.UtcNow;
        public DateTime Checked { get; private set; } = default(DateTime).ToUniversalTime();
        public string? RecoveryCode { get; private set; } = string.Empty;
        public DateTime RecoveryExpirationDate { get; set; } = default(DateTime).ToUniversalTime();
        private readonly int _saltLength = 256;

        public Password()
        {
            Salt = new byte[_saltLength];
        }


        public bool Check(string? password)
        {
            if (password == null) return false;

            Checked = DateTime.UtcNow;
            return (HashedPassword == ComputeSha256Hash(GetSaltedPassword(password)));
        }

        public void Update(string? password, string? newPassword)
        {
            if (Check(password)) Update(newPassword, true);
        }

        private void SetHashedPassword(string password)
        {
            PreviousHashedPassword = HashedPassword;
            HashedPassword = ComputeSha256Hash(GetSaltedPassword(password));
            Updated = DateTime.UtcNow;

            RecoveryCode = "";
            RecoveryExpirationDate = default(DateTime).ToUniversalTime();

            // SHA-256 produces a 256-bit (32 bytes) hash value. It's usually represented as a hexadecimal number of 64 digits.
            System.Diagnostics.Debug.Assert(HashedPassword.Length == 64);
        }

        public void CreateRecoveryCode()
        {
            RecoveryCode = MyUtils.RandomUtils.GetRandomAlphanumericString(6);
            RecoveryExpirationDate = DateTime.UtcNow.AddHours(8);
        }

        public bool RecoveryPassword(string newPassword, string recoveryCode)
        {
            if (string.IsNullOrEmpty(RecoveryCode) ||
                recoveryCode != RecoveryCode ||
                RecoveryExpirationDate < DateTime.UtcNow) return false;

            SetHashedPassword(newPassword);
            return true;
        }

        public void Update(string? password, bool itIsNewPassword = false)
        {
            if (password == null) return;

            if (!IsFirstTime && !itIsNewPassword) return;

            IsFirstTime = false;
            SetHashedPassword(password);
        }

        public void GenerateSalt()
        {
            System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(HashedPassword));
            System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(PreviousHashedPassword));
            Random r = new Random();
            r.NextBytes(Salt);
        }

        private byte[] GetSaltedPassword(string password)
        {
            System.Diagnostics.Debug.Assert(Salt.Length == _saltLength);

            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var psw = new byte[Salt.Length + passwordBytes.Length];

            Salt.CopyTo(psw, 0);
            passwordBytes.CopyTo(psw, Salt.Length);

            return psw;
        }

        // Reference: https://www.c-sharpcorner.com/article/compute-sha256-hash-in-c-sharp/
        static string ComputeSha256Hash(byte[] rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(rawData);

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

    }
}