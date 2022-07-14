using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;



namespace Model;

public class Adm : Interfaces.IvalidateObject
{
    public int Id {get;set;}
    public string Nome {get;set;}
    public string EDV {get;set;}
    public string Senha {get;set;}
    public List<Pesquisa> Pesquisas {get;set;}//dependencia
    //contrutores
    public Adm(){}
    public Adm(string nome,string edv,string senha){
        Nome=nome;
        EDV=edv;
        Senha=senha;
        Pesquisas = new List<Pesquisa>();
    }
    //Metodos
    public void ValidateObjectData(){
        if(String.IsNullOrWhiteSpace(Nome))
            throw new Exceptions.InvalidObjectDataException("Nome");
        if(String.IsNullOrWhiteSpace(EDV))
            throw new Exceptions.InvalidObjectDataException("EDV");
        if(String.IsNullOrWhiteSpace(Senha))
            throw new Exceptions.InvalidObjectDataException("Senha");
    }
    public void ValidateObjectNotExistance(){
        using (var context = new Context()){
            if (context.Adm.FirstOrDefault(e=>e.Id==Id || e.EDV==EDV)!= null)
                throw new Exceptions.ObjectAlreadyExistsException();
        }
    }
    public void ValidateObjectExistence(){
        using(var context = new Context()){
            if(context.Adm.FirstOrDefault(e=>e.Id==Id || e.EDV==EDV)== null)
                throw new Exceptions.ObjectDoesntExistException();
        }
    }
    public int save(){
        var id=0;

        ValidateObjectData();
        ValidateObjectNotExistance();

        using(var context = new Context()){
            var adm = new Adm{
                Nome=this.Nome,
                EDV=this.EDV,
                Senha=this.Senha,
            };
            context.Adm.Add(adm);
            context.SaveChanges();
            id=adm.Id;
        }
        return id;
    }
    public void delete(int id){
        using (var context = new Context()){
            var adm = context.Adm.FirstOrDefault(i=>i.Id == id);
            ValidateObjectData();
            ValidateObjectExistence();
            context.Adm.Remove(adm);
            context.SaveChanges();
        }
    }
    public static object findById(int id){
        using (var context =new Context()){
            var adm = context.Adm.Where(a=>a.Id==id).Single();
            return adm;
        }
    }
    public static bool verifyLogin(string edv, string senha){
        
        using(var context = new Context()){
            var adm = context.Adm.Where(a=>a.EDV == edv && a.Senha == senha);

            if (adm == null){
                return false;
            }else{
                return true;
            }
        }
    }
}
