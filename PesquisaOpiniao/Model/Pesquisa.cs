namespace Model;
using Microsoft.EntityFrameworkCore;
using DTO;
public class Pesquisa
{
    public int Id {get;set;}
    public string Nome {get;set;}
    public string Codigo {get;set;}
    public Adm Adm {get;set;}
    public int AdmId {get;set;}
    public List<Pergunta> Perguntas {get;set;}//dependencia
    //contrutores
    public Pesquisa(){}
    public Pesquisa(string nome, string codigo,Adm adm){
        Nome=nome;
        Codigo=codigo;
        Adm=adm;
        Perguntas = new List<Pergunta>();
    }
    //Metodos
    public void ValidateObjectData(){
        if(String.IsNullOrWhiteSpace(Nome))
            throw new Exceptions.InvalidObjectDataException("Nome");
        if(String.IsNullOrWhiteSpace(Codigo))
            throw new Exceptions.InvalidObjectDataException("Codigo");
        if(Perguntas == null)
            throw new Exceptions.InvalidObjectDataException("Perguntas");
    }
    public void ValidateObjectNotExistance(){
        using (var context = new Context()){
            if (context.Pesquisa.FirstOrDefault(e=>e.Id==Id || e.Codigo==Codigo)!= null)
                throw new Exceptions.ObjectAlreadyExistsException();
        }
    }
    public void ValidateObjectExistence(){
        using(var context = new Context()){
            if(context.Pesquisa.FirstOrDefault(e=>e.Id==Id || e.Codigo==Codigo)== null)
                throw new Exceptions.ObjectDoesntExistException();
        }
    }
    public int save(){
        var id=0;

        ValidateObjectData();

        using(var context = new Context()){
            var pesquisa = new Pesquisa{
                Nome=this.Nome,
                Codigo=this.Codigo,
                Adm=this.Adm,
            };
            context.Pesquisa.Add(pesquisa);
            context.Entry(pesquisa.Adm).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();
            id=pesquisa.Id;
        }
        return id;
    }
    public void delete(int id){
        using(var context = new Context()){
            var pesquisa = context.Pesquisa.FirstOrDefault(p=>p.Id == id);
            ValidateObjectData();
            ValidateObjectExistence();
            context.Pesquisa.Remove(pesquisa);
            context.SaveChanges();
        }
    }
    public static object findById(int id){
        using (var context =new Context()){
            var pesquisa = context.Pesquisa.Where(a=>a.Id==id).Single();
            return pesquisa;
        }
    }

    public static Pesquisa getByCodigo(string codigo){
        using(var context = new Context())
        {
            var pesquisa = context.Pesquisa.Where(a=>a.Codigo == codigo).Single();
            // var adm = context.Adm.Where(a=> a.Id == pesquisa.AdmId).Single();
            // pesquisa.Adm = adm;
            pesquisa.Perguntas = Pesquisa.getAllPerguntas(pesquisa.Id);
            foreach( Pergunta pg in pesquisa.Perguntas){
                pg.Alternativas = Pergunta.getAllAlternativas(pg.Id);
            }
            return pesquisa;
        }
    }

    public static List<Pergunta> getAllPerguntas(int id)
    {
        using (var context = new Context())
        {
            List<Pergunta> perguntas = new List<Pergunta>();
            var perguntasQuery = context.Pergunta.Where(a => a.PesquisaId == id);
            foreach (Pergunta pg in perguntasQuery)
            {
                perguntas.Add(pg);
            }
            return perguntas;
        }

    }
}