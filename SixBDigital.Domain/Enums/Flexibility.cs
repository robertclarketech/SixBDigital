namespace SixBDigital.Domain.Enums
{
	using System.ComponentModel.DataAnnotations;

	public enum Flexibility
	{
		[Display(Name = "+/- One Day")]
		PlusMinusOneDay = 0,

		[Display(Name = "+/- Two Days")]
		PlusMinusTwoDays = 1,

		[Display(Name = "+/- Three Days")]
		PlusMinusThreeDays = 2
	}
}
