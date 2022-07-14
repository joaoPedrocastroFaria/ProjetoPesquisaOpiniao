namespace Model.Exceptions;



public class ObjectDoesntExistException : ObjectExistanceException
{
    public override string Message => $"Objeto não existe no Banco de Dados.";
}
