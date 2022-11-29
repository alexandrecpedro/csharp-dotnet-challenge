namespace ORMChallenge21Days;

/** RECORD **/
// Compare objects, imutable object
public record ClientRecord(
    int Id,
    string Name,
    string Phone,
    string CPF
);