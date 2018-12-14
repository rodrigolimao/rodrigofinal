﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using F2018Letterkenny.Controllers;
using F2018Letterkenny.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace F2018Letterkenny.Tests.Controllers
{
    [TestClass]
    public class CharactersControllerTest
    {
        CharactersController controller;
        Mock<IMockCharacter> mock;
        List<Character> characters;

        [TestInitialize]
        public void TestInitialize()
        {
            mock = new Mock<IMockCharacter>();

            characters = new List<Character>
            {
                new Character
                {
                    CharacterId = 43,
                    Name = "Reilly",
                    Description = "Hockey Player with Flow",
                    Quote = "Wheel, snipe, celly boys",
                    Photo = "reilly.png"
                },
                new Character
                {
                    CharacterId = 53,
                    Name = "Jonesy",
                    Description = "Even Dumber Hockey Player",
                    Quote = "Backcheck, Forecheck, Paycheque",
                    Photo = "jonesey.png"
                }
            };

            mock.Setup(m => m.Characters).Returns(characters.AsQueryable());
            controller = new CharactersController(mock.Object);
        }

        // GET: Characthers/Index

        [TestMethod]

        public void IndexViewLoads()

        {
            // act
            ViewResult result = controller.Index() as ViewResult;
            // assert
            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod]
        public void IndexValidLoadsCharacters()

        {
            // act
            var result = (List<Character>)((ViewResult)controller.Index()).Model;

            // assert
            CollectionAssert.AreEqual(characters, result);
        }

        // GET: Characthers/Edit

        [TestMethod]
        public void EditValidIdLoadsView()

        {
            // act 
            ViewResult result = (ViewResult)controller.Edit(100);

            // assert 
            Assert.AreEqual("Edit", result.ViewName);

        }

        [TestMethod]

        public void EditLoadsCharacterValidId()

        {
            // act 
            Character res = (Character)((ViewResult)controller.Edit(53)).Model;

            // assert 
            Assert.AreEqual(characters[0], res);
        }

        [TestMethod]
        public void EditInvalidId()

        {
            // act 
            ViewResult result = (ViewResult)controller.Edit(1234567);
            // assert 
            Assert.AreEqual("Error", result.ViewName);
        }

        [TestMethod]
        public void EditNoId()
        {
            // arrange
            int? id = null;

            // act 
            ViewResult result = (ViewResult)controller.Edit(id);
            // assert         
            Assert.AreEqual("Error", result.ViewName);
        }


        [TestMethod]

        public void EditSaveInvalid()

        {
            // arrange
            controller.ModelState.AddModelError("Random error", "Fake error summary");

            // act 
            ViewResult result = (ViewResult)controller.Edit(characters[0]);

            // assert
            Assert.AreEqual("Edit", result.ViewName);

        }

        // POST: Characters/Edit

        [TestMethod]
        public void ModelValidIndexReturned()
        {
            // act
            RedirectToRouteResult res = (RedirectToRouteResult)controller.Edit(characters[0]);

            // assert
            Assert.AreEqual("Index", res.RouteValues["action"]);
        }


    
}
}
