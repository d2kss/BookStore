using System.Text.Json;

namespace BookStoreMgmt.Models
{
    public class ErrorDetails
    {
        public int statuccode { get; set; }
        public string? message { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }
}
