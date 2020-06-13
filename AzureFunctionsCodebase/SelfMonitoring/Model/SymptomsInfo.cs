using System;
using System.Collections.Generic;
using System.Text;

namespace BackToWorkFunctions.Model
{
    public class SymptomsInfo
    {
        public string UserId { get; set; }
        public string DateOfEntry { get; set; }
        public bool UserIsExposed { get; set; }
        public string ExposureDate { get; set; }
        public string QuarantineStartDate { get; set; }
        public string QuarantineEndDate { get; set; }
        public bool IsSymptomatic { get; set; }
        public bool SymptomFever { get; set; }
        public bool SymptomCough { get; set; }
        public bool SymptomShortnessOfBreath { get; set; }
        public bool SymptomChills { get; set; }
        public bool SymptomMusclePain { get; set; }
        public bool SymptomSoreThroat { get; set; }
        public bool SymptomLossOfSmellTaste { get; set; }
        public bool SymptomVomiting { get; set; }
        public bool SymptomDiarrhea { get; set; }
        public decimal Temperature { get; set; }
        public bool UserIsSymptomatic { get; set; }
        public bool ClearToWorkToday { get; set; }        
        public string GUID { get; set; }
    }
}