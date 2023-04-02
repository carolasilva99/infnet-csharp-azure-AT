using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobs
{
    public class CountryFlagsBlob : Blob
    {
        private const string ContainerName = "country-flags";

        public CountryFlagsBlob( ): base(ContainerName)
        {
        }
    }
}