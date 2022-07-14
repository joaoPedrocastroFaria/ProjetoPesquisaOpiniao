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
            var respostas = context.Resposta.Include(p=>p.Pergunta).Where(r=>r.Pergunta.Id == pergunta.Id);
            var alternativas = context.Alternativas.Include(p=>p.Pergunta).Where(a=>a.Pergunta.Id == pergunta.Id);
            context.Resposta.RemoveRange(respostas);
            context.Alternativas.RemoveRange(alternativas);
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
        using (var context =new Context()){
            var pergunta = context.Pergunta.Where(a=>a.Id==id).Single();
            return pergunta;
        }
    }
}


