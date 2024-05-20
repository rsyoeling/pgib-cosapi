using Api.Entities;
using System.Collections.Generic;

namespace Api.Services
{
    public interface IEmployeeService
    {
        List<EvaluacionBE> ListarEvaluaciones(EvaluacionBE evaluacionBE);
        List<Dictionary<string, object>> findAllEmployees();
    }

}
