namespace Model.Exceptions;


public class InvalidDataWhileSavingException : InvalidDataException
{
    public InvalidDataWhileSavingException(string dado) : base(dado)
    {

    }
    
    public override string Message => $"Propriedade \"{Dado}\" deve estar vazia ao ser salvar no Banco de Dados.";
}
