public class Questionnaire
{
    public int MarriageStatus { get; set; }

    public string? Email { get; set; }
    public string? Phone { get; set; }

    public PersonalInformation PersonalInformation { get; set; } = new();
    public PersonalInformation SpousePersonalInformation { get; set; } = new();

    public LanguageSkills LanguageSkills { get; set; } = new();
    public LanguageSkills SpouseLanguageSkills { get; set; } = new();

    public Education Education { get; set; } = new();
    public Education SpouseEducation { get; set; } = new();

    public List<WorkExperience> WorkExperience { get; set; } = new();
    public List<WorkExperience> SpouseWorkExperience { get; set; } = new();

    public bool InCanada { get; set; }
    public bool FamilyInCanada { get; set; }
    public bool AppliedInCanada { get; set; }
    public bool DeniedInCanada { get; set; }
    public bool JobOffered { get; set; }
    public bool GonnaStudy { get; set; }
    public bool CriminalRecord { get; set; }
    public bool MedicalProblems { get; set; }
    public bool GotLawyer { get; set; }
    public bool GonnaStudyHard { get; set; }
}

public class PersonalInformation
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public int Age { get; set; }
}

public class LanguageSkills
{
    public int English { get; set; }
    public int Franch { get; set; }
    public string? Esl { get; set; }
}

public class Education
{
    public double SchoolYears { get; set; }
    public int Collage { get; set; }
    public double CollageYears { get; set; }
    public string? Profession { get; set; }
}

public class WorkExperience
{
    public string? Profession { get; set; }
    public double Experience { get; set; }
}
