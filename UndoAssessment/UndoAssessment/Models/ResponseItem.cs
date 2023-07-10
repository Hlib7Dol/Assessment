using System.Text.Json.Serialization;

namespace UndoAssessment.Models
{
    public class ResponseItem
    {
        public int ErrorCode { get; set; }

        public string Message { get; set; }

        public string Date { get; set; }
    }
}
