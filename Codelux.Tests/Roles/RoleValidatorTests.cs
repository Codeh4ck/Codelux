using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codelux.Common.Models;
using Codelux.ServiceStack.Roles;
using Microsoft.Extensions.Primitives;
using NUnit.Framework;

namespace Codelux.Tests.Roles
{
    [TestFixture]
    public class RoleValidatorTests
    {
        private TestRoleValidator _testRoleValidator;

        [SetUp]
        public void Setup()
        {
            _testRoleValidator = new();
        }

        [Test]
        public void GivenValidUserIdWhenIGetModelAsyncThenValidModelIsReturned()
        {
            Guid userId = TestRoleValidator.TestUserId;
            
            IHasRole roleableModel = _testRoleValidator.GetModelAsync(userId).Result;

            Assert.IsNotNull(roleableModel);
            Assert.AreEqual(typeof(TestRoleModel), roleableModel.GetType());

            TestRoleModel testRoleModel = (TestRoleModel)roleableModel;
            Assert.AreEqual(userId, testRoleModel.Id);
            Assert.AreEqual(2, testRoleModel.Roles.Count);
        }

        [Test]
        public void GivenInvalidUserIdWhenIGetModelAsyncThenNullIsReturned()
        {
            Guid userId = Guid.NewGuid();

            IHasRole roleableModel = _testRoleValidator.GetModelAsync(userId).Result;

            Assert.IsNull(roleableModel);
        }

        [Test]
        public void GivenInvalidKeyTypeWhenIGetModelAsyncThenNullIsReturned()
        {
            string userId = "some-user-id";
            IHasRole roleableModel = _testRoleValidator.GetModelAsync(userId).Result;

            Assert.IsNull(roleableModel);
        }

        [Test]
        public void GivenValidIHasRoleModelAndHasValidRoleWhenIHasRolesThenTrueIsReturned()
        {
            Guid userId = TestRoleValidator.TestUserId;

            IHasRole roleableModel = _testRoleValidator.GetModelAsync(userId).Result;

            Assert.IsNotNull(roleableModel);

            bool hasMemberRole = _testRoleValidator.HasRole(roleableModel, new MemberRole().Level);
            bool hasModeratorRole = _testRoleValidator.HasRole(roleableModel, new ModeratorRole().Level);
            bool hasAdminRole = _testRoleValidator.HasRole(roleableModel, new AdminRole().Level);

            Assert.IsTrue(hasMemberRole);
            Assert.IsTrue(hasModeratorRole);
            Assert.IsFalse(hasAdminRole);
        }

        [Test]
        public void GivenIHasRoleModelWithNullOrEmptyRolesWhenIHasRoleThenFalseIsReturned()
        {
            Guid userId = TestRoleValidator.TestUserId;

            IHasRole roleableModel = _testRoleValidator.GetModelAsync(userId).Result;

            Assert.IsNotNull(roleableModel);

            roleableModel.Roles.Clear();
            
            Assert.AreEqual(0, roleableModel.Roles.Count);
            Assert.IsFalse(_testRoleValidator.HasRole(roleableModel, new ModeratorRole().Level));

            roleableModel = _testRoleValidator.GetModelAsync(userId).Result;
            Assert.IsNotNull(roleableModel);

            roleableModel.Roles = null;
            Assert.IsFalse(_testRoleValidator.HasRole(roleableModel, new ModeratorRole().Level));
        }
    }


    public class TestRoleValidator : RoleValidatorBase
    {
        public static readonly Guid TestUserId = Guid.Parse("810d6114-60d0-4050-8aca-26deabff1d74");

        public override Task<IHasRole> GetModelAsync(object key, CancellationChangeToken token = default)
        {
            if (key.GetType() != typeof(Guid)) return Task.FromResult((IHasRole)null);
            Guid guidKey = (Guid)key;

            if (guidKey != TestUserId) return Task.FromResult((IHasRole)null);

            IHasRole hasRole = new TestRoleModel()
            {
                Id = TestUserId,
                Username = "TestUser",
                Roles = new()
                {
                    new MemberRole(),
                    new ModeratorRole()
                }
            };

            return Task.FromResult(hasRole);
        }
    }
}
