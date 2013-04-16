using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PatientPortal.Models
{
    public enum BreslowDepth
    {
        T1 = 1,
        T2 = 2,
        T3 = 3,
        T4 = 4
    }

    public enum LesionSiteType
    {
        Cutaneous = 1,
        Mucosal = 2,
        Acral = 3
    }

    public enum ClarksLevel
    {
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        V = 5
    }

    public enum TStage
    {
        T1a = 1,
        T1b = 2,
        T2a = 3,
        T2b = 4,
        T3a = 5,
        T3b = 6,
        T4a = 7,
        T4b = 8
    }

    public enum LymphNodeInvolvement
    {
        Microscopic = 1,
        Macroscopic = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }

    public enum MenopausalStatus
    {
        [Display(Name = "Post-menopausal")]
        PostMenopausal = 1,

        [Display(Name = "Pre-menopausal")]
        Premenopausal = 2
    }

    public enum IFN
    {
        alpha2b = 1,
        Pegylated = 2,
        None = 3
    }

    public enum Ethnicity
    {
        [Display(Name = "Hispanic Or Latino")]
        HispanicOrLatino = 1,

        [Display(Name = "Not Hispanic or Latino")]
        NotHispanicOrLatino = 2,

        [Display(Name = "Unknown - Individual not reporting ethnicity")]
        UnknownIndivudualsNotReportingEthnicity = 3
    }

    public enum Race
    {
        [Display(Name = "American Indian/Alaska Native")]
        AmericanIndianAlaskaNative = 1,
        [Display(Name = "Asian")]
        Asian = 2,
        [Display(Name = "Native Hawaiian or Other Pacific Islander")]
        NativeHawaiianOrOtherPacificIslander = 3,
        [Display(Name = "Black or African American")]
        BlackOrAfricanAmerican = 4,
        [Display(Name = "White")]
        White = 5,
        [Display(Name = "More than one race")]
        MoreThanOneRace = 6,
        [Display(Name = "Unknown or not Reported")]
        UnknownOrNotReported = 7,
    }

    public enum YesNo
    {
        Yes = 1,
        No = 2
    }

    public enum YesNoUnknown
    {
        Yes = 1,
        No = 2,
        Unknown = 3
    }

    public enum Mitoses
    {
        [Display(Name = "<1 mm2")]
        LessThanOnemm = 1,
        [Display(Name = "1 mm2")]
        Onemm = 2,
        [Display(Name = "1-5 mm2")]
        OneToFivemm = 3,
        [Display(Name = ">5 mm2")]
        LargerThanFivemm = 4
    }

    public enum PalliationOrCompleteResection
    {
        [Display(Name = "Palliation")]
        Palliation =1,
        [Display(Name = "Complete Resection")]
        CompleteResection =2
    }

    public enum LesionSite
    {
        // head, neck, torso back, torso abdomen, back, upper extremity (left), upper extremity (righ)
        // lower extremity(left), lower extremity(right), left hand, right hand, left foot, right foot
        Head =1,
        Neck =2,
        [Display(Name = "Torso (back)")]
        TorsoBack = 3,
        [Display(Name = "Torso (abdomen)")]
        TorsoAbdomen = 4,
        Back = 5,
        [Display(Name = "Upper extremity (left)")]
        UpperExtremityLeft = 6,
        [Display(Name = "Upper extremity (right)")]
        UpperExtremityRight = 7,
        [Display(Name = "Lower extremity (left)")]
        LowerExtremityLeft = 8,
        [Display(Name = "Lower extremity (right)")]
        LowerExtremityRight = 9,
        [Display(Name = "Left hand")]
        LeftHand = 10,
        [Display(Name = "Right hand")]
        RightHand =11,
        [Display(Name = "Left foot")]
        LeftFoot =12,
        [Display(Name = "Right foot")]
        RightFoot = 13
    }

    //pull-down : liver lung bone brain lymph nodes (cervical), lymph nodes(axillary), lymph nodes (inguinal), lymph nodes (other), skin, other
    public enum Site2
    {
        Liver =1,
        Lung = 2,
        Bone =3,
        Brain =4,

        [Display(Name = "Lymph nodes (cervical)")]
        LymphNodesCervical =5,
        [Display(Name = "Lymph nodes (axillary)")]
        LymphNodesAxillary =6,
        [Display(Name = "Lymph nodes (inguinal)")]
        LymphNodesInguinal =7,
        [Display(Name = "Lymph nodes (other)")]
        LymphNodesOther =8,
        Skin =9,
        Other =10
    }

    public enum BiopsyOrResection
    {//dropdown: biopsy, partial resection, complete resection
        Biopsy = 1,
        [Display(Name = "Partial resection")]
        PartialResection = 2,
        [Display(Name = "Complete resection")]
        CompleteResection =3
    }

    public enum ImagingType
    {
        [Display(Name = "CT Scan")]
        CTScan = 1,
        MRI = 2,

        [Display(Name = "PET Scan")]
        PETScan = 3
    }

    //Progressive Disease/Stable Disease/Partial REsponse/Complete Response
    public enum Response
    {
        [Display(Name = "Progressive disease")]
        ProgressiveDisease = 1,
        [Display(Name = "Stable disease")]
        StableDisease = 2,
        [Display(Name = "Partial response")]
        PartialResponse = 3,
        [Display(Name = "Complete response")]
        CompleteResponse = 4
    }

    public class SectionName
    {


    }

    [BsonIgnoreExtraElements]
    public class PatientModel
    {
        [BsonId]
        [Required]
        [Display(Name = "ID", GroupName = "Information")]
        public string ID { get; set; }

        public bool Locked { get; set; }

        [Required]
        [Display(Name = "Patient Identifier", GroupName = "Information")]
        public string PatientIdentifier { get; set; }

        [Required]
        [Display(Name = "Date", GroupName = "Information")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Patient Age", GroupName = "Information")]
        [Range(1, 200)]

        public int PatientAge { get; set; }

        [Display(Name = "Patient Race", GroupName = "Information")]
        [Required]
        public Race? PatientRace { get; set; }

        [Display(Name = "Patient Ethnicity", GroupName = "Information")]
        [Required]
        public Ethnicity? PatientEthnicity { get; set; }

        [Display(Name = "Patient Gender at Birth", GroupName = "Information")]
        public Gender? PatientGender { get; set; }

        [Display(Name = "Patient Summary", GroupName = "Information")]
        [DataType(DataType.MultilineText)]
        public string PatientSummary { get; set; }

        [Display(Name = "Test Subsection Title #1", GroupName = "Initial Dx")]
        public SectionName TestTitle1 { get; set; }

        [Display(Name = "Date of Dx", GroupName = "Initial Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? DxDate { get; set; }

        [Display(Name = "Site of Lesion", GroupName = "Initial Dx")]
        public LesionSite? SiteOfLesion { get; set; }

        [Display(Name = "Lesion Site Type", GroupName = "Initial Dx")]
        public LesionSiteType? LesionSiteType { get; set; }


        [Display(Name = "Histology", GroupName = "Initial Dx")]
        public string Histology { get; set; }


        [Display(Name = "Breslow Depth (mm)", GroupName = "Initial Dx")]
        public double? BreslowDepth { get; set; }


        [Display(Name = "Ulceration", GroupName = "Initial Dx")]
        public YesNo? Ulceration { get; set; }

        [Display(Name = "Mitoses", GroupName = "Initial Dx")]
        public Mitoses? Mitoses { get; set; }

        [Display(Name = "Clarks Level", GroupName = "Initial Dx")]
        public ClarksLevel? ClarksLevel { get; set; }


        [Display(Name = "T Stage", GroupName = "Initial Dx")]
        public TStage? TStage { get; set; }

        [Display(Name = "Biopsy Details (Test Title #2)", GroupName = "Initial Dx")]
        public SectionName TestTitle2 { get; set; }

        [Display(Name = "Date of Bx", GroupName = "Initial Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? BxDate { get; set; }


        //TODO: FILE
        [Display(Name = "Report of Bx", GroupName = "Initial Dx")]
        [DataType(DataType.MultilineText)]
        public string BxReport { get; set; }


        [Display(Name = "Paraffin block collected", GroupName = "Initial Dx")]
        public YesNo? ParaffinBlockCollectedInitialDx { get; set; }

        [Display(Name = "Primary Surgery Date", GroupName = "Initial Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? PrimarySurgeryDate { get; set; }

        [Display(Name = "Lymph node involvement", GroupName = "Initial Dx")]
        public LymphNodeInvolvement? LymphNodeInvolvement { get; set; }

        [Display(Name = "Number of Lymph Nodes evaluated", GroupName = "Initial Dx")]
        public int? NumberofLNEvaluated { get; set; }

        [Display(Name = "Number of Lymph Nodes involved", GroupName = "Initial Dx")]
        public int? NumberofLNInvolved { get; set; }

        //Pull-down
        [Display(Name = "Matted Lymph Nodes", GroupName = "Initial Dx")]
        public YesNoUnknown? MattedLNs { get; set; }

        //pull down
        [Display(Name = "Extracapsular Extensions", GroupName = "Initial Dx")]
        public YesNoUnknown? ExtraCapsularExtensionsLNs { get; set; }

        [Display(Name = "Surgical Resection", GroupName = "Initial Dx")]
        public YesNo? SurgicalResection { get; set; }


        [Display(Name = "Date of Surgical Resection", GroupName = "Initial Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? SurgicalResectionDate { get; set; }

        [Display(Name = "Extent of Surgery", GroupName = "Initial Dx")]
        [DataType(DataType.MultilineText)]
        public string ExtentOfSurgery { get; set; }

        //TODO: Allow for "unknown" value (not just missing)
        [Display(Name = "LDH Level at Dx  (IU/L) ", GroupName = "Initial Dx")]
        public int? LDHLevelAtDx { get; set; }


        //pull-down : liver lung bone brain lymph nodes (cervical), lymph nodes(axillary), lymph nodes (inguinal), lymph nodes (other), skin, other
        [Display(Name = "Metastatic Sites", GroupName = "Initial Dx")]
        public Site2[] MetastaticSites { get; set; }

        /*    [Display(Name = "LDH Level at Dx  (IU/L) ", GroupName = "Initial Dx")]
            public int LDHLevelAtDx { get; set; }

            [Display(Name = "Metastatic Sites", GroupName = "Initial Dx")]
            [DataType(DataType.MultilineText)]
            public string MetastaticSites { get; set; }*/

        //inter
        //[Display(Name = "Type of adjuvant treatment", GroupName = "Initial Dx")]
        //public string AdjuvantTreatmentType { get; set; }

        //subject (type of adjuvant treatment)

        //yes/no/unknown  X
        [Display(Name = "IFN", GroupName = "Initial Dx")]
        public YesNoUnknown? IFNType { get; set; }

        [Display(Name = "Vaccine", GroupName = "Initial Dx")]
        public YesNoUnknown? ClinicalTrialVaccine { get; set; }

        [Display(Name = "Ipilimumab", GroupName = "Initial Dx")]
        public YesNoUnknown? ClinicalTrialIpilimumab { get; set; }

        [Display(Name = "BRAF inhibitor", GroupName = "Initial Dx")]
        public YesNoUnknown? ClinicalTrialBRAFInhibitor { get; set; }

        [Display(Name = "Other", GroupName = "Initial Dx")]
        public YesNoUnknown? ClinicalTrialOther { get; set; }

        [Display(Name = "Clinical trial - Other", GroupName = "Initial Dx")]
        public string ClinicalTrialOtherDescription { get; set; }

        [Display(Name = "Duration of Therapy (months)", GroupName = "Initial Dx")]
        public int DurationOfTherapy { get; set; }

        [Display(Name = "Acute toxicities resulting from adjuvant treatment", GroupName = "Initial Dx")]
        [DataType(DataType.MultilineText)]
        public string AcuteToxicitiesAdjuvantTreatment { get; set; }

        [Display(Name = "Chronic toxicities resulting from adjuvant treatment", GroupName = "Initial Dx")]
        [DataType(DataType.MultilineText)]
        public string ChronicToxicitiesAdjuvantTreatment { get; set; }




        // Metastatic Disease
        //date of recurrence


        //same sites (brain, lung, liver ... ) like metastatic sites
        [Display(Name = "Sites of recurrence", GroupName = "Metastatic Disease")]
        public Site2[] SitesOfRecurrence { get; set; }


        [Display(Name = "LDH level at recurrence", GroupName = "Metastatic Disease")]
        public int? LDHLevelAtRecurrence { get; set; }

        [Display(Name = " Biopsy or resection of disease", GroupName = "Metastatic Disease")]
        public BiopsyOrResection? BiopsyOrResection { get; set; }

        //TODO:  Biopsy or resection of disease
        //dropdown: biopsy, partial resection, complete resection

        [Display(Name = "Radiation?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Radiation { get; set; }

        [Display(Name = "Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? Radiation1Date { get; set; }

        [Display(Name = "Type of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation1Type { get; set; }

        [Display(Name = "Location of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation1Location { get; set; }

        [Display(Name = "Amount of Radiation (cGy)", GroupName = "Metastatic Disease")]
        public int? Radiation1Amount { get; set; }

        [Display(Name = "Date of surgery", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? SurgeryDate { get; set; }

        [Display(Name = "Date of Treatment #1", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? Treatment1Date { get; set; }

        [Display(Name = "Response to Treatment #1", GroupName = "Metastatic Disease")]
        public string ResponseToTreatment1 { get; set; }

        [DisplayFormat(DataFormatString = "{0:F1}", ApplyFormatInEditMode = true)]
        [Display(Name = "Duration of response to  treatment #1 (months)", GroupName = "Metastatic Disease")]
        public double? DurationOfResponseTreatment1 { get; set; }


        [Display(Name = "Doses of treatment #1", GroupName = "Metastatic Disease")]
        public int? DosesOfTreatment1 { get; set; }




        /*        [Display(Name = "Acute toxicities resulting from treatment #1", GroupName = "Initial Dx")]
                [DataType(DataType.MultilineText)]
                public string AcuteToxicitiesTreatment1 { get; set; }

                [Display(Name = "Chronic toxicities resulting from treatment #1", GroupName = "Initial Dx")]
                [DataType(DataType.MultilineText)]
                public string ChronicToxicitiesTreatment1 { get; set; }

                [Display(Name = "Sites of progression to treatment #1", GroupName = "Initial Dx")]
                [DataType(DataType.MultilineText)]
                public string SitesOfProgressionTreatment1 { get; set; }*/



        //surgery for metastatic disease yes or no 





        [Display(Name = "Paraffin block collected", GroupName = "Metastatic Disease")]
        public YesNo? ParaffinBlockCollectedMetastatic { get; set; }

        [Display(Name = "Comorbid medical conditions", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string ComorbidMedicalConditions { get; set; }

        [Display(Name = "If female, menopausal Status", GroupName = "Metastatic Disease")]
        public MenopausalStatus? MenopausalStatus { get; set; }

        [Display(Name = "Previous non-melanoma cancer(s)", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string PreviousNonMelanomaCancers { get; set; }

        [Display(Name = "Prior treatment(s) of these malignancies", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string PriorTreatments { get; set; }

        [Display(Name = "Allergies - Drugs", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string DrugAllergies { get; set; }

        [Display(Name = "Type(s) of Allergy(s)", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string AllergyTypes { get; set; }


        [Display(Name = "Medications", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Medications { get; set; }

        [Display(Name = "Review of Systems - Pertinent positives", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string ReviewOfSystems1 { get; set; }



        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "HT (cm)", GroupName = "Metastatic Disease")]
        public double? Height { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Wt (kg)", GroupName = "Metastatic Disease")]
        public double? Weight { get; set; }

        [Display(Name = "Physical Exam - Pertinent positives", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string PhysicalExamGeneral { get; set; }

        [Display(Name = "Mutation Status - BRAF", GroupName = "Metastatic Disease")]
        public YesNoUnknown? MutationStatusBRAF { get; set; }

        [Display(Name = "Mutation Status - NRAS", GroupName = "Metastatic Disease")]
        public YesNoUnknown? MutationStatusNRAS { get; set; }

        [Display(Name = "Mutation Status - CKIT", GroupName = "Metastatic Disease")]
        public YesNoUnknown? MutationStatusCKIT { get; set; }



        //Laboratory values

        //
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "WBC", GroupName = "Metastatic Disease")]
        public double? WBC { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "ANC", GroupName = "Metastatic Disease")]
        public double? ANC { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hgb", GroupName = "Metastatic Disease")]
        public double? Hgb { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Plts", GroupName = "Metastatic Disease")]
        public double? Plts { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "AST (NML Value)", GroupName = "Metastatic Disease")]
        public double? AST { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "ALT (NML value)", GroupName = "Metastatic Disease")]
        public double? ALT { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bilirubin", GroupName = "Metastatic Disease")]
        public double? Bili { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Alk phos (NML value)", GroupName = "Metastatic Disease")]
        public double? AlkPhos { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Na", GroupName = "Metastatic Disease")]
        public double? Na { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "K", GroupName = "Metastatic Disease")]
        public double? K { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cr (NML value)", GroupName = "Metastatic Disease")]
        public double? Cr { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Calculated GFR", GroupName = "Metastatic Disease")]
        public double? CalculatedGFR { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Glucose", GroupName = "Metastatic Disease")]
        public double? Glu { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "LDH", GroupName = "Metastatic Disease")]
        public double? LDH { get; set; }

        //section urinalysis

        [Display(Name = "Proteinuria", GroupName = "Metastatic Disease")]
        public YesNo? Proteinuria { get; set; }

        [Display(Name = "Hematuria", GroupName = "Metastatic Disease")]
        public YesNo? Hematuria { get; set; }

        //additional commments


        //Radiology

        //dropdown: CT Scan/MRI/PET Scan
        [Display(Name = "Imaging Type", GroupName = "Metastatic Disease")] 
        public ImagingType? ImagingType { get; set; }

        [Display(Name = "Imaging date", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? ImagingDate { get; set; }

    
        [Display(Name = "Most recent response on imaging", GroupName = "Metastatic Disease")]
        public Response? MostRecentResponseOnImaging { get; set; }

        //Upload
        [Display(Name = "Radiology Report", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string RadiologyReport { get; set; }

        public PatientModel()
        {
            Date = DateTime.Today;
        }

    }

    public class PatientViewModel
    {
        public PatientModel Patient { get; set; }

        public PatientViewModel()
        {
            Patient = new PatientModel();
            Files = new List<FileModel>();
        }
        public PatientViewModel(PatientModel model, IList<FileModel> files)
        {
            Patient = model;
            Files = files;
        }

        public IList<FileModel> Files { get; set; }

    }

    public class FileModel
    {

        public string Filename { get; set; }
        public string PatientID { get; set; }
        public ObjectId ID { get; set; }

        public string GetFileType()
        {
            //TODO: something more sophisticated later on
            return System.IO.Path.GetExtension(Filename);
        }

    }
}