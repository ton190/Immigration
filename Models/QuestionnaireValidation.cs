using Immigration;
using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.Extensions.Localization;

public class QuestionnaireValidator : AbstractValidator<Questionnaire>
{
    private string _regPhone = @"^\s*(?:\+?(\d{1,3}))?[-. (]*(\d{3})[-. )]*(\d{3})[-. ]*(\d{4})(?: *x(\d+))?\s*$";
    private string _regString = @"^[a-zA-Z]";

    public QuestionnaireValidator(IStringLocalizer<App> Localizer)
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

        RuleFor(x => x.PersonalInformation.Name)
            .Cascade(CascadeMode.Stop)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s2 p3"]))
            .Must(IsString)
            .WithMessage(String.Format(Localizer["qe e2"], Localizer["qe s2 p3"]));
        RuleFor(x => x.SpousePersonalInformation.Name)
            .Cascade(CascadeMode.Stop)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s2 p3"]))
            .Must(IsString)
            .WithMessage(String.Format(Localizer["qe e2"], Localizer["qe s2 p3"]));

        RuleFor(x => x.PersonalInformation.Address)
            .Cascade(CascadeMode.Stop)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s2 p4"]));
        RuleFor(x => x.SpousePersonalInformation.Address)
            .Cascade(CascadeMode.Stop)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s2 p4"]));

        RuleFor(x => x.PersonalInformation.Age)
            .LessThan(101)
            .WithMessage(Localizer["qe e4"]);
        RuleFor(x => x.SpousePersonalInformation.Age)
            .LessThan(101)
            .WithMessage(Localizer["qe e4"]);

        RuleFor(x => x.LanguageSkills.Esl)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s3 p3"]));
        RuleFor(x => x.SpouseLanguageSkills.Esl)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s3 p3"]));

        RuleFor(x => x.Education.Profession)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s4 p4"]));
        RuleFor(x => x.SpouseEducation.Profession)
            .Must(IsShort)
            .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s4 p4"]));

        RuleForEach(x => x.WorkExperience).ChildRules(x =>
            {
                x.RuleFor(x => x.Profession)
                .Must(IsShort)
                .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s5 p1"]));
            });
        RuleForEach(x => x.SpouseWorkExperience).ChildRules(x =>
            {
                x.RuleFor(x => x.Profession)
                .Must(IsShort)
                .WithMessage(String.Format(Localizer["qe e3"], Localizer["qe s5 p1"]));
            });
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
