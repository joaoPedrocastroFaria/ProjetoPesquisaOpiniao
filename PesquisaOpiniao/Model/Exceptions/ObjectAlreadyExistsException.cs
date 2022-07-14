namespace Model.Exceptions;

public class ObjectAlreadyExistsException : ObjectExistanceException
{
    public override string Message => $"Objeto já existente no Banco de Dados.";
}
