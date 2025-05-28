using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentViewModel
    {
        public int? Id { get; set; }

        [Required(ErrorMessage ="Please Code is Required !!")]
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name ="Creation Date")]
        public DateTime CreationDate { get; set; }
    }
}
