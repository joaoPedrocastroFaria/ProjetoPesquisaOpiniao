using Microsoft.AspNetCore.Mvc;
using DTO;
using Model;

namespace Controller.Controllers;

[ApiController]
[Route("Adm")]
public class AdmController : ControllerBase{
    [HttpGet]
    [Route("login")]
    public IActionResult logar([FromBody] string edv, [FromBody]string senha){
        // Response.Headers.Add("Access-Control-Allow-Origin", "*");
        bool response = Model.Adm.verifyLogin(edv, senha);

        return Ok(response);
    }

    [HttpPost]
    [Route("register")]
    public IActionResult cadastrar([FromBody]AdmDTO admDTO){

        var adm = new Model.Adm(admDTO.Nome, admDTO.EDV, admDTO.Senha);

        try{
            Model.Adm.save();
        }
        catch (Model.Exceptions.InvalidObjectDataException e)
        {
            return BadRequest(e, "Os dados do objeto são inválidos.");
        }
        catch (Model.Exceptions.ObjectAlreadyExistsException e)
        {
            return BadRequest(e, "O objeto já existe no Banco de Dados.");
        }
        
        return Ok();
    }
}