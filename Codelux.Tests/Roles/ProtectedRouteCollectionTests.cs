using Moq;
using System;
using System.Linq;
using NUnit.Framework;
using Codelux.Common.Models;
using Codelux.ServiceStack.Roles;
using System.Collections.Generic;

namespace Codelux.Tests.Roles
{
    [TestFixture]
    public class ProtectedRouteCollectionTests
    {
        private Mock<IRoleValidator> _roleValidatorMock;
        private ProtectedRouteCollection _routeCollection;

        [SetUp]
        public void Setup()
        {
            _roleValidatorMock = new Mock<IRoleValidator>();
            _routeCollection = new ProtectedRouteCollection(_roleValidatorMock.Object);
        }

        [Test]
        public void GivenRouteCollectionWhenIAddProtectedRouteThenRouteIsAdded()
        {
            bool result = _routeCollection.AddProtectedRoute(typeof(TestRequest), new ModeratorRole());
            Assert.IsTrue(result);
        }

        [Test]
        public void GivenRouteCollectionWhenIAddProtectedRouteThatExistsThenRouteIsNotAdded()
        {
            // Add non-existing route
            bool result = _routeCollection.AddProtectedRoute(typeof(TestRequest), new ModeratorRole());
            Assert.IsTrue(result);

            // Add existing route
            result = _routeCollection.AddProtectedRoute(typeof(TestRequest), new ModeratorRole());
            Assert.False(result);
        }

        [Test]
        public void GivenRouteCollectionWhenIRemoveExistingRouteThenRouteIsRemoved()
        {
            bool result = _routeCollection.AddProtectedRoute(typeof(TestRequest), new ModeratorRole());
            Assert.IsTrue(result);

            Assert.IsTrue(_routeCollection.RemoveProtectedRoute(typeof(TestRequest)));
        }

        [Test]
        public void GivenRouteCollectionWhenIRemoveNonExistingRouteThenRouteIsNotRemoved()
        {
            Assert.IsFalse(_routeCollection.RemoveProtectedRoute(typeof(TestRequest)));
        }

        [Test]
        public void GivenRouteCollectionWhenIMakeRequestAndUserHasRoleThenCanExecuteReturnsTrue()
        {
            TestRoleModel model = new TestRoleModel()
            {
                Id = Guid.NewGuid(),
                Roles = new List<IRole>()
                {
                    new MemberRole(),
                    new ModeratorRole()
                },
                Username = "TestUser"
            };

            _routeCollection.AddProtectedRoute(typeof(TestRequest), new ModeratorRole());

            Assert.That(model.Roles.FirstOrDefault(x => x.Level >= new ModeratorRole().Level) != null);

            _roleValidatorMock.Setup(x => x.HasRole(model, new ModeratorRole().Level)).Returns(true);

            bool canExecute = _routeCollection.CanExecute(typeof(TestRequest), model);
            Assert.IsTrue(canExecute);

            _roleValidatorMock.Verify(x => x.HasRole(model, new ModeratorRole().Level), Times.Once);
        }

        [Test]
        public void GivenRouteCollectionWhenIMakeRequestAndUserDoesNotHaveRoleThenCanExecuteReturnsFalse()
        {
            TestRoleModel model = new TestRoleModel()
            {
                Id = Guid.NewGuid(),
                Roles = new List<IRole>()
                {
                    new MemberRole(),
                    new ModeratorRole()
                },
                Username = "TestUser"
            };

            _routeCollection.AddProtectedRoute(typeof(TestRequest), new AdminRole());

            Assert.That(model.Roles.FirstOrDefault(x => x.Level >= new ModeratorRole().Level) != null);

            _roleValidatorMock.Setup(x => x.HasRole(model, new AdminRole().Level)).Returns(false);

            bool canExecute = _routeCollection.CanExecute(typeof(TestRequest), model);
            Assert.IsFalse(canExecute);

            _roleValidatorMock.Verify(x => x.HasRole(model, new AdminRole().Level), Times.Once);
        }
    }
}
