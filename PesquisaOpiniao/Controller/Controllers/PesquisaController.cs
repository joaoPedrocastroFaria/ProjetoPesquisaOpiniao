using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;


[ApiController]
[Route("Pesquisa")]
public class PesquisaController : Controllerbase{

    [HttpGet]
    [Route("get")]
    public IActionResult getPesquisa(){
        
    }
}