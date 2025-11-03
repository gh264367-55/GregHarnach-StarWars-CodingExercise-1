using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using GregHarnach_starWars_CodingExercise.Models;

namespace GregHarnach_starWars_CodingExercise.Tests.Models
{
    [TestClass]
    public class StarshipValidationTests
    {
        [TestMethod]
        public void Name_Is_Required()
        {
            var s = new Starship { Name = "" }; // invalid
            var ctx = new ValidationContext(s);
            var results = new List<ValidationResult>();

            var valid = Validator.TryValidateObject(s, ctx, results, validateAllProperties: true);

            Assert.IsFalse(valid);
            Assert.IsTrue(results.Exists(r => r.MemberNames.Contains("Name")));
        }
    }
}
