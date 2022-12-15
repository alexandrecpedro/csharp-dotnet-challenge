namespace MinimalApi.ModelViews;

public struct Error
{
    public required int Codigo {get; set;}
    public required string Mensagem {get; set;}
}