namespace ServicesTests.Services
{
    using global::Services.Services;
    using NUnit.Framework;
    using System;

    public class TestRefereeService
    {
        [Test]
        public void RefereeService_IsValid_TooLongText_ReturnFalse()
        {
            RefereeService refereeService = new RefereeService();

            bool result = refereeService.IsValid("zaojezaknezaenzaonezaonez");

            Assert.IsFalse(result);
        }

        [Test]
        public void RefereeService_IsValid_GoodLenghtNotPatter_ReturnFalse()
        {
            RefereeService refereeService = new RefereeService();

            bool result = refereeService.IsValid("flop");

            Assert.IsFalse(result);
        }

        [Pairwise]
        public void RefereeService_IsValid_GoodLenghtNotPatter_ReturnFalse(
            [Values('r', 'j', 'v', 'b', 'o', 'n')]char firstPlay,
            [Values('r', 'j', 'v', 'b', 'o', 'n')]char secondPlay,
            [Values('r', 'j', 'v', 'b', 'o', 'n')]char thirdPlay,
            [Values('r', 'j', 'v', 'b', 'o', 'n')]char fourthPlay)
        {
            RefereeService refereeService = new RefereeService();
            char[] arr = new char[4] { firstPlay, secondPlay, thirdPlay, fourthPlay };
            bool result = refereeService.IsValid(new string(arr));

            Assert.IsTrue(result);
        }

    }
}
