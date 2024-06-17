using Api.Dto;
using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IAvanceService
    {
        ObjectResult Insertar_Avance(AvanceDto obj);
        List<AvanceDto> Listar_AvancesPorModelo(int idModelo);
    }
}
