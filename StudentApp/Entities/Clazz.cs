using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApp.Entities
{
    class Clazz
    {
        [Key]
        public int ClazzId { get; set; }
        public string Name { get; set; }
        public string Teacher { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public ClazzStatus Status { get; set; }
    }

    public enum ClazzStatus
    {
        Active = 1,
        Deactive = 0
    }
}
