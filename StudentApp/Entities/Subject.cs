using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entities
{
    public class Subject
    {
        public Subject()
        {
            this.Status = SubjectStatus.Active;
        }
        [Key]
        public int SubjectId { get; set; }
        public string Name { get; set; }
        public SubjectStatus Status { get; set; }
    }

    public enum SubjectStatus
    {
        Active = 1,
        Deactive = 0
    }
}
