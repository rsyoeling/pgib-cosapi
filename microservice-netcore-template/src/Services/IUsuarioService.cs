using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IUsuarioService
    {
        ObjectResult Listar_Usuario();
        ObjectResult Listar_Usuario_Por_Id(int idUsuario);
        ObjectResult Insertar_Usuario(UsuarioRequest usuarioRequest);
        ObjectResult Actualizar_Usuario(ActUsuRequest usuarioRequest);
        ObjectResult Eliminar_Usuario(int idUsuario, byte estatus);
    }
}
