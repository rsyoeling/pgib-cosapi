using Api.Dto;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IParametrosRepository
    {
        List<ParametrosDto> ListarParametrosPorModelo(int idModelo);
    }
}
