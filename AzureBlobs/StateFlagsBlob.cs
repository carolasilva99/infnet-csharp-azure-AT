using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureBlobs
{
    public class StateFlagsBlob : Blob
    {
        private const string ContainerName = "states-flags";

        public StateFlagsBlob( ): base(ContainerName)
        {
        }
    }
}