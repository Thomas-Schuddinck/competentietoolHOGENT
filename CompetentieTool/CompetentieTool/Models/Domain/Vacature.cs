﻿using CompetentieTool.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetentieTool.Models.Domain
{
    public class Vacature
    {
        public String Id { get; set; }

        public String Functie { get; set; }

        public String Beschrijving { get; set; }

        public IEnumerable<Competentie> Competenties => CompetentiesLijst.Select(c => c.Competentie);

        public ICollection<VacatureCompetenties> CompetentiesLijst { get; set; }


        public Vacature()
        {
            CompetentiesLijst = new List<VacatureCompetenties>();
        }

        public void AddCompetentie(ICollection<Competentie> competenties)
        {
            foreach(Competentie comp in competenties)
                CompetentiesLijst.Add(new VacatureCompetenties { Vacature = this, VacatureId = this.Id, Competentie = comp, CompetentieId = comp.Id});
        }
    }
}
