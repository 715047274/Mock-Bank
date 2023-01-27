using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MockBank.Data;
using MockBank.Domain.Entities.Berkeleys;
using NUnit.Framework;

namespace Application.UnitTests.RepositoryTest
{
    public class MockScenarioTest
    {
        private TestUtility _tu;
        private UnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _tu = new TestUtility();
            _unitOfWork = new UnitOfWork(_tu._configuration);
        }

        [TearDown]
        public void Teardown()
        {
            _unitOfWork.Dispose();
        }

        [Test]
        public void CreateCardHolderScenarioTest()
        {
            /*
             * Steps:
             * 1. Create CardHolder with Account Information
             * 2. Verify the Account Information 
             */
            
        }

        [Test]
        public void ActiveAccountStatusTest()
        {
            /*
             *Steps:
             * 1. Check Created Account
             * 2. Update Account Status to Active
             * 3. Verify the Account status with Account Id
             */
            
        }

        [Test]
        public void LoadValueAccountTest()
        {
        }

        [Test]
        public void LoadValueAccountAndTimeOutSyncTest()
        {
        }

        [Test]
        public void AccountTransactionTest()
        {
        }

    }
}