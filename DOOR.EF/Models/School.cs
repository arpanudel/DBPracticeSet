using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("SCHOOL")]
    public partial class School
    {
        public School()
        {
            Courses = new HashSet<Course>();
            Enrollments = new HashSet<Enrollment>();
            Sections = new HashSet<Section>();
            Students = new HashSet<Student>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }
        [Column("SCHOOL_NAME")]
        [StringLength(30)]
        [Unicode(false)]
        public string SchoolName { get; set; } = null!;
        [Column("CREATED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("MODIFIED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("School")]
        public virtual ICollection<Course> Courses { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Section> Sections { get; set; }
        [InverseProperty("School")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
