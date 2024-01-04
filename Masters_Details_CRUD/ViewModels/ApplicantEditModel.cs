using Masters_Details_CRUD.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Masters_Details_CRUD.ViewModels
{
    public class ApplicantEditModel
    {
        public int ApplicantId { get; set; }
        [Required, StringLength(50)]
        public string ApplicantName { get; set; } = default!;
        [Required, Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }
        public string AppliedFor { get; set; } = default!;
        public bool IsReadyToWork { get; set; }
        public List<Qualification> Qualifications { get; set; } = new List<Qualification>();
    }
}
