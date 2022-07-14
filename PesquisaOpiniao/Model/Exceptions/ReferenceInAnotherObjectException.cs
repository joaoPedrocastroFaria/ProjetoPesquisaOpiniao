namespace Model.Exceptions;

public class ReferenceInAnotherObjectException : ObjectExistanceException
{
    public override string Message => $"Objeto está referenciado em outro objeto no Banco de Dados.";
}
