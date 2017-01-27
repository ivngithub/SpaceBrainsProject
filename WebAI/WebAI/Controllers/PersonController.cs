﻿using BusinessLogic.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAI.Models;
using AutoMapper;
using BusinessLogic.DTO;

namespace WebAI.Controllers
{
    public class PersonController : Controller
    {
        IPersonService personService = null;
        
        public PersonController(IPersonService personService)
        {
            this.personService = personService;
            //Mapper.Initialize(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
            //Mapper.Initialize(cfg => cfg.CreateMap<KeyWordViewModel, KeyWordDTO>());
        }
  
        public ActionResult PersonList()
        {
            return View(GetPersons());
        }

        public ActionResult KeyWordList(int personId = 1)
        {
            ViewBag.Persons = GetPersons();
            ViewBag.KeyWords = GetKeyWords(personId);

            return View();
        }

        public ActionResult FIlteredKeyWord(PersonViewModel person)
        {
            ViewBag.SelectedPersonId = person.Id;
            return PartialView("_KeyWordList", GetKeyWords(person.Id));
        }

        IEnumerable<PersonViewModel> GetPersons()
        {
            
            var per = personService.GetPersons();
            Mapper.Initialize(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            return Mapper.Map<IEnumerable<PersonDTO>, IEnumerable<PersonViewModel>>(per);
        }

        IEnumerable<KeyWordViewModel> GetKeyWords()
        {
            var kw = personService.GetKeyWords();
            Mapper.Initialize(cfg => cfg.CreateMap<KeyWordDTO, KeyWordViewModel>());
            return Mapper.Map<IEnumerable<KeyWordDTO>, IEnumerable<KeyWordViewModel>>(kw);
        }

        IEnumerable<KeyWordViewModel> GetKeyWords(int id)
        {
            var kw = personService.GetKeyWordsForPerson(id);
            Mapper.Initialize(cfg => cfg.CreateMap<KeyWordDTO, KeyWordViewModel>());
            return Mapper.Map<IEnumerable<KeyWordDTO>, IEnumerable<KeyWordViewModel>>(kw);
        }

        [HttpGet]
        public ActionResult AddPerson()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddPerson(PersonViewModel newPerson)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
            personService.AddPerson(Mapper.Map<PersonViewModel, PersonDTO>(newPerson));
            return RedirectToAction("PersonList");
        }

        [HttpGet]
        public ActionResult AddKeyWord(int id)
        {
            ViewBag.PersonId = id;
            ViewBag.Person = personService.GetPersonById(id).Name;
            return View();
        }

        [HttpPost]
        public ActionResult AddKeyWord(KeyWordViewModel newKeyWord)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<KeyWordViewModel, KeyWordDTO>());
            personService.AddKeyWord(Mapper.Map<KeyWordViewModel, KeyWordDTO>(newKeyWord));
            return RedirectToAction("KeyWordList");
        }

        [HttpGet]
        public ActionResult ChangePerson(int id)
        {
            var personDTO = personService.GetPersonById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<PersonDTO, PersonViewModel>());
            return View(Mapper.Map<PersonDTO, PersonViewModel>(personDTO));
        }


        [HttpPost]
        public ActionResult ChangePerson(PersonViewModel personToChange)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<PersonViewModel, PersonDTO>());
            var personDTO = Mapper.Map<PersonViewModel, PersonDTO>(personToChange);
            personService.ChangePerson(personDTO);
            return RedirectToAction("PersonList");
        }

        public ActionResult DeletePerson(int id)
        {
            personService.DeletePersonById(id);
            return RedirectToAction("PersonList");
        }

        [HttpGet]
        public ActionResult ChangeKeyWord(int id)
        {
            var keyWordDTO = personService.GetKeyWordById(id);
            Mapper.Initialize(cfg => cfg.CreateMap<KeyWordDTO, KeyWordViewModel>());
            return View(Mapper.Map<KeyWordDTO, KeyWordViewModel>(keyWordDTO));
        }


        [HttpPost]
        public ActionResult ChangeKeyWord(KeyWordViewModel keyWordToChange)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<KeyWordViewModel, KeyWordDTO>());
            var keyWordDTO = Mapper.Map<KeyWordViewModel, KeyWordDTO>(keyWordToChange);
            personService.ChangeKeyWord(keyWordDTO);
            return RedirectToAction("KeyWordList");
        }

        public ActionResult DeleteKeyWord(int id)
        {
            personService.DeleteKeyWordById(id);
            return RedirectToAction("KeyWordList");
        }
    }
}