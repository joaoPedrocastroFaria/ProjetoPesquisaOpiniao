    using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Model;
public class Pergunta
{
    public int Id {get;set;}
    public string ConteudoPergunta {get;set;}
    public int TipoPergunta {get;set;} 
    public int QtddAlternativas {get;set;}
    public List<Alternativas> Alternativas {get;set;} //dependencia
    public List<Resposta> Respostas {get;set;}
    public Pesquisa Pesquisa {get;set;}

    public int PesquisaId {get;set;}

    //contrutores
    public Pergunta(){}
    public Pergunta(string conteudoPergunta,int tipoPergunta, int qtddAlternativas){
        ConteudoPergunta=conteudoPergunta;
        TipoPergunta=tipoPergunta;
        QtddAlternativas=qtddAlternativas;
        Alternativas = new List<Alternativas>();
        Respostas = new List<Resposta>();
    }
    
    //Metodos
    public void ValidateObjectData(){
        if(String.IsNullOrWhiteSpace(ConteudoPergunta))
            throw new Exceptions.InvalidObjectDataException("ConteudoPergunta");
        if(String.IsNullOrWhiteSpace(TipoPergunta.ToString()))
            throw new Exceptions.InvalidObjectDataException("TipoPergunta");
        if(String.IsNullOrWhiteSpace(QtddAlternativas.ToString()))
            throw new Exceptions.InvalidObjectDataException("QtddAlternativas");
    }
    public void ValidateObjectNotExistance(){
        using (var context = new Context()){
            if (context.Pergunta.FirstOrDefault(e=>e.Id==Id)!= null)
                throw new Exceptions.ObjectAlreadyExistsException();
        }
    }
    public void ValidateObjectExistence(){
        using(var context = new Context()){
            if(context.Pergunta.FirstOrDefault(e=>e.Id==Id)== null)
                throw new Exceptions.ObjectDoesntExistException();
        }
    }
    public int save(){
        var id=0;

        ValidateObjectData();

        using(var context = new Context()){
            var pergunta = new Pergunta{
                ConteudoPergunta=this.ConteudoPergunta,
                TipoPergunta=this.TipoPergunta,
                QtddAlternativas=this.QtddAlternativas
            };
            context.Pergunta.Add(pergunta);
            context.SaveChanges();
            id=pergunta.Id;
        }
        return id;
    }
    public void delete(int id){
        using(var context = new Context()){
            var pergunta = context.Pergunta.FirstOrDefault(p=>p.Id == id );
            ValidateObjectData();
            ValidateObjectExistence();
            context.Pergunta.Remove(pergunta);
            context.SaveChanges();
        }
    }
    public void update(int id){
        using(var context = new Context()){
            var pergunta = context.Pergunta.Where(a=>a.Id == id).Single();
            ValidateObjectData();
            ValidateObjectExistence();
            pergunta.ConteudoPergunta=this.ConteudoPergunta;
            context.SaveChanges();
        }
    }
    public static object findById(int id){
        using (var context = new Context()){
            var pergunta = context.Pergunta.Where(a=>a.Id==id).Single();
            return pergunta;
        }
    }

    public static List<Alternativas> getAllAlternativas (int id){
        using (var context = new Context()){
            List<Alternativas> alternativas = new List<Alternativas>();
            var alternativasQuery = context.Alternativas.Where(a => a.PerguntaId == id);
            foreach (Alternativas pg in alternativasQuery)
            {
                alternativas.Add(pg);
                Console.WriteLine(pg.ConteudoAlternativa);
            }
            return alternativas;
        }
    }
} 


