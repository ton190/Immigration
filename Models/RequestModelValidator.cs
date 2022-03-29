using Immigration;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Text.RegularExpressions;

public class RequestModelValidator : AbstractValidator<RequestModel>
{
    private string _regPhone = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";
    private string _regString = @"^[a-zA-Z]";

    public RequestModelValidator(IStringLocalizer<App> Localizer)
    {
        RuleFor(x => x.Email)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(String.Format(Localizer["qe e1"], Localizer["qe s2 p1"]))
            .EmailAddress()
            .WithMessage(String.Format(Localizer["qe e2"], Localizer["qe s2 p1"]));

        RuleFor(x => x.Phone)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage(String.Format(Localizer["qe e1"], Localizer["qe s2 p2"]))
            .Matches(_regPhone)
            .WithMessage(String.Format(Localizer["qe e2"], Localizer["qe s2 p2"]));

        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s2 p3"]))
            .Must(IsString)
            .WithMessage(String.Format(Localizer["qe e2"], Localizer["qe s2 p3"]));
    }

    private bool IsString(string? input)
    {
        if (input == "" || input is null) return true;
        return Regex.IsMatch(input, _regString);
    }
    private bool IsShort(string? input)
    {
        if (input == "" || input is null) return true;
        return input?.Length > 2;
    }
}
