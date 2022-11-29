namespace ORMChallenge21Days;

/** STRUCT **/
// Stack Memory
public struct ClientStruct
{
    public int Id { get;set; }
    public string Name { get;set; } = default!;
    public string Phone { get;set; } = default!;
    public required string? CPF { get;set; }
}