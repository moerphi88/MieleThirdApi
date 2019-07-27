using MieleThirdApi.Data;
using MieleThirdApi.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    // https://developerhandbook.com/unit-testing/writing-unit-tests-with-nunit-and-moq/
    // https://github.com/nunit/docs/wiki/TestCase-Attribute
    public class LoginUnitTests
    {
        
        [SetUp]
        public void Setup()
        {
            //Hier den ToDoItem Manager wahrscheinlich global für alle Tests erzeugen?! Oder muss der vor jedem Test neu erzeugt werden?
            
        }

        //Non valid test implemented

        [Test]
        public async Task LoginSuccessful()
        {
            var sut = new LoginMockManager();

            Assert.IsFalse(await sut.IsLoggedIn());
        }

        
    }
}