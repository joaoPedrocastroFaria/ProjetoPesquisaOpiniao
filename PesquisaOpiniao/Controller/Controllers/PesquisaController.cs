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
        var pesquisa = Model.Pesquisa.getByCodigo(codigo);
        Response.Headers.Add("Access-Control-Allow-Origin" , "*");
        return pesquisa;
    }
}