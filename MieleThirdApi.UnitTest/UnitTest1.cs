using MieleThirdApi.Data;
using MieleThirdApi.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    // https://developerhandbook.com/unit-testing/writing-unit-tests-with-nunit-and-moq/
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
        public async Task GetEndeZeitCorrect(int remainingHours, int remainingMinutes)
        {
            var api = new RestApiService();
            var g = new GeraeteManager(api);
            var a = await g.GetDeviceAsync("1235");
            var dth = new DateTimeHelperMock() { DateTimeMock = new DateTime(2018, 6, 28, 20, 20, 0) };

            a.State.remainingTime = new List<int>() { remainingHours, remainingMinutes };
            var sut = new DevicelistItem(a, dth);

            Assert.AreEqual("21:20", sut.EndeZeit);

        }
    }
}