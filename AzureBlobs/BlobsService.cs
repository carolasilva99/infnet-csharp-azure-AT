using AzureBlobs;

namespace CountriesApi.Services
{
    public static class BlobsService
    {
        public static async Task<string> Upload(string base64, PhotoTypeEnum photoType)
        {
            if (string.IsNullOrEmpty(base64)) 
                return string.Empty;

            var blob = photoType switch
            {
                PhotoTypeEnum.STATE_FLAG => new StateFlagsBlob(),
                PhotoTypeEnum.COUNTRY_FLAG => new CountryFlagsBlob(),
                PhotoTypeEnum.PROFILE_PIC => new StateFlagsBlob(),
                _ => default(Blob)
            };

            return await blob.AddBlobToContainer(base64);
        }

    }

    public enum PhotoTypeEnum
    {
        STATE_FLAG, COUNTRY_FLAG, PROFILE_PIC
    }
}
