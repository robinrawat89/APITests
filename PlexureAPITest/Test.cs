using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp.Serialization.Json;

namespace PlexureAPITest
{
    [TestFixture]
    public class Test
    {
        Service service;

        [OneTimeSetUp]
        public void Setup()
        {
            service = new Service();
            
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            if (service != null)
            {
                service.Dispose();
                service = null;
            }
        }

        [Test]
        public void TEST_001_Login_With_Valid_User()
        {
            var response = service.Login("Tester", "Plexure123");

            response.Expect(HttpStatusCode.OK);
           
        }
        [Test]
        public void TEST_002_Login_With_InValid_User()
        {
            var response = service.Login("Tester123", "Plexure123");

            response.Expect(HttpStatusCode.Unauthorized);

            response.ExpectError("\"Error: Unauthorized\"");


        }

        [Test]
        public void TEST_003_Login_With_Blank_User()
        {
            var response = service.Login("", "Plexure123");

            response.Expect(HttpStatusCode.Unauthorized);

            response.ExpectError("\"Error: Unauthorized\"");

        }


        [Test]
        public void TEST_005_Get_Points_For_Logged_In_User()
        {
            var points = service.GetPoints();
            
            points.Expect(HttpStatusCode.Accepted);
        
        }

        [Test]
        public void TEST_004_Purchase_Product()
        {
            int productId = 1;
            var response = service.Purchase(productId);

            response.Expect(HttpStatusCode.Accepted);
        }

        [Test]
        public void TEST_006_Purchase_InvalidProduct()
        {
            int productId = 0200001;
            var response = service.Purchase(productId);

            response.Expect(HttpStatusCode.BadRequest);

            response.ExpectError("\"Error: Invalid product id\"");

        }
    }
}
