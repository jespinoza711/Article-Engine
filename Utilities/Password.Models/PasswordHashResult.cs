
namespace Password.Models
{
    public struct PasswordHashResult
    {
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }
    }
}
