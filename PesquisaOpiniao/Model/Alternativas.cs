using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Model;
public class Alternativas
{
    public int Id {get;set;}
    public string ConteudoAlternativa {get;set;}
    public Pergunta Pergunta {get;set;}//dependencia
    public int PerguntaId {get;set;}
    //contrutores
    public Alternativas(){}
    public Alternativas(string conteudoAlternativa, Pergunta pergunta){
        ConteudoAlternativa=conteudoAlternativa;
        Pergunta=pergunta;
    }
    //Metodos
    public void ValidateObjectData(){
        if(String.IsNullOrWhiteSpace(ConteudoAlternativa))
            throw new Exceptions.InvalidObjectDataException("ConteudoAlternativa");
    }
    public void ValidateObjectNotExistance(){
        using (var context = new Context()){
            if (context.Alternativas.FirstOrDefault(e=>e.Id==Id )!= null)
                throw new Exceptions.ObjectAlreadyExistsException();
        }
    }
    public void ValidateObjectExistence(){
        using(var context = new Context()){
            if(context.Alternativas.FirstOrDefault(e=>e.Id==Id )== null)
                throw new Exceptions.ObjectDoesntExistException();
        }
    }
    public int save(){
        var id=0;

        ValidateObjectData();

        using(var context = new Context()){
            var alternativas = new Alternativas{
                ConteudoAlternativa=this.ConteudoAlternativa,
                Pergunta=this.Pergunta,
            };
            context.Alternativas.Add(alternativas);
            context.Entry(alternativas.Pergunta).State = Microsoft.EntityFrameworkCore.EntityState.Unchanged;
            context.SaveChanges();
            id=alternativas.Id;

        }
        return id;
    }
    public void delete(int id){
        using (var context =new Context()){
            var alternativa =context.Alternativas.FirstOrDefault(i=>i.Id == id);
            ValidateObjectData();
            ValidateObjectExistence();
            context.Alternativas.Remove(alternativa);
            context.SaveChanges();
        }
    }
    public void update(int id){
        using(var context = new Context()){
            var alternativa = context.Alternativas.Where(a=>a.Id == id).Single();
            alternativa.ConteudoAlternativa=this.ConteudoAlternativa;
            ValidateObjectData();
            ValidateObjectExistence();
            context.SaveChanges();
        }
    }
    public static object findById(int id){
        using (var context =new Context()){
            var alternativas = context.Alternativas.Where(a=>a.PerguntaId == id).Single();
            return alternativas;
        }
    }
}

