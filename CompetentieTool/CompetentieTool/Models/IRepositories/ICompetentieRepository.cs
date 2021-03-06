﻿using CompetentieTool.Domain;
using CompetentieTool.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompetentieTool.Models.IRepositories
{
    public interface ICompetentieRepository
    {
        IEnumerable<Competentie> GetAll();
        IEnumerable<Competentie> GetBasisCompetenties();
        Competentie GetBy(string id);
        IEnumerable<Competentie> GetByType(CompetentieType type);
        void SaveChanges();
    }
}
