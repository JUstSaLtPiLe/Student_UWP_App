using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace StudentApp.Entities
{
    [DataContract]
    public class Grade
    {
        public int AccountId { get; set; }
        public int SubjectId { get; set; }
        [Range(0, 10)]
        [Required]
        public int AssignmentGrade { get; set; }
        [Range(0, 15)]
        [Required]
        public int PraticalGrade { get; set; }
        [Range(0, 10)]
        [Required]
        public int TheoricalGrade { get; set; }
        public GradeStatus TheoricalGradeStatus { get; set; }
        public GradeStatus PraticalGradeStatus { get; set; }
        public GradeStatus AssignmentGradeStatus { get; set; }
        //public float Mark { get; set; }
        //public GradeType GradeType { get; set; }
        public GradeStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }

        public Grade()
        {
            this.AssignmentGradeStatus = GradeStatus.Passed;
            this.TheoricalGradeStatus = GradeStatus.Passed;
            this.PraticalGradeStatus = GradeStatus.Passed;
        }
    }

    public enum GradeStatus
    {
        Failed = 0,
        Passed = 1,
    }

    public enum GradeType
    {
        AssignmentGrade = 1,
        PraticalGrade = 2,
        TheoricalGrade = 3
    }
}