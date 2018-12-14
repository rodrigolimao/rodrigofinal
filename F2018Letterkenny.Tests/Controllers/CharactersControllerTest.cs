using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using F2018Letterkenny.Controllers;
using F2018Letterkenny.Models;
using System.Collections.Generic;
using System.Linq;

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
                    CharacterId = 43,
                    Name = "Jonesy",
                    Description = "Even Dumber Hockey Player",
                    Quote = "Backcheck, Forecheck, Paycheque",
                    Photo = "jonesey.png"
                }
            };

            mock.Setup(m => m.Characters).Returns(characters.AsQueryable());
            controller = new CharactersController(mock.Object);
        }
    }
}
