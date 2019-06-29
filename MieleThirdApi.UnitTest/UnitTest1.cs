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
    public class DevicelistItemTests
    {
        
        [SetUp]
        public void Setup()
        {
            //Hier den ToDoItem Manager wahrscheinlich global für alle Tests erzeugen?! Oder muss der vor jedem Test neu erzeugt werden?
            
        }

        [TestCase(1)]
        [TestCase(0)]
        public async Task GetCorrectApplianceIcon(int i)
        {
            var api = new RestApiService();
            var g = new GeraeteManager(api);
            var a = await g.GetDeviceAsync("1235");

            a.Ident.type.value_raw = i;
            var sut = new DevicelistItem( a );
            Assert.AreEqual(sut.IconUri, "icon.png");
        }

        [TestCase("Hund","Herz")]
        [TestCase("", "Herz")]
        [TestCase(null, "Herz")]
        public async Task GetDeviceNameCorrect(string devicename, string devicetype)
        {
            var api = new RestApiService();
            var g = new GeraeteManager(api);
            var a = await g.GetDeviceAsync("1235");

            a.Ident.deviceName = devicename;
            a.Ident.type.value_localized = devicetype;

            var sut = new DevicelistItem(a);

            if(string.IsNullOrEmpty(devicename))
                Assert.AreEqual(sut.Name, devicetype);
            else
                Assert.AreEqual(sut.Name, devicename);
        }
        
        [TestCase(1,0)]
        [TestCase(4, 0)]
        [TestCase(3, 50)]
        // null noch checken
        // 0,0 noch checken
        //kann remaining time 00:00 oder null sein? Was genau soll da dann rauskommen?
        public async Task GetEndeZeitCorrect(int remainingHours, int remainingMinutes)
        {
            var api = new RestApiService();
            var g = new GeraeteManager(api);
            var a = await g.GetDeviceAsync("1235");
            var dth = new DateTimeHelperMock() { DateTimeMock = new DateTime(2018, 6, 28, 20, 20, 0) };

            a.State.remainingTime = new List<int>() { remainingHours, remainingMinutes };
            var sut = new DevicelistItem(a, dth);

            
            //Hier macht man wahrscheinlich eher drei Tests raus?! 
            if(remainingHours == 4) Assert.AreEqual("00:20", sut.EndeZeit, $"Correct EndeZeit should be 00:20, but is {sut.EndeZeit}");
            if(remainingHours == 3) Assert.AreEqual("00:10", sut.EndeZeit, $"Correct EndeZeit should be 00:10, but is {sut.EndeZeit}");
            if(remainingHours == 1) Assert.AreEqual("21:20", sut.EndeZeit, $"Correct EndeZeit should be 21:20, but is {sut.EndeZeit}");
        }

        [Test]
        // remaining , elapsed
        // null , null => 0.0
        // 1:59, null => 0.0
        // null, 2 => 0.0
        // 1:48, 0:12 => 0,1
        // 0:02, 2:59
        // 0:0, 2:0
        public async Task GetProgressbarValue()
        {
            var api = new RestApiService();
            var g = new GeraeteManager(api);
            var a = await g.GetDeviceAsync("1235");

            a.State.remainingTime = new List<int>() { 1, 48 };
            a.State.elapsedTime = new List<int>() { 0, 12 };

            var sut = new DevicelistItem(a);

            Assert.AreEqual(.1, sut.ProgressBarValue);
        }
    }
}