using MieleThirdApi.Data;
using MieleThirdApi.Model;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
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
            var g = new GeraeteManager();
            var a = await g.GetDeviceAsync("1235");

            a.Ident.type.value_raw = i;
            var sut = new DevicelistItem( a );
            Assert.AreEqual(sut.IconUri, "icon.png");
        }

    }
}