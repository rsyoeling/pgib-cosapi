using Api.Entities;
using System.Collections.Generic;

namespace Api.Repository
{
    public interface IEmployeeRepository
    {
        List<EvaluacionBE> ListarEvaluaciones(EvaluacionBE evaluacionBE);
        List<Dictionary<string, object>> findAllEmployees();
    }

}
