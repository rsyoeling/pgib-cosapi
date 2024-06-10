using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IModelosService
    {
        ObjectResult Insertar_Modelos(ModelosRequest modelosRequest);

        List<ModeloResponse> Listar_ModeloPorProyecto(int idProyecto);

        ObjectResult EliminarModelo(int idModelo);

        ModeloResponse Buscar_ModeloPorId(int idModelo);
    }
}
