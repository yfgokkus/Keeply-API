using System;
using System.Collections.Generic;

namespace DiaryAppDbContext
{
    public partial class User
    {
        public User()
        {
            DailyNotes = new HashSet<DailyNote>();
        }

        public int Id { get; set; }
        public string NameSurname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<DailyNote> DailyNotes { get; set; }
    }
}
