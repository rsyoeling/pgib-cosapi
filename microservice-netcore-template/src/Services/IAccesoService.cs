﻿using Api.Dto;
using Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services
{
    public interface IAccesoService
    {
        ObjectResult AccesoMenu(int idRol);
        ObjectResult Actualizar_AccesoPorRol(AccesoRequest accesoRequest);
    }
}
