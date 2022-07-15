namespace Model;
public class Resposta
{
    public int Id {get;set;}
    public string ConteudoResposta {get;set;}
    public Pergunta Pergunta {get;set;}

    //contrutores
    public Resposta(){}
    public Resposta(string conteudoResposta,Pergunta pergunta){
        ConteudoResposta=conteudoResposta;
        Pergunta=pergunta;
    }
    //Metodos
    public void ValidateObjectData(){
        if(String.IsNullOrWhiteSpace(ConteudoResposta))
            throw new Exceptions.InvalidObjectDataException("ConteudoResposta");
    }
    public void ValidateObjectNotExistance(){
        using (var context = new Context()){
            if (context.Resposta.FirstOrDefault(e=>e.Id==Id || e.ConteudoResposta==ConteudoResposta)!= null)
                throw new Exceptions.ObjectAlreadyExistsException();
        }
    }
    public void ValidateObjectExistence(){
        using(var context = new Context()){
            if(context.Resposta.FirstOrDefault(e=>e.Id==Id || e.ConteudoResposta==ConteudoResposta)== null)
                throw new Exceptions.ObjectDoesntExistException();
        }
    }
    public int save(){
        var id=0;

        ValidateObjectData();

        using(var context = new Context()){
            var resposta = new Resposta{
                ConteudoResposta=this.ConteudoResposta,
                Pergunta=this.Pergunta,
            };
            context.Resposta.Add(resposta);
            context.Entry(resposta.Pergunta).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();
            id=resposta.Id;

        }
        return id;
    }
    public static object findById(int id){
        using (var context = new Context()){
            var resposta = context.Resposta.Where(a=>a.Id==id).Single();
            return resposta;
        }
    }
}