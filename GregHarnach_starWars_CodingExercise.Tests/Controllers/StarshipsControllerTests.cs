using GregHarnach_starWars_CodingExercise.Controllers;
using GregHarnach_starWars_CodingExercise.Data;
using GregHarnach_starWars_CodingExercise.Models;
using GregHarnach_starWars_CodingExercise.Tests.TestUtils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace GregHarnach_starWars_CodingExercise.Tests.Controllers
{
    [TestClass]
    public class StarshipsControllerTests
    {
        private AppDbContext _db = null!;
        private StarshipsController _sut = null!;

        [TestInitialize]
        public void Setup()
        {
            _db = TestDbFactory.CreateInMemoryDb();
            StarshipSeedData.AddSampleStarships(_db);
            //_sut = new StarshipsController(_db);
            _sut = new StarshipsController(_db, NullLogger<StarshipsController>.Instance);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _db.Dispose();
        }

        [TestMethod]
        public async Task Index_Returns_View_With_List()
        {
            var result = await _sut.Index() as ViewResult;
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(System.Collections.Generic.IEnumerable<Starship>));

            var model = (System.Collections.Generic.IEnumerable<Starship>)result.Model!;
            Assert.IsTrue(model.Any());
        }

        [TestMethod]
        public async Task Details_Returns_NotFound_For_Missing_Id()
        {
            var result = await _sut.Details(id: 9999);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Details_Returns_View_For_Valid_Id()
        {
            var any = await _db.Starships.AsNoTracking().FirstAsync();
            var result = await _sut.Details(any.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Starship));
            var ship = (Starship)result.Model!;
            Assert.AreEqual(any.Id, ship.Id);
        }

        [TestMethod]
        public async Task Create_Post_Persists_And_Redirects()
        {
            var toCreate = new Starship { Name = "A-wing", Model = "RZ-1", Manufacturer = "Kuat Systems Engineering" };

            var result = await _sut.Create(toCreate) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var exists = await _db.Starships.AnyAsync(s => s.Name == "A-wing");
            Assert.IsTrue(exists);
        }

        [TestMethod]
        public async Task Edit_Post_Id_Mismatch_Returns_NotFound()
        {
            var any = await _db.Starships.AsNoTracking().FirstAsync();
            any.Name = "NewName";
            var result = await _sut.Edit(any.Id + 999, any);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task Edit_Persists_And_Redirects()
        {
            var ship = await _db.Starships.FirstAsync();
            var originalId = ship.Id;

            ship.Name = "Edited Name";

            var result = await _sut.Edit(originalId, ship) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var updated = await _db.Starships.FindAsync(originalId);
            Assert.AreEqual("Edited Name", updated!.Name);
        }

        [TestMethod]
        public async Task Delete_Get_Returns_View_When_Found()
        {
            var any = await _db.Starships.AsNoTracking().FirstAsync();
            var result = await _sut.Delete(any.Id) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Starship));
        }

        [TestMethod]
        public async Task DeleteConfirmed_Removes_And_Redirects()
        {
            var ship = await _db.Starships.AsNoTracking().FirstAsync();
            var id = ship.Id;

            var result = await _sut.DeleteConfirmed(id) as RedirectToActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);

            var deleted = await _db.Starships.FindAsync(id);
            Assert.IsNull(deleted);
        }
    }
}
