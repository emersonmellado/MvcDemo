using System;

namespace Web.Models
{
    public class ChangeLog
    {
        public int ChangeLogId { get; set; }
        public Guid RequestId { get; set; }
        public string PrimaryKey { get; set; }
        public string Entity { get; set; }
        public string OriginalValue { get; set; }
        public string CurrentValue { get; set; }
        public DateTime Date { get; set; }
    }
}