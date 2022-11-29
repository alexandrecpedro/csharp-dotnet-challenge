namespace ORMChallenge21Days;

public class Client
{
    public int Id { get;set; }
    public string Name { get;set; } = default!;
    public string Phone { get;set; } = default!;
    public required string? CPF { get;set; }

    public void NameNotNull(string name = default!) {
        this.Name = name;
    }
}