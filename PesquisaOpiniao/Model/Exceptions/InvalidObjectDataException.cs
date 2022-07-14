namespace Model.Exceptions;

public class InvalidObjectDataException : InvalidDataException
{
    public InvalidObjectDataException(string dado) : base(dado)
    {

    }

    public override string Message => $"Propriedade \"{Dado}\" deste objeto é inválida.";
}