﻿using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository
{
    public interface IAccesoRepository
    {
        ObjectResult Listar_AccesoPorRol(int idRol);
        ObjectResult Actualizar_AccesoPorRol(AccesoRequest accesoRequest);
    }
}
