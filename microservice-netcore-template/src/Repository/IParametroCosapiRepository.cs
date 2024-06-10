using Api.Dto;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IParametroCosapiRepository
    {
        List<ParametroCosapiDto> ListarParametrosCosapi();
    }
}
