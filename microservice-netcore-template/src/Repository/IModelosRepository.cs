using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IModelosRepository
    {
        ObjectResult Insertar_Modelos(ModelosRequest modelosRequest);
        ObjectResult Actualizar_Modelos(ModelosRequest modelosRequest);

        List<ModeloResponse> Listar_ModeloPorProyecto(int idProyecto);

        ModeloResponse Buscar_ModeloPorId(int idModelo);

        ObjectResult EliminarModelo(int idModelo);
    }
}
