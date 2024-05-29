﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dto
{
    public class ModelosRequest
    {
        public int idProyectos { get; set; }
        public string modelo { get; set; }
        public string disciplina { get; set; }
        public string estatus { get; set; }
        public string urn { get; set; }
        public int modeloversion { get; set; }
        public string estado { get; set; }
        public int usuarioCreacion { get; set; }
        //public int usuarioModificacion { get; set; }
        public string fechaCreacion { get; set; }
        //public string fechaModificacion { get; set; }
        public List<Parametros> Parametros { get; set; }
    }
    public class Parametros
    {
        public string parametro_cosapi { get; set; }
        public string grupo { get; set; }
        public string parametro { get; set; }
        public string valor { get; set; }
    }
}