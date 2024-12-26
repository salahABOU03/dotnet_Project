using System;

namespace NomDuProjet.Models;
public class ExportData
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; } // e.g., Excel, CSV
    public DateTime ExportDate { get; set; }
}
