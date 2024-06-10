using Api.Dto;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IParametrosService
    {
        List<ParametrosDto> ListarParametrosPorModelo(int idModelo);
    }
}
