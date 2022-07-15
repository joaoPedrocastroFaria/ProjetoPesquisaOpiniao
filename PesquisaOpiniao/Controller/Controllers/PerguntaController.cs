using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;
[ApiController]
[Route("Pergunta")]

public class PerguntaController : ControllerBase {

    [HttpGet]
    [Route("oi")]
    public void getPergunta(int id){

    }

    [HttpGet]
    [Route("getAlternativas/{id}")]
    public List<Alternativas> getAlternativas(int id){
        Console.WriteLine("Paassou");
        var alternativas = Model.Pergunta.getAllAlternativas(id);
        Response.Headers.Add("Access-Control-Allow-Origin" , "*");

        return alternativas;
    }
}