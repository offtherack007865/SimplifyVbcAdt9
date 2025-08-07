﻿using System;
using System.Collections.Generic;

namespace SimplifyVbcAdt9.Data.Models;

public partial class HumanaCensusMaster
{
    public int HumanaCensusMasterId { get; set; }

    public string Dummy1 { get; set; } = null!;

    public string PatientName { get; set; } = null!;

    public string DateOfBirth { get; set; } = null!;

    public string PaneledProvider { get; set; } = null!;

    public string SdohDetails { get; set; } = null!;

    public string SrfDetails { get; set; } = null!;

    public string SrfFlagYorN { get; set; } = null!;

    public string StayType { get; set; } = null!;

    public DateTime AdmitDate { get; set; }

    public DateTime DischargeDate { get; set; }

    public string AuthStatus { get; set; } = null!;

    public string Disposition { get; set; } = null!;

    public string PatEligible { get; set; } = null!;

    public string PatEligibleThrough { get; set; } = null!;

    public string ReadmitRisk { get; set; } = null!;

    public string Readmit { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string DxDescription { get; set; } = null!;

    public string FacilityOrPractitioner { get; set; } = null!;

    public string NewYorN { get; set; } = null!;

    public string FullFilename { get; set; } = null!;
}
