using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Display (Name ="First Name")]
		public string FirstName { get; set; } = null!;
		[Display(Name = "Last Name")]

		public string LastName { get; set; } = null!;
		//[Required(ErrorMessage ="User Name is Required")]
		public string UserName { get; set; } = null!;

		[EmailAddress]
		public string Email { get; set; } = null!;
		[DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[DataType(DataType.Password)]
		[Display(Name = "Confirm Password")]
		[Compare("Password",ErrorMessage ="Confirm Password Doesn't match the Password")]
		public string confirmPassword { get; set; } = null!;


		public bool IsAgree { get; set; }

	}
}
