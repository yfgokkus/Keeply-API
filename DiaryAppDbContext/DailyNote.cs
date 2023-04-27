using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DiaryAppDbContext

{
    public partial class DailyNote
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
