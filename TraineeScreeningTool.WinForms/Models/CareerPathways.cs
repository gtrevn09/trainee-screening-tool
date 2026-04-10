namespace TraineeScreeningTool.WinForms.Models;

// Centralized list of all career pathways offered by LIFE Works
public static class CareerPathways
{
    public static readonly string[] All =
    [
        "Carpentry",
        "Information Technology",
        "Digital Marketing",
        "Bookkeeping / Accounting",
        "Automotive Technician",
        "Manufacturing",
        "Restaurant Management",
        "Administrative Assistant",
        "Customer Service / Sales",
        "Landscape Technician"
    ];

    // Returns recommended certifications for a given pathway (plus universal ones).
    public static string[] GetCertificationsFor(string pathway)
    {
        var general = new[]
        {
            "OSHA 10 Safety",
            "CPR / First Aid",
            "Microsoft Office Specialist (MOS) – Word",
            "Microsoft Office Specialist (MOS) – Excel"
        };

        var specific = pathway switch
        {
            "Carpentry" => new[]
            {
                "NCCER Carpentry Level 1",
                "NCCER Carpentry Level 2",
                "OSHA 30 Construction",
                "Lead Renovator Certification (EPA RRP)",
                "Forklift Operator Certification"
            },
            "Information Technology" => new[]
            {
                "CompTIA IT Fundamentals (ITF+)",
                "CompTIA A+",
                "CompTIA Network+",
                "CompTIA Security+",
                "Google IT Support Certificate",
                "Microsoft Certified: Azure Fundamentals"
            },
            "Digital Marketing" => new[]
            {
                "Google Digital Marketing & E-commerce Certificate",
                "Google Analytics Certification",
                "HubSpot Content Marketing Certification",
                "HubSpot Social Media Marketing Certification",
                "Meta (Facebook) Blueprint Certification",
                "Hootsuite Social Marketing Certification"
            },
            "Bookkeeping / Accounting" => new[]
            {
                "QuickBooks Certified User",
                "Intuit Bookkeeping Certificate",
                "NACPB Bookkeeping Certification",
                "Xero Advisor Certified",
                "Microsoft Office Specialist (MOS) – Excel (Advanced)"
            },
            "Automotive Technician" => new[]
            {
                "ASE Student Certification",
                "ASE A1 – Engine Repair",
                "ASE A4 – Steering & Suspension",
                "ASE A6 – Electrical / Electronic Systems",
                "EPA 609 Refrigerant Handling",
                "NATEF Automotive Service Excellence"
            },
            "Manufacturing" => new[]
            {
                "MSSC Certified Production Technician (CPT)",
                "NCCER Industrial Maintenance",
                "Forklift Operator Certification",
                "OSHA 30 General Industry",
                "Six Sigma Yellow Belt",
                "Lean Manufacturing Fundamentals"
            },
            "Restaurant Management" => new[]
            {
                "ServSafe Food Manager Certification",
                "ServSafe Food Handler Certificate",
                "ServSafe Alcohol Certification",
                "National Restaurant Association ManageFirst",
                "TIPS Alcohol Training Certification"
            },
            "Administrative Assistant" => new[]
            {
                "Microsoft Office Specialist (MOS) – Word (Expert)",
                "Microsoft Office Specialist (MOS) – Excel (Expert)",
                "Microsoft Office Specialist (MOS) – Outlook",
                "PACE Administrative Certification (IAAP)",
                "Notary Public Commission",
                "QuickBooks Certified User"
            },
            "Customer Service / Sales" => new[]
            {
                "HDI Customer Service Representative (HDI-CSR)",
                "Salesforce Certified Associate",
                "HubSpot Sales Software Certification",
                "CCSP Customer Service Professional",
                "TIPS Conflict De-escalation"
            },
            "Landscape Technician" => new[]
            {
                "NALP Landscape Industry Certified Technician",
                "Pesticide Applicator License",
                "Irrigation Association Certified Irrigation Technician",
                "OSHA 10 Construction",
                "Forklift / Skid Steer Operator Certification"
            },
            _ => Array.Empty<string>()
        };

        return general.Concat(specific).Distinct().ToArray();
    }
}