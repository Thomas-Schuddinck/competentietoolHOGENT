﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompetentieTool.Models.Domain;
using CompetentieTool.Models.IRepositories;
using CompetentieTool.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CompetentieTool.Controllers
{
    public class BedrijfController : Controller
    {
        private readonly IVacatureRepository _vacatureRepository;
        private readonly ICompetentieRepository _competentieRepository;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VacaturesList()
        {
            //TODO(Joren): filter op bedrijf
            return View(_vacatureRepository.GetAll());
        }

        public IActionResult Create()
        {
            var temp = new VacatureViewModel();
            temp.SetCompetentieRepository(_competentieRepository);

            var vac = new Vacature();
            vac.AddCompetenties(_competentieRepository.GetBasisCompetenties().ToList());
            temp.VacatureCompetenties = vac.CompetentiesLijst;

            return View(temp);
        }

        [HttpPost]
        public IActionResult Create(VacatureViewModel vm)
        {
            vm.SetCompetentieRepository(_competentieRepository);

            var temp = new Vacature
            {
                Functie = vm.Functie,
                Beschrijving = vm.Beschrijving,
                CompetentiesLijst = (ICollection<VacatureCompetentie>)(vm.VacatureCompetenties)
            };
            _vacatureRepository.Add(temp);

            return RedirectToAction("VacaturesList");
        }

        public IActionResult Edit(String id)
        {
            var vac = _vacatureRepository.GetBy(id);
            var temp = new VacatureViewModel(vac);
            temp.SetCompetentieRepository(_competentieRepository);

            return View(temp);
        }

        [HttpPost]
        public IActionResult Edit(VacatureViewModel vm)
        {
            var temp = new Vacature
            {
                Id = vm.Id,
                Functie = vm.Functie,
                Beschrijving = vm.Beschrijving,
                CompetentiesLijst = (ICollection<VacatureCompetentie>)(vm.VacatureCompetenties)
            };

            _vacatureRepository.Update(temp);

            return Details(vm.Id);
        }

        public IActionResult Delete(String id)
        {
            var vac = _vacatureRepository.GetBy(id);
            var temp = new VacatureViewModel(vac);
            temp.SetCompetentieRepository(_competentieRepository);

            return View(temp);
        }

        [HttpDelete]
        public IActionResult DeletePost(String id)
        {
            _vacatureRepository.Delete(id);

            return VacaturesList();
        }

        public IActionResult Details(String id)
        {
            var vac = _vacatureRepository.GetBy(id);
            var temp = new VacatureViewModel(vac);
            temp.SetCompetentieRepository(_competentieRepository);

            return View(temp);
        }

        public BedrijfController(IVacatureRepository vacatureRepository, ICompetentieRepository competentieRepository)
        {
            _vacatureRepository = vacatureRepository;
            _competentieRepository = competentieRepository;
        }
    }
}