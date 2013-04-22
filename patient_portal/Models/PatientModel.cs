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
    public enum StageT
    {
        T0=0,
        T1=1,
        T2=2,
        T3=3,
        T4=4
    }
    public enum StageN
    {
        N0 = 0,
        N1 = 1,
        N2 = 2 
    }
    public enum StageM
    {
        M0 = 0,
        M1 = 1
    }

    public enum LymphNodeInvolvement
    {
        Microscopic = 1,
        Macroscopic = 2
    }
    public enum LymphNodeBiopsy
    {
        Performed = 1,
        NotPerformed = 2,
        Unknown = 3
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


    public enum LocalResectionType
    {//dropdown: WLE, Amputation
       
        [Display(Name = "Wide Local Excision")]
        WideLocalExcision = 1,
        [Display(Name = "Amputation")]
        Amputation = 2
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
    //@todo: change the order
    public enum Response
    {
        [Display(Name = "Progressive disease")]
        ProgressiveDisease = 1,
        [Display(Name = "Stable disease")]
        StableDisease = 2,
        [Display(Name = "Minor Response")]
        MinorResponse = 5,
        [Display(Name = "Partial response")]
        PartialResponse = 3,
        [Display(Name = "Complete response")]
        CompleteResponse = 4
    }

    //SurgicalMargin
    public enum SurgicalMargin
    {
        [Display(Name = "Unknown")]
        Unknown = 1,
        [Display(Name = "5mm")]
        fiveMM = 2,
        [Display(Name = "10mm")]
        tenMM = 3,
        [Display(Name = "20mm")]
        twentymm = 4,
        [Display(Name = "Greater than 20mm")]
        gttwentymm = 5
    }
    //mets sites at presentation
    public enum SitesatPresentation
    {
        [Display(Name = "None")]
        None = 1,
        [Display(Name = "Multiple")]
        Multiple = 2,
        [Display(Name = "Wide Spread")]
        WideSpread = 3
    }
    public enum SatelliteNodules
    {
        [Display(Name = "Identified")]
        Identified = 1,
        [Display(Name = "Not Identified")]
        NotIdentified = 2
    }
    public class SectionName
    {


    }
    public class SubSectionName
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

        //primary tumor section
        [Display(Name = "Primary Tumor", GroupName = "Primary Dx")]
        public SectionName TestTitle2 { get; set; }

        [Display(Name = "Primary Tumor Date of Bx", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? BxDate { get; set; }

        //TODO: FILE
        [Display(Name = "Primary Tumor Report of Bx", GroupName = "Primary Dx")]
        [DataType(DataType.MultilineText)]
        public string BxReport { get; set; }


        [Display(Name = "Paraffin block collected", GroupName = "Primary Dx")]
        public YesNo? ParaffinBlockCollectedInitialDx { get; set; }

        [Display(Name = "Date of Dx", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? DxDate { get; set; }

        [Display(Name = "Primary Tumor Site", GroupName = "Primary Dx")] //Site of Lesion -renamed Sara
        public LesionSite? SiteOfLesion { get; set; }

        [Display(Name = "Lesion Site Type", GroupName = "Primary Dx")]
        public LesionSiteType? LesionSiteType { get; set; }


        [Display(Name = "Histology", GroupName = "Primary Dx")]
        public string Histology { get; set; }

        [Display(Name = "Histology Type", GroupName = "Primary Dx")]
        public string HistologyType { get; set; }

        [Display(Name = "Breslow Depth (mm)", GroupName = "Primary Dx")]
        public double? BreslowDepth { get; set; }

        [Display(Name = "Specimen Size", GroupName = "Primary Dx")]
        public string SpecimenSize { get; set; }

        [Display(Name = "Satellite nodule(s)", GroupName = "Primary Dx")]
        public SatelliteNodules? SatelliteNodules { get; set; }

        [Display(Name = "Stage T", GroupName = "Primary Dx")]
        public StageT? StageT { get; set; }

        [Display(Name = "Stage N", GroupName = "Primary Dx")]
        public StageN? StageN { get; set; }

        [Display(Name = "Stage M", GroupName = "Primary Dx")]
        public StageM? StageM { get; set; }


        [Display(Name = "Ulceration", GroupName = "Primary Dx")]
        public YesNo? Ulceration { get; set; }

        [Display(Name = "Mitoses", GroupName = "Primary Dx")]
        public Mitoses? Mitoses { get; set; }

        [Display(Name = "Clarks Level", GroupName = "Primary Dx")]
        public ClarksLevel? ClarksLevel { get; set; }


        [Display(Name = "T Stage", GroupName = "Primary Dx")]
        public TStage? TStage { get; set; }

        //TODO: Allow for "unknown" value (not just missing)
        [Display(Name = "LDH Level at Dx  (IU/L) ", GroupName = "Primary Dx")]
        public int? LDHLevelAtDx { get; set; }

        [Display(Name = "Primary Surgery Date", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        
        public DateTime? PrimarySurgeryDate { get; set; }

        // Lymph Node sub section
         [Display(Name = "Sentinel Lymph Node Biopsy", GroupName = "Primary Dx")]
        public SectionName TestTitle1 { get; set; }

        [Display(Name = "Lymph node Biopsy", GroupName = "Primary Dx")]
        public LymphNodeBiopsy? LymphNodeBiopsy { get; set; }

        [Display(Name = "Lymph node involvement", GroupName = "Primary Dx")]
        public LymphNodeInvolvement? LymphNodeInvolvement { get; set; }

        [Display(Name = "Number of Lymph Nodes evaluated", GroupName = "Primary Dx")]
        public int? NumberofLNEvaluated { get; set; }

        [Display(Name = "Number of Lymph Nodes involved", GroupName = "Primary Dx")]
        public int? NumberofLNInvolved { get; set; }

        //Pull-down
        [Display(Name = "Matted Lymph Nodes", GroupName = "Primary Dx")]
        public YesNoUnknown? MattedLNs { get; set; }

        //pull down
        [Display(Name = "Extracapsular Extensions", GroupName = "Primary Dx")]
        public YesNoUnknown? ExtraCapsularExtensionsLNs { get; set; }

        [Display(Name = "Lymph Node Dissection", GroupName = "Primary Dx")]
        public YesNo? SurgicalResection { get; set; }


        [Display(Name = "Date of Surgical Resection", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
       
        public DateTime? SurgicalResectionDate { get; set; }

        [Display(Name = "Type of Surgical Resection", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public LocalResectionType? SurgicalResectionType { get; set; }
        
        [Display(Name = "Surgical Margin", GroupName = "Primary Dx")]
        public SurgicalMargin? SurgicalMargin { get; set; } // we are renaming extent of surgery to Surgical Margin
        

        // [Display(Name = "Extent of Surgery", GroupName = "Primary Dx")]  
       //   [DataType(DataType.MultilineText)]
      //  public string ExtentOfSurgery { get; set; }

 

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
        
        // Adjuvant Treatment sub section
        [Display(Name = "Adjuvant Treatment", GroupName = "Primary Dx")]
        public SectionName TestTitle3 { get; set; }

        [Display(Name = "IFN", GroupName = "Primary Dx")]
        public YesNoUnknown? IFNType { get; set; }

        [Display(Name = "Start Date of IFN", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? IFNStartDate { get; set; }

        [Display(Name = "End Date of IFN", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? IFNEndDate { get; set; }

        [Display(Name = "IFN Cycles", GroupName = "Primary Dx")]
        public string IFNCycles { get; set; }

        [Display(Name = "Vaccine", GroupName = "Primary Dx")]
        public YesNoUnknown? ClinicalTrialVaccine { get; set; }

        [Display(Name = "Start Date of Vaccine", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? VaccineStartDate { get; set; }

        [Display(Name = "End Date of Vaccine", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")] 
        public DateTime? VaccineEndDate { get; set; }

        [Display(Name = "Vaccine Cycles", GroupName = "Primary Dx")]
        public string VaccineCycles { get; set; }

        [Display(Name = "Other", GroupName = "Primary Dx")]
        public YesNoUnknown? ClinicalTrialOther { get; set; }

        [Display(Name = "Start Date of Other", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? OtherStartDate { get; set; }

        [Display(Name = "End Date of Other", GroupName = "Primary Dx")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? OtherEndDate { get; set; }

        [Display(Name = "Other Cycles", GroupName = "Primary Dx")]
        public string OtherCycles { get; set; }

        [Display(Name = "Clinical trial - Other", GroupName = "Primary Dx")]
        public string ClinicalTrialOtherDescription { get; set; }

        [Display(Name = "Duration of Therapy (months)", GroupName = "Primary Dx")]
        public int DurationOfTherapy { get; set; }

        [Display(Name = "Acute toxicities resulting from adjuvant treatment", GroupName = "Primary Dx")]
        [DataType(DataType.MultilineText)]
        public string AcuteToxicitiesAdjuvantTreatment { get; set; }

        [Display(Name = "Chronic toxicities resulting from adjuvant treatment", GroupName = "Primary Dx")]
        [DataType(DataType.MultilineText)]
        public string ChronicToxicitiesAdjuvantTreatment { get; set; }

        //pull-down : liver lung bone brain lymph nodes (cervical), lymph nodes(axillary), lymph nodes (inguinal), lymph nodes (other), skin, other
        [Display(Name = "Metastatic Sites at Presentation", GroupName = "Primary Dx")]
        public SitesatPresentation? MetastaticSites { get; set; }
        

        // Metastatic Disease
        //date of recurrence
        //first Mets
        [Display(Name = "First Metastatic Dx", GroupName = "Metastatic Disease")]
        public SectionName TestTitle4 { get; set; }

        [Display(Name = "Site of First Metastatic Dx", GroupName = "Metastatic Disease")]
        public Site2[] SiteOfRecurrence1 { get; set; }

        [Display(Name = "Date of First Metastatic Dx", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateofRecurrance1 { get; set; }

        [Display(Name = "Surgery on First Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle11 { get; set; }

        [Display(Name = "Surgery?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Surgery1 { get; set; }

        [Display(Name = "Date of surgery", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? SurgeryDate1 { get; set; }

        [Display(Name = "Type of Surgery", GroupName = "Metastatic Disease")]
        public string SurgeryType1 { get; set; }

        [Display(Name = "Response to Surgery", GroupName = "Metastatic Disease")]
        public Response? Surgery1Response { get; set; }

        [Display(Name = "Radiation on First Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle5 { get; set; }

        [Display(Name = "Radiation?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Radiation1 { get; set; }

        [Display(Name = "Start Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation1StartDate { get; set; }

        [Display(Name = "End Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation1EndDate { get; set; }

        [Display(Name = "Type of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation1Type { get; set; }

        [Display(Name = "Location of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation1Location { get; set; }

        [Display(Name = "Amount of Radiation (cGy)", GroupName = "Metastatic Disease")]
        public int? Radiation1Amount { get; set; }

        [Display(Name = "Response to Radiation", GroupName = "Metastatic Disease")]
        public Response? Radiation1Response { get; set; }

        [Display(Name = "Systemic Treatment on First Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle12 { get; set; }

        [Display(Name = "Treatment", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance1 { get; set; }


        [Display(Name = "Start Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance1StartDate { get; set; }

        [Display(Name = "End Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance1EndDate { get; set; }

        [Display(Name = "Cycles", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance1Cycles { get; set; }

        [Display(Name = "Response to Treatment", GroupName = "Metastatic Disease")]
        public Response? TreatmentofRecurrance1ClinicalTrialResponse { get; set; }

        //Second Mets
        [Display(Name = "Progression1 of Metastatic Dx", GroupName = "Metastatic Disease")]
        public SectionName TestTitle13 { get; set; }

        [Display(Name = "Site of Progression 1" , GroupName = "Metastatic Disease")]
        public Site2[] SiteOfRecurrence2 { get; set; }

        [Display(Name = "Date of First Metastatic Dx", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateofRecurrance2 { get; set; }

        [Display(Name = "Surgery on Progression1 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle14 { get; set; }

        [Display(Name = "Surgery?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Surgery2 { get; set; }

        [Display(Name = "Date of surgery", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Surgery2Date { get; set; }

        [Display(Name = "Type of Surgery", GroupName = "Metastatic Disease")]
        public string Surgery2Type { get; set; }

        [Display(Name = "Response to Surgery", GroupName = "Metastatic Disease")]
        public Response? Surgery2Response { get; set; }

        [Display(Name = "Radiation on Progression1 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle16 { get; set; }

        [Display(Name = "Radiation?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Radiation2 { get; set; }

        [Display(Name = "Start Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation2StartDate { get; set; }

        [Display(Name = "End Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation2EndDate { get; set; }

        [Display(Name = "Type of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation2Type { get; set; }

        [Display(Name = "Location of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation2Location { get; set; }

        [Display(Name = "Amount of Radiation (cGy)", GroupName = "Metastatic Disease")]
        public int? Radiation2Amount { get; set; }

        [Display(Name = "Response to Radiation", GroupName = "Metastatic Disease")]
        public Response? Radiation2Response { get; set; }

        [Display(Name = "Systemic Treatment on Progression1 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle15 { get; set; }

        [Display(Name = "Treatment", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance2 { get; set; }


        [Display(Name = "Start Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance2StartDate { get; set; }

        [Display(Name = "End Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance2EndDate { get; set; }

        [Display(Name = "Cycles", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance2Cycles { get; set; }

        [Display(Name = "Response to Treatment", GroupName = "Metastatic Disease")]
        public Response? TreatmentofRecurrance2ClinicalTrialResponse { get; set; }

        //third Mets
        [Display(Name = "Progression2 of Metastatic Dx", GroupName = "Metastatic Disease")]
        public SectionName TestTitle17 { get; set; }

        [Display(Name = "Site of Progression 2", GroupName = "Metastatic Disease")]
        public Site2[] SiteOfRecurrence3 { get; set; }

        [Display(Name = "Date of Second Metastatic Dx", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DateofRecurrance3 { get; set; }

        [Display(Name = "Surgery on Progression2 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle18 { get; set; }

        [Display(Name = "Surgery?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Surgery3 { get; set; }

        [Display(Name = "Date of surgery", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Surgery3Date { get; set; }

        [Display(Name = "Type of Surgery", GroupName = "Metastatic Disease")]
        public string Surgery3Type { get; set; }

        [Display(Name = "Response to Surgery", GroupName = "Metastatic Disease")]
        public Response? Surgery3Response { get; set; }

        [Display(Name = "Radiation on Progression2 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle19 { get; set; }

        [Display(Name = "Radiation?", GroupName = "Metastatic Disease")]
        public YesNoUnknown? Radiation3 { get; set; }

        [Display(Name = "Start Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation3StartDate { get; set; }

        [Display(Name = "End Date of Radiation", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? Radiation3EndDate { get; set; }

        [Display(Name = "Type of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation3Type { get; set; }

        [Display(Name = "Location of Radiation", GroupName = "Metastatic Disease")]
        [DataType(DataType.MultilineText)]
        public string Radiation3Location { get; set; }

        [Display(Name = "Amount of Radiation (cGy)", GroupName = "Metastatic Disease")]
        public int? Radiation3Amount { get; set; }

        [Display(Name = "Response to Radiation", GroupName = "Metastatic Disease")]
        public Response? Radiation3Response { get; set; }

        [Display(Name = "Systemic Treatment on Progression2 Metastatic Dx", GroupName = "Metastatic Disease")]
        public SubSectionName TestTitle20 { get; set; }

        [Display(Name = "Treatment", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance3 { get; set; }


        [Display(Name = "Start Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance3StartDate { get; set; }

        [Display(Name = "End Date of Treatment", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? TreatmentofRecurrance3EndDate { get; set; }

        [Display(Name = "Cycles", GroupName = "Metastatic Disease")]
        public string TreatmentofRecurrance3Cycles { get; set; }

        [Display(Name = "Response to Treatment", GroupName = "Metastatic Disease")]
        public Response? TreatmentofRecurrance3ClinicalTrialResponse { get; set; }

        //same sites (brain, lung, liver ... ) like metastatic sites
    /*    [Display(Name = "Sites of recurrence", GroupName = "Metastatic Disease")]
        public Site2[] SitesOfRecurrence { get; set; }


        [Display(Name = "LDH level at recurrence", GroupName = "Metastatic Disease")]
        public int? LDHLevelAtRecurrence { get; set; }

        [Display(Name = " Biopsy or resection of disease", GroupName = "Metastatic Disease")]
        public BiopsyOrResection? BiopsyOrResection { get; set; }

        [Display(Name = "Ipilimumab", GroupName = "Metastatic Disease")]
        public YesNoUnknown? ClinicalTrialIpilimumab { get; set; }

        [Display(Name = "Start Date of Ipilimumab", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? IpilimumabStartDate { get; set; }

        [Display(Name = "End Date of Ipilimumab", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? IpilimumabEndDate { get; set; }

        [Display(Name = "Ipilimumab Cycles", GroupName = "Metastatic Disease")]
        public string IpilimumabCycles { get; set; }

        [Display(Name = "Response to Ipilimumab", GroupName = "Metastatic Disease")]
        public Response? ClinicalTrialIpilimumabResponse { get; set; }

        [Display(Name = "BRAF inhibitor", GroupName = "Metastatic Disease")]
        public YesNoUnknown? ClinicalTrialBRAFInhibitor { get; set; }

        [Display(Name = "Start Date of BRAF inhibitor", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BrafInhStartDate { get; set; }

        [Display(Name = "End Date of BRAF inhibitor", GroupName = "Metastatic Disease")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? BrafInhEndDate { get; set; }

        [Display(Name = "BRAF inhibitor Cycles", GroupName = "Metastatic Disease")]
        public string BrafInhCycles { get; set; }

        [Display(Name = "Response to BRAF inhibitor", GroupName = "Metastatic Disease")]
        public Response? ClinicalTrialBrafInhResponse { get; set; }

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


        [Display(Name = "Paraffin block collected", GroupName = "Metastatic Disease")]
        public YesNo? ParaffinBlockCollectedMetastatic { get; set; }

        */

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


        //additional commments


        //Radiology
        [Display(Name = "Past Medical History", GroupName = "Current Presentation")]
        public SectionName TestTitle6 { get; set; }

        [Display(Name = "Comorbid medical conditions", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string ComorbidMedicalConditions { get; set; }

        [Display(Name = "If female, menopausal Status", GroupName = "Current Presentation")]
        public MenopausalStatus? MenopausalStatus { get; set; }

        [Display(Name = "Previous non-melanoma cancer(s)", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string PreviousNonMelanomaCancers { get; set; }

        [Display(Name = "Prior treatment(s) of these malignancies", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string PriorTreatments { get; set; }

        [Display(Name = "Allergies - Drugs", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string DrugAllergies { get; set; }

        [Display(Name = "Type(s) of Allergy(s)", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string AllergyTypes { get; set; }


        [Display(Name = "Medications", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string Medications { get; set; }

        [Display(Name = "Review of Systems - Pertinent positives", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string ReviewOfSystems1 { get; set; }

        //Imaging/Radiology
        //dropdown: CT Scan/MRI/PET Scan
        [Display(Name = "Imaging/Radiology", GroupName = "Current Presentation")]
        public SectionName TestTitle7 { get; set; }

        [Display(Name = "Imaging Type", GroupName = "Current Presentation")]
        public ImagingType? ImagingType { get; set; }

        [Display(Name = "Imaging date", GroupName = "Current Presentation")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]

        public DateTime? ImagingDate { get; set; }


        [Display(Name = "Most recent response on imaging", GroupName = "Current Presentation")]
        public Response? MostRecentResponseOnImaging { get; set; }

        //Upload
        [Display(Name = "Radiology Report", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string RadiologyReport { get; set; }

        //Laboratory values
        [Display(Name = "LAB Work", GroupName = "Current Presentation")]
        public SectionName TestTitle8 { get; set; }

        //Blood Work
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "WBC", GroupName = "Current Presentation")]
        public double? WBC { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "ANC", GroupName = "Current Presentation")]
        public double? ANC { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hgb", GroupName = "Current Presentation")]
        public double? Hgb { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Plts", GroupName = "Current Presentation")]
        public double? Plts { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "AST (NML Value)", GroupName = "Current Presentation")]
        public double? AST { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "ALT (NML value)", GroupName = "Current Presentation")]
        public double? ALT { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Bilirubin", GroupName = "Current Presentation")]
        public double? Bili { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Alk phos (NML value)", GroupName = "Current Presentation")]
        public double? AlkPhos { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Na", GroupName = "Current Presentation")]
        public double? Na { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "K", GroupName = "Current Presentation")]
        public double? K { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Cr (NML value)", GroupName = "Current Presentation")]
        public double? Cr { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Calculated GFR", GroupName = "Current Presentation")]
        public double? CalculatedGFR { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Glucose", GroupName = "Current Presentation")]
        public double? Glu { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "LDH", GroupName = "Current Presentation")]
        public double? LDH { get; set; }


        //section urinalysis

        [Display(Name = "Proteinuria", GroupName = "Current Presentation")]
        public YesNo? Proteinuria { get; set; }

        [Display(Name = "Hematuria", GroupName = "Current Presentation")]
        public YesNo? Hematuria { get; set; }


        //Physical Exam
        [Display(Name = "Physical Exam", GroupName = "Current Presentation")]
        public SectionName TestTitle9 { get; set; }
        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "HT (cm)", GroupName = "Current Presentation")]
        public double? Height { get; set; }


        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Display(Name = "Wt (kg)", GroupName = "Current Presentation")]
        public double? Weight { get; set; }

        [Display(Name = "Physical Exam - Pertinent positives", GroupName = "Current Presentation")]
        [DataType(DataType.MultilineText)]
        public string PhysicalExamGeneral { get; set; }

        //Vital Signs
        [Display(Name = "Vital Signs", GroupName = "Current Presentation")]
        public SectionName TestTitle21 { get; set; }

        [Display(Name = "Blood Pressure", GroupName = "Current Presentation")]
        public string BloodPressure { get; set; }

        [Display(Name = "Pulse", GroupName = "Current Presentation")]
        public string Pulse { get; set; }

        [Display(Name = "Respiratory Rate", GroupName = "Current Presentation")]
        public string RespiratoryRate { get; set; }

        [Display(Name = "Temperature", GroupName = "Current Presentation")]
        public string Temperature { get; set; }


        //Current Melaoma Features
        [Display(Name = "Current Melanoma Features", GroupName = "Current Presentation")]
        public SectionName TestTitle22 { get; set; }
        [Display(Name = "Mutation Status - BRAF", GroupName = "Current Presentation")]
        public YesNoUnknown? MutationStatusBRAF { get; set; }

        [Display(Name = "Mutation Status - NRAS", GroupName = "Current Presentation")]
        public YesNoUnknown? MutationStatusNRAS { get; set; }

        [Display(Name = "Mutation Status - CKIT", GroupName = "Current Presentation")]
        public YesNoUnknown? MutationStatusCKIT { get; set; }



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