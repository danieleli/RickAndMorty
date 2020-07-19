namespace RickAndMorty.Services
{
    using System;

    public sealed class TransientServiceException : Exception
    {
        public TransientServiceException(string message)
            : base(message)
        {
        }
    }
}