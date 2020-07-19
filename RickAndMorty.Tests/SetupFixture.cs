namespace RickAndMorty.Tests
{
    public sealed class SetupFixture
    {
        private static readonly Setup SetupValue = SetupFixture.CreateSetup();

        public Setup Setup => SetupValue;

        private static Setup CreateSetup()
        {
            Setup setup = new Setup();
            setup.Initialize();
            return setup;
        }
    }
}