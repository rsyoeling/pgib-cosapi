using Api.Dto;
using Api.Entities;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IAvanceRepository
    {
        ObjectResult Insertar_Avance(AvanceDto obj);
        public List<AvanceDto> Listar_AvancesPorModelo(int idModelo);
    }
}
