using System;
using System.Runtime.Serialization;

namespace Microsoft.eShopWeb.ApplicationCore.Exceptions
{
    [Serializable]
    internal class WishListNotFoundException : Exception
    {
        private int wishlistId;

        public WishListNotFoundException()
        {
        }

        public WishListNotFoundException(int wishlistId)
        {
            this.wishlistId = wishlistId;
        }

        public WishListNotFoundException(string message) : base(message)
        {
        }

        public WishListNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WishListNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}