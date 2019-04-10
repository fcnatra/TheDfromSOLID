using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using TheDfromSOLID.Interfaces;
using System.Collections.Generic;
using TheDfromSOLID;
using System.Threading;
using System;

namespace TheDFromSolidTests
{
    [TestClass]
    public class InputHubReaderTests
    {
        public static IEnumerable<object[]> _inputOutputData => new List<object[]>
        {
            new object[] {
                new List<string> {"Any text" },
                $"Any Text{Environment.NewLine}"
            },
            new object[] {
                new List<string> {"Any text","Two Lines" },
                $"Any Text{Environment.NewLine}Two Lines{Environment.NewLine}"
            },
        };

        [TestMethod]
        public void OnStartReadingReadsInmediatelyFromHubBeforeWaitingTheIntervalDefined()
        {
            var readingIntervalInMs = 2000;
            var testWaitingTimeInMs = 2;

            IHub fakeHub = A.Fake<IHub>();
            IDumpSystem fakeDumpSystem = A.Fake<IDumpSystem>();
            IConfiguration fakeConfig = A.Fake<IConfiguration>();
            A.CallTo(() => fakeConfig.ReadingIntervalInMs).Returns(readingIntervalInMs);

            var hubReader = new InputHubReader
            {
                Hub = fakeHub,
                Configuration = fakeConfig,
                DumpSystem = fakeDumpSystem
            };

            var now = DateTime.Now;
            hubReader.StartListening();
            Thread.Sleep(testWaitingTimeInMs);
            hubReader.StopListening();
            var elapsedMs = DateTime.Now.Subtract(now).TotalMilliseconds;

            Assert.IsTrue(elapsedMs < readingIntervalInMs);

            A.CallTo(() => fakeHub.ReadFromHub()).MustHaveHappenedOnceExactly();
        }

        [DataTestMethod]
        [DynamicData(nameof(_inputOutputData))]
        public void GivenATextProvidedByTheHubTheSameTextNewLineMustBeWritenIntoTheDumpSystem(List<string> inputContent, string expectedResult)
        {
            var readingIntervalInMs = 10;
            var testWaitingTimeInMs = readingIntervalInMs/2;

            IHub fakeHub = A.Fake<IHub>();
            A.CallTo(() => fakeHub.ReadFromHub()).Returns(inputContent);

            IConfiguration fakeConfig = A.Fake<IConfiguration>();
            A.CallTo(() => fakeConfig.ReadingIntervalInMs).Returns(readingIntervalInMs);

            IDumpSystem fakeDumpSystem = A.Fake<IDumpSystem>();

            var hubReader = new InputHubReader
            {
                Hub = fakeHub,
                Configuration = fakeConfig,
                DumpSystem = fakeDumpSystem
            };

            hubReader.StartListening();
            Thread.Sleep(testWaitingTimeInMs);
            hubReader.StopListening();

            A.CallTo(() => fakeDumpSystem.DumpContent(
                A<string>
                .That
                .IsEqualTo<string>(expectedResult)))
                .MustHaveHappenedOnceExactly();
        }

        [TestMethod]
        public void GivenAReadingIntervalItMustNotChangeBetweenHubReads()
        {
            var inputContent = new List<string> { "Any text" };
            var expectedResult = "Any text" + Environment.NewLine;
            var readingIntervalInMs = 10;
            var largerReadingIntervalInMs = 20000;
            var testWaitingTimeInMs = readingIntervalInMs * 2;

            IHub fakeHub = A.Fake<IHub>();
            A.CallTo(() => fakeHub.ReadFromHub()).Returns(inputContent);

            IConfiguration fakeConfig = A.Fake<IConfiguration>();
            A.CallTo(() => fakeConfig.ReadingIntervalInMs).Returns(readingIntervalInMs);

            IDumpSystem fakeDumpSystem = A.Fake<IDumpSystem>();

            var hubReader = new InputHubReader
            {
                Hub = fakeHub,
                Configuration = fakeConfig,
                DumpSystem = fakeDumpSystem
            };

            hubReader.StartListening();

            A.CallTo(() => fakeConfig.ReadingIntervalInMs).Returns(largerReadingIntervalInMs);
            Thread.Sleep(testWaitingTimeInMs);

            hubReader.StopListening();

            A.CallTo(() => fakeDumpSystem.DumpContent(
                A<string>
                .That
                .IsEqualTo(expectedResult)))
                .MustHaveHappened(3, Times.Exactly);
        }

    }
}
