using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("Pesquisa")]
public class PesquisaController : ControllerBase {

    [HttpGet]
    [Route("get/{codigo}")]
    public object getPesquisa(string codigo){
        Pesquisa pesquisa = Model.Pesquisa.getByCodigo(codigo);
        Response.Headers.Add("Access-Control-Allow-Origin" , "*");
        return pesquisa;
    }


    [HttpGet]
    [Route("getPerguntas/{id}")]
    public List<Pergunta> getPerguntas(int id){
        Console.WriteLine("Passou");
        var perguntas = Model.Pesquisa.getAllPerguntas(id);
        Response.Headers.Add("Access-Control-Allow-Origin" , "*");

        return perguntas;
    }
}