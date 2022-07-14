using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("Pesquisa")]
public class PesquisaController : ControllerBase {

    [HttpGet]
    [Route("get")]
    public IActionResult getPesquisa([FromBody]string codigo){
        var pesquisa = Model.Pesquisa.getByCodigo(codigo);

        return new ObjectResult(pesquisa);
    }
}