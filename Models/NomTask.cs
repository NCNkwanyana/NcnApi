namespace NcnApi.Models;
public class NomTask
{
    
public int Id { get;set;}
public string Title{ get;set;} =default!;
public string? Description {get;set;}
public string? AssignedId{get;set;}
public DateTime DueDate{get;set;}

    
}