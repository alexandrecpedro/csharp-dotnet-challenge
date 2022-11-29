namespace ORMChallenge21Days;

/** RECORD **/
// Compare objects, imutable object
public record struct ClientRecordStruct (
    int Id,
    string Name,
    string Phone,
    string CPF
);