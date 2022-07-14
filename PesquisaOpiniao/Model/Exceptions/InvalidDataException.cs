namespace Model.Exceptions;

public class InvalidDataException : PesquisaException{
    public string Dado;

    public InvalidDataException(string dado)
        => Dado = dado;

    public override string Message => $"Propriedade \"{Dado}\" deste objeto é invalida.";
}